using BibaFramework.BibaGame;
using strange.extensions.command.impl;
using System;

namespace BibaFramework.BibaAnalytic
{
	public abstract class BaseTrackActivityCommand : Command
	{
		[Inject]
		public bool Status { get; set; }

		[Inject]
		public BibaProfile BibaProfile { get; set; }

		[Inject]
		public IDataService DataService { get; set; }

		public override void Execute ()
		{
			if (Status) 
			{
				StartTracking ();
				TurnOffOtherTrackingSignals ();
			} 
			else
			{	
				AddActivityTime ();
			}
		}

		void StartTracking ()
		{
			if(LMVSession.DateStart == default(DateTime))
			{
				LMVSession.DateStart = DateTime.UtcNow;
			}
		}

		void AddActivityTime ()
		{
			if (LMVSession.DateStart!= default(DateTime)) 
			{
				LMVSession.SessionScore += (DateTime.UtcNow - LMVSession.DateStart).TotalSeconds;
				LMVSession.DateStart = default(DateTime);

				DataService.Save ();
			}
		}

		protected abstract LMVSession LMVSession { get;	}
		protected abstract void TurnOffOtherTrackingSignals();
	}
}