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

		private BibaSystem BibaSystem { get; set; }
		private BibaSession BibaSession { get; set; }
		private BibaAccount BibaAccount { get; set; }

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
			BibaSystem = GetInstanceFromContext<BibaSystem> ();
			BibaSession = GetInstanceFromContext<BibaSession> ();
			BibaAccount = GetInstanceFromContext<BibaAccount> ();

			StubAnimator = GetInstanceFromContext<IAnimatorControllerPlayable> (BibaMenuConstants.BIBA_STATE_MACHINE);
		}

		void Reset()
		{
			BibaSystem = new BibaSystem ();
			BibaSession = new BibaSession ();
			BibaAccount = new BibaAccount ();

			GetInstanceFromContext<TestModelResetSignal> ().Dispatch ();
		}

		#region - Activity Tracking
		[Test]
		public void TestEndActivityTracking()
		{
			GetInstanceFromContext<ApplicationPausedSignal> ().Dispatch ();
			Assert.AreEqual (0, (int) BibaAccount.SelectedProfile.LScore);

			Reset ();
		}

		[Test]
		public void TestStartActivityTracking()
		{
			GetInstanceFromContext<ApplicationPausedSignal> ().Dispatch ();
			WaitForSeconds (1);
			GetInstanceFromContext<ApplicationUnPausedSignal> ().Dispatch();
			Assert.AreEqual(0, GetSeondsSinceTime(BibaSession.LScoreStart));

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
			Assert.AreEqual(0, GetSeondsSinceTime(BibaSession.VScoreStart));
			Assert.AreEqual (1, (int)BibaAccount.SelectedProfile.LScore);

			WaitForSeconds (1);

			//Start moderate activity
			GetInstanceFromContext<ToggleTrackModerateActivitySignal>().Dispatch (true);
			Assert.AreEqual (1, (int) BibaAccount.SelectedProfile.VScore); 
			Assert.AreEqual (1, (int) BibaAccount.SelectedProfile.LScore); 

			WaitForSeconds (1);

			//Switch back to light activity
			GetInstanceFromContext<ToggleTrackLightActivitySignal>().Dispatch (true);
			Assert.AreEqual (1, (int) BibaAccount.SelectedProfile.MScore);
			Assert.AreEqual (1, (int) BibaAccount.SelectedProfile.VScore); 
			Assert.AreEqual (1, (int) BibaAccount.SelectedProfile.LScore); 

			Reset ();
		}
		#endregion

		#region - Gameplay
		[Test]
		public void TestTrySetHighScore()
		{
			var newScore = BibaSystem.Highscore - 1;
			GetInstanceFromContext<TryToSetHighScoreSignal> ().Dispatch (newScore);
			Assert.Greater(BibaSystem.Highscore, newScore);

			newScore = BibaSystem.Highscore + 1;
			GetInstanceFromContext<TryToSetHighScoreSignal> ().Dispatch (newScore);
			Assert.AreEqual (BibaSystem.Highscore, newScore);

			Reset ();
		}

		[Test]
		public void TestEquipmentSelected()
		{
			var equipmentSelected = BibaSession.SelectedEquipments.Find (equip => equip.EquipmentType == BibaEquipmentType.bridge);
			Assert.IsNull (equipmentSelected);

			//Select the bridge equipment
			GetInstanceFromContext<EquipmentSelectedSignal> ().Dispatch (BibaEquipmentType.bridge, true);

			equipmentSelected = BibaSession.SelectedEquipments.Find (equip => equip.EquipmentType == BibaEquipmentType.bridge);
			Assert.IsNotNull (equipmentSelected);

			var equipmentPlayed = BibaAccount.SelectedProfile.PlayedEquipments.Find (equip => equip.EquipmentType == BibaEquipmentType.bridge);
			Assert.IsNotNull (equipmentPlayed);

			//Deselect the bridge equipment
			GetInstanceFromContext<EquipmentSelectedSignal> ().Dispatch (BibaEquipmentType.bridge, false);

			equipmentSelected = BibaSession.SelectedEquipments.Find (equip => equip.EquipmentType == BibaEquipmentType.bridge);
			Assert.IsNull (equipmentSelected);

			Reset ();
		}

		[Test]
		public void TestEquipmentPlayed()
		{
			var equipment = BibaAccount.SelectedProfile.PlayedEquipments.Find (equip => equip.EquipmentType == BibaEquipmentType.bridge);
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
			BibaSystem.LastPlayedTime = DateTime.UtcNow - BibaGameConstants.INACTIVE_DURATION;
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

			BibaSession.TagEnabled = false;
			checkTagScanSignal.Dispatch ();
			Assert.IsFalse(StubAnimator.GetBool(MenuStateCondition.ShowTagScan));

			BibaSession.TagEnabled = true;
			BibaSystem.LastCameraReminderTime = DateTime.UtcNow - BibaGameConstants.AR_REMINDER_DURATION + new TimeSpan(0,0,1);
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

			BibaSystem.LastChartBoostTime = DateTime.UtcNow;

			checkChartboostSignal.Dispatch ();
			Assert.IsFalse (StubAnimator.GetBool (MenuStateCondition.ShowChartBoost));

			BibaSystem.LastChartBoostTime = DateTime.UtcNow - BibaGameConstants.CHARTBOOST_CHECK_DURATION;

			checkChartboostSignal.Dispatch ();
			Assert.IsTrue(StubAnimator.GetBool(MenuStateCondition.ShowChartBoost));

			Reset ();
		}
		#endregion

		#region - Utility
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