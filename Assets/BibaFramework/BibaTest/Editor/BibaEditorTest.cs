using System;
using System.Diagnostics;
using BibaFramework.BibaAnalytic;
using BibaFramework.BibaGame;
using BibaFramework.BibaMenu;
using NUnit.Framework;
using strange.extensions.context.impl;
using strange.extensions.mediation.api;
using UnityEngine;
using UnityEngine.Experimental.Director;

namespace BibaFramework.BibaTest
{
	public class BibaEditorTest 
	{
		private StubContext StubContext { get; set; }

		private BibaDevice BibaDevice { get; set; }
		private BibaDeviceSession BibaDeviceSession { get; set; }
		private BibaAccount BibaAccount { get; set; }
		private BibaProfile BibaProfile { get; set; }

		private IAnimatorControllerPlayable StubAnimator { get; set; }

		[SetUp]
		public void Init()
		{
			Context.firstContext = null;
			StubContext = new StubContext (new object(), true);
			StubContext.Start ();

			StubContext.injectionBinder.Bind<IMediationBinder>().To<StubMediationBinder>().ToSingleton();
			StubContext.mediationBinder = StubContext.injectionBinder.GetInstance<IMediationBinder>() as IMediationBinder;
			StubContext.mediationBinder.Bind<MockView>().To<MockMediator>();

			//Setup Model
			BibaDevice = GetInstanceFromContext<BibaDevice> ();
			BibaDeviceSession = GetInstanceFromContext<BibaDeviceSession> ();
			BibaAccount = GetInstanceFromContext<BibaAccount> ();

			BindInstanceToContext<BibaProfile> (BibaAccount.BibaProfiles [0]);
			BibaProfile = GetInstanceFromContext<BibaProfile>();

			StubAnimator = GetInstanceFromContext<IAnimatorControllerPlayable> (BibaMenuConstants.BIBA_STATE_MACHINE);
		}

		void Reset()
		{
			BibaDevice.Reset();
			BibaDeviceSession.Reset();
			BibaAccount.Reset();

			BindInstanceToContext<BibaProfile> (BibaAccount.BibaProfiles [0]);
			BibaProfile = GetInstanceFromContext<BibaProfile>();

			GetInstanceFromContext<TestModelResetSignal> ().Dispatch ();
		}

		#region - Activity Tracking
		[Test]
		public void TestEndActivityTracking()
		{
			GetInstanceFromContext<ApplicationPausedSignal> ().Dispatch ();
			Assert.AreEqual (0, (int) BibaProfile.BibaProfileSession.SessionLScore);

			Reset ();
		}

		[Test]
		public void TestStartActivityTracking()
		{
			GetInstanceFromContext<ApplicationPausedSignal> ().Dispatch ();
			WaitForSeconds (1);
			GetInstanceFromContext<ApplicationUnPausedSignal> ().Dispatch();
			Assert.AreEqual(0, GetSeondsSinceTime(BibaProfile.BibaProfileSession.LScoreStart));

			Reset ();
		}

		[Test]
		public void TestSwitchActivityTracking()
		{
			//Reset the activity tracking
			GetInstanceFromContext<ApplicationPausedSignal>().Dispatch ();

			//Start light activity
			GetInstanceFromContext<ApplicationUnPausedSignal>().Dispatch ();

			WaitForSeconds (1);

			//Start vigorous activity
			GetInstanceFromContext<ToggleTrackVigorousActivitySignal>().Dispatch (true);
			Assert.AreEqual(0, GetSeondsSinceTime(BibaProfile.BibaProfileSession.VScoreStart));
			Assert.AreEqual (1, (int)BibaProfile.BibaProfileSession.SessionLScore);

			WaitForSeconds (1);

			//Start moderate activity
			GetInstanceFromContext<ToggleTrackModerateActivitySignal>().Dispatch (true);
			Assert.AreEqual (1, (int) BibaProfile.BibaProfileSession.SessionVScore); 
			Assert.AreEqual (1, (int) BibaProfile.BibaProfileSession.SessionLScore); 

			WaitForSeconds (1);

			//Switch back to light activity
			GetInstanceFromContext<ToggleTrackLightActivitySignal>().Dispatch (true);
			Assert.AreEqual (1, (int) BibaProfile.BibaProfileSession.SessionMScore);
			Assert.AreEqual (1, (int) BibaProfile.BibaProfileSession.SessionVScore); 
			Assert.AreEqual (1, (int) BibaProfile.BibaProfileSession.SessionLScore); 

			Reset ();
		}
		#endregion

		#region - Gameplay
		[Test]
		public void TestTrySetHighScore()
		{
			var newScore = BibaDevice.Highscore - 1;
			GetInstanceFromContext<TryToSetHighScoreSignal> ().Dispatch (newScore);
			Assert.Greater(BibaDevice.Highscore, newScore);

			newScore = BibaDevice.Highscore + 1;
			GetInstanceFromContext<TryToSetHighScoreSignal> ().Dispatch (newScore);
			Assert.AreEqual (BibaDevice.Highscore, newScore);

			Reset ();
		}

		[Test]
		public void TestEquipmentSelected()
		{
			var equipmentSelected = BibaDeviceSession.SelectedEquipments.Find (equip => equip.EquipmentType == BibaEquipmentType.bridge);
			Assert.IsNull (equipmentSelected);

			//Select the bridge equipment
			GetInstanceFromContext<EquipmentSelectedSignal> ().Dispatch (BibaEquipmentType.bridge, true);

			equipmentSelected = BibaDeviceSession.SelectedEquipments.Find (equip => equip.EquipmentType == BibaEquipmentType.bridge);
			Assert.IsNotNull (equipmentSelected);

			var equipmentPlayed = BibaDevice.TotalEquipments.Find (equip => equip.EquipmentType == BibaEquipmentType.bridge);
			Assert.IsNotNull (equipmentPlayed);

			//Deselect the bridge equipment
			GetInstanceFromContext<EquipmentSelectedSignal> ().Dispatch (BibaEquipmentType.bridge, false);

			equipmentSelected = BibaDeviceSession.SelectedEquipments.Find (equip => equip.EquipmentType == BibaEquipmentType.bridge);
			Assert.IsNull (equipmentSelected);

			Reset ();
		}

		[Test]
		public void TestEquipmentPlayed()
		{
			var equipment = BibaDevice.TotalEquipments.Find (equip => equip.EquipmentType == BibaEquipmentType.bridge);
			Assert.IsNotNull (equipment);
			Assert.AreEqual (equipment.NumberOfTimePlayed, 0);

			GetInstanceFromContext<EquipmentPlayedSignal> ().Dispatch (BibaEquipmentType.bridge);
			Assert.AreEqual (equipment.NumberOfTimePlayed, 1);

			GetInstanceFromContext<EquipmentPlayedSignal> ().Dispatch (BibaEquipmentType.bridge);
			Assert.AreEqual (equipment.NumberOfTimePlayed, 2);

			Reset ();
		}
		#endregion

		#region - Inactive
		[Test]
		public void TestInactiveScreen()
		{
			GetInstanceFromContext<EquipmentSelectedSignal>().Dispatch(BibaEquipmentType.bridge, true);
			GetInstanceFromContext<TestCheckForSessionEndCommandSignal> ().Dispatch();
			Assert.IsFalse(StubAnimator.GetBool(MenuStateCondition.ShowInactive));

			GetInstanceFromContext<EquipmentSelectedSignal>().Dispatch(BibaEquipmentType.bridge, false);
			BibaDevice.LastPlayedTime = DateTime.UtcNow - BibaGameConstants.INACTIVE_DURATION;
			WaitForSeconds (1);
			GetInstanceFromContext<TestCheckForSessionEndCommandSignal> ().Dispatch();
			Assert.IsFalse (StubAnimator.GetBool(MenuStateCondition.ShowInactive));

			GetInstanceFromContext<EquipmentSelectedSignal>().Dispatch(BibaEquipmentType.bridge, true);
			GetInstanceFromContext<TestCheckForSessionEndCommandSignal> ().Dispatch();
			Assert.IsTrue (StubAnimator.GetBool(MenuStateCondition.ShowInactive));

			Reset ();
		}
		#endregion

		#region - TagScan
		[Test]
		public void TestTagScanScreen()
		{
			var checkTagScanSignal = GetInstanceFromContext<TestCheckToSkipTagScanCommandSignal> ();

			BibaDeviceSession.TagEnabled = false;
			checkTagScanSignal.Dispatch ();
			Assert.IsFalse(StubAnimator.GetBool(MenuStateCondition.ShowTagScan));

			BibaDeviceSession.TagEnabled = true;
			BibaDevice.LastCameraReminderTime = DateTime.UtcNow - BibaGameConstants.AR_REMINDER_DURATION + new TimeSpan(0,0,1);
			checkTagScanSignal.Dispatch ();
			Assert.IsFalse (StubAnimator.GetBool (MenuStateCondition.ShowTagScan));

			WaitForSeconds (1);

			checkTagScanSignal.Dispatch ();
			Assert.IsTrue (StubAnimator.GetBool (MenuStateCondition.ShowTagScan));

			Reset ();
		}
		#endregion

		#region - Chartboost
		[Test]
		public void TestShowChartBoostScreen()
		{
			var checkChartboostSignal = GetInstanceFromContext<TestCheckForChartBoostCommandSignal> ();

			checkChartboostSignal.Dispatch ();
			Assert.IsTrue(StubAnimator.GetBool(MenuStateCondition.ShowChartBoost));

			BibaDevice.LastChartBoostTime = DateTime.UtcNow;

			checkChartboostSignal.Dispatch ();
			Assert.IsFalse (StubAnimator.GetBool (MenuStateCondition.ShowChartBoost));

			BibaDevice.LastChartBoostTime = DateTime.UtcNow - BibaGameConstants.CHARTBOOST_CHECK_DURATION;

			checkChartboostSignal.Dispatch ();
			Assert.IsTrue(StubAnimator.GetBool(MenuStateCondition.ShowChartBoost));

			Reset ();
		}
		#endregion

		#region - Utility
		void BindInstanceToContext<T>(T instance)
		{
			StubContext.injectionBinder.Unbind<T> ();
			StubContext.injectionBinder.Bind<T> ().To (instance).ToSingleton ();
		}

		T GetInstanceFromContext<T>(string name = "")
		{
			if (string.IsNullOrEmpty (name)) 
			{
				return StubContext.injectionBinder.GetInstance<T> ();
			}

			return StubContext.injectionBinder.GetInstance<T> (name);
		}

		int GetSeondsSinceTime(DateTime startTime)
		{
			return (int) ((DateTime.UtcNow - startTime).TotalSeconds);
		}
		#endregion

		private void WaitForSeconds(float seconds)
		{
			var stopWatch = new Stopwatch ();
			stopWatch.Reset();
			stopWatch.Start();
			while (stopWatch.ElapsedMilliseconds < seconds * 1000) 
			{
			}
			stopWatch.Stop();
		}
	}
}