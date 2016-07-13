using BibaFramework.BibaGame;
using strange.extensions.command.impl;

namespace BibaFramework.BibaAnalytic
{
	public abstract class BaseTrackActivityCommand : Command
	{
		[Inject]
		public bool Status { get; set; }

		[Inject]
		public BibaSession BibaSession { get; set; }

		[Inject]
		public BibaAccount BibaAccount { get; set; }

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

		protected abstract void StartTracking ();
		protected abstract void TurnOffOtherTrackingSignals();
		protected abstract void AddActivityTime();
	}
}

