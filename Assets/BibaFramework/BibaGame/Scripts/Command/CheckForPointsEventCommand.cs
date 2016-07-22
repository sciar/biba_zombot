using System;
using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
	public abstract class BaseCheckForPointsEventCommand : Command
	{
		public abstract string KeyToCheck { get; }

		[Inject]
		public PointEventService PointEventService { get; set; }

		public override void Execute ()
		{
			PointEventService.CheckAndCompletePointEvent (KeyToCheck);
		}
	}

	public class CheckForPointsEventCommand : BaseCheckForPointsEventCommand
	{
		[Inject]
		public string keyToCheck { get; set; }

		public override string KeyToCheck {
			get {
				return keyToCheck;
			}
		}
	}

	public abstract class BaseCheckFirstTimePointsEventCommand : BaseCheckForPointsEventCommand
	{
		[Inject]
		public BibaProfile BibaProfile { get; set; }

		public override void Execute ()
		{
			if (!BibaProfile.CompletedPointEvents.Contains (KeyToCheck)) 
			{
				base.Execute ();
			}
		}
	}

	//Non-repeatable events
	public class CheckForFirstStartPointsEventCommand: BaseCheckFirstTimePointsEventCommand
	{
		public override string KeyToCheck {
			get {
				return BibaPointEvents.first_start;
			}
		}
	}

	public class CheckForFirstEquipmentInputPointsEventCommand: BaseCheckFirstTimePointsEventCommand
	{
		public override string KeyToCheck {
			get {
				return BibaPointEvents.first_equip_input;
			}
		}
	}

	public class CheckForFirstRoundCompletedPointsEventCommand: BaseCheckFirstTimePointsEventCommand
	{
		public override string KeyToCheck {
			get {
				return BibaPointEvents.first_round_completed;
			}
		}
	}

	public class CheckForFirstGameCompletedPointsEventCommand: BaseCheckFirstTimePointsEventCommand
	{
		public override string KeyToCheck {
			get {
				return BibaPointEvents.first_game_completed;
			}
		}
	}
	
	public class CheckForFirstScanCompletedPointsEventCommand: BaseCheckFirstTimePointsEventCommand
	{
		public override string KeyToCheck {
			get {
				return BibaPointEvents.first_successful_scan;
			}
		}
	}

	//Repeatable events
	public class CheckForGameCompletedPointsEventCommand: BaseCheckForPointsEventCommand
	{
		public override string KeyToCheck {
			get {
				return BibaPointEvents.game_completed;
			}
		}
	}

	public class CheckForRoundCompletedPointsEventCommand: BaseCheckForPointsEventCommand
	{
		public override string KeyToCheck {
			get {
				return BibaPointEvents.round_completed;
			}
		}
	}

	public class CheckForScanCompletedPointsEventCommand: BaseCheckForPointsEventCommand
	{
		public override string KeyToCheck {
			get {
				return BibaPointEvents.scan_completed;
			}
		}
	}

	public class CheckForSuccessfulPairedPointsEventCommand: BaseCheckForPointsEventCommand
	{
		public override string KeyToCheck {
			get {
				return BibaPointEvents.successfully_paired;
			}
		}
	}
}