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

		private BibaGameModel BibaGameModel { get; set; }
		private BibaSessionModel BibaSessionModel { get; set; }
		private SessionInfo SessionInfo { get { return BibaSessionModel.SessionInfo; }}
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
			BibaGameModel = GetInstanceFromContext<BibaGameModel> ();
			BibaSessionModel = GetInstanceFromContext<BibaSessionModel> ();
			StubAnimator = GetInstanceFromContext<IAnimatorControllerPlayable> (BibaMenuConstants.BIBA_STATE_MACHINE);
		}

		void Reset()
		{
			BibaGameModel.Reset ();
			GetInstanceFromContext<TestModelResetSignal> ().Dispatch ();
		}

		#region - Activity Tracking
		[Test]
		public void TestEndActivityTracking()
		{
			GetInstanceFromContext<ApplicationPausedSignal> ().Dispatch ();
			Assert.AreEqual (0, (int) SessionInfo.LightActivityTime);

			Reset ();
		}

		[Test]
		public void TestStartActivityTracking()
		{
			GetInstanceFromContext<ApplicationPausedSignal> ().Dispatch ();
			WaitForSeconds (1);
			GetInstanceFromContext<ApplicationUnPausedSignal> ().Dispatch();
			Assert.AreEqual(0, GetSeondsSinceTime(SessionInfo.LightTrackingStartTime));

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
			Assert.AreEqual(0, GetSeondsSinceTime(BibaSessionModel.SessionInfo.VigorousTrackingStartTime));
			Assert.AreEqual (1, (int)BibaSessionModel.SessionInfo.LightActivityTime);

			WaitForSeconds (1);

			//Start moderate activity
			GetInstanceFromContext<ToggleTrackModerateActivitySignal>().Dispatch (true);
			Assert.AreEqual (1, (int) BibaSessionModel.SessionInfo.VigorousActivityTime); 
			Assert.AreEqual (1, (int) BibaSessionModel.SessionInfo.LightActivityTime); 

			WaitForSeconds (1);

			//Switch back to light activity
			GetInstanceFromContext<ToggleTrackLightActivitySignal>().Dispatch (true);
			Assert.AreEqual (1, (int) BibaSessionModel.SessionInfo.ModerateActivityTime);
			Assert.AreEqual (1, (int) BibaSessionModel.SessionInfo.VigorousActivityTime); 
			Assert.AreEqual (1, (int) BibaSessionModel.SessionInfo.LightActivityTime); 

			Reset ();
		}
		#endregion

		#region - Gameplay
		[Test]
		public void TestTrySetHighScore()
		{
			var newScore = BibaGameModel.HighScore - 1;
			GetInstanceFromContext<TryToSetHighScoreSignal> ().Dispatch (newScore);
			Assert.Greater(BibaGameModel.HighScore, newScore);

			newScore = BibaGameModel.HighScore + 1;
			GetInstanceFromContext<TryToSetHighScoreSignal> ().Dispatch (newScore);
			Assert.AreEqual (BibaGameModel.HighScore, newScore);

			Reset ();
		}

		[Test]
		public void TestEquipmentSelected()
		{
			var equipmentSelected = BibaGameModel.SelectedEquipments.Find (equip => equip.EquipmentType == BibaEquipmentType.bridge);
			Assert.IsNull (equipmentSelected);

			//Select the bridge equipment
			GetInstanceFromContext<EquipmentSelectedSignal> ().Dispatch (BibaEquipmentType.bridge, true);

			equipmentSelected = BibaGameModel.SelectedEquipments.Find (equip => equip.EquipmentType == BibaEquipmentType.bridge);
			Assert.IsNotNull (equipmentSelected);

			var equipmentPlayed = BibaGameModel.TotalPlayedEquipments.Find (equip => equip.EquipmentType == BibaEquipmentType.bridge);
			Assert.IsNotNull (equipmentPlayed);
			Assert.AreEqual (equipmentPlayed.NumberOfTimeSelected, 1);

			//Deselect the bridge equipment
			GetInstanceFromContext<EquipmentSelectedSignal> ().Dispatch (BibaEquipmentType.bridge, false);

			equipmentSelected = BibaGameModel.SelectedEquipments.Find (equip => equip.EquipmentType == BibaEquipmentType.bridge);
			Assert.IsNull (equipmentSelected);
			Assert.AreEqual (equipmentPlayed.NumberOfTimeSelected, 0);

			Reset ();
		}

		[Test]
		public void TestEquipmentPlayed()
		{
			var equipment = BibaGameModel.TotalPlayedEquipments.Find (equip => equip.EquipmentType == BibaEquipmentType.bridge);
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
			BibaGameModel.LastPlayedTime = DateTime.UtcNow - BibaGameConstants.INACTIVE_DURATION;
			WaitForSeconds (1);
			GetInstanceFromContext<TestCheckForInactiveResetCommandSignal> ().Dispatch();
			Assert.IsFalse (StubAnimator.GetBool(MenuStateCondition.ShowInactive));

			GetInstanceFromContext<EquipmentSelectedSignal>().Dispatch(BibaEquipmentType.bridge, true);
			GetInstanceFromContext<TestCheckForInactiveResetCommandSignal> ().Dispatch();
			Assert.IsTrue (StubAnimator.GetBool(MenuStateCondition.ShowInactive));

			Reset ();
		}
		#endregion

		#region - TagScan
		[Test]
		public void TestTagScanScreen()
		{
			var checkTagScanSignal = GetInstanceFromContext<TestCheckToSkipTagScanCommandSignal> ();

			BibaGameModel.TagEnabled = false;
			checkTagScanSignal.Dispatch ();
			Assert.IsFalse(StubAnimator.GetBool(MenuStateCondition.ShowTagScan));

			BibaGameModel.TagEnabled = true;
			BibaGameModel.LastCameraReminderTime = DateTime.UtcNow - BibaGameConstants.AR_REMINDER_DURATION + new TimeSpan(0,0,1);
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

			BibaGameModel.LastChartBoostTime = DateTime.UtcNow;

			checkChartboostSignal.Dispatch ();
			Assert.IsFalse (StubAnimator.GetBool (MenuStateCondition.ShowChartBoost));

			BibaGameModel.LastChartBoostTime = DateTime.UtcNow - BibaGameConstants.CHARTBOOST_CHECK_DURATION;

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