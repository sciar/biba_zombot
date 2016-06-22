using System;
using System.Diagnostics;
using BibaFramework.BibaAnalytic;
using BibaFramework.BibaGame;
using NUnit.Framework;
using strange.extensions.context.impl;
using strange.extensions.mediation.api;

namespace BibaFramework.BibaTest
{
	public class BibaEditorTest 
	{
		private StubContext StubContext { get; set; }

		private BibaGameModel BibaGameModel { get; set; }
		private BibaSessionModel BibaSessionModel { get; set; }

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
			BibaGameModel = StubContext.injectionBinder.GetInstance<BibaGameModel> ();
			BibaSessionModel = StubContext.injectionBinder.GetInstance<BibaSessionModel> ();
		}

		[Test]
		public void TestAppPauseActivityTracking()
		{
			var appPauseSignal = StubContext.injectionBinder.GetInstance<ApplicationPausedSignal> () as ApplicationPausedSignal;
			appPauseSignal.Dispatch ();

			Assert.AreEqual (0, (int) BibaSessionModel.SessionInfo.LightActivityTime);
		}

		[Test]
		public void TestAppUnpauseActivityTracking()
		{
			var prevActivityTime = BibaSessionModel.SessionInfo.LightActivityTime;

			var appUnPausedSignal = StubContext.injectionBinder.GetInstance<ApplicationUnPausedSignal> () as ApplicationUnPausedSignal;
			appUnPausedSignal.Dispatch ();

			var secondsSinceTrackingStart = (int)(DateTime.UtcNow - BibaSessionModel.SessionInfo.LightTrackingStartTime).TotalSeconds;
			Assert.AreEqual(0, secondsSinceTrackingStart);
		}

		[Test]
		public void TestSwitchActivitytracking()
		{
			var appPauseSignal = StubContext.injectionBinder.GetInstance<ApplicationPausedSignal> () as ApplicationPausedSignal;
			var appUnPausedSignal = StubContext.injectionBinder.GetInstance<ApplicationUnPausedSignal> () as ApplicationUnPausedSignal;
			var toggleLightActivitySignal = StubContext.injectionBinder.GetInstance<ToggleTrackLightActivitySignal> () as ToggleTrackLightActivitySignal;
			var toggleModerateActivitySignal = StubContext.injectionBinder.GetInstance<ToggleTrackModerateActivitySignal> () as ToggleTrackModerateActivitySignal;
			var toggleVigorousActivitySignal = StubContext.injectionBinder.GetInstance<ToggleTrackVigorousActivitySignal> () as ToggleTrackVigorousActivitySignal;

			appPauseSignal.Dispatch ();
			appUnPausedSignal.Dispatch ();

			Delay (1f);

			toggleVigorousActivitySignal.Dispatch (true);
			var secondsSinceTrackingStart = (int)(DateTime.UtcNow - BibaSessionModel.SessionInfo.VigorousTrackingStartTime).TotalSeconds;
			Assert.AreEqual(0, secondsSinceTrackingStart);

			Delay (1f);

			toggleModerateActivitySignal.Dispatch (true);
			Assert.AreEqual (1, (int) BibaSessionModel.SessionInfo.VigorousActivityTime); 

			Delay (1f);

			toggleLightActivitySignal.Dispatch (true);
			Assert.AreEqual (1, (int) BibaSessionModel.SessionInfo.ModerateActivityTime); 
		}

		private void Delay(float seconds)
		{
			var stopWatch = new Stopwatch ();
			stopWatch.Reset();
			stopWatch.Start();
			while (stopWatch.ElapsedMilliseconds < seconds * 1000) { }
			stopWatch.Stop();
		}
	}
}