using System;
using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
	public class CheckForPointsEventCommand : Command
	{
		[Inject]
		public string keyToCheck { get; set; }

		public virtual string KeyToCheck {
			get {
				return keyToCheck;
			}
		}

		[Inject]
		public PointEventService PointEventService { get; set; }

		public override void Execute ()
		{
			PointEventService.CheckAndCompletePointEvent (KeyToCheck);
		}
	}

	public class CheckForFirstStartPointsEventCommand: CheckForPointsEventCommand
	{
		public override string KeyToCheck {
			get {
				return BibaPointEvents.first_start;
			}
		}
	}

	public class CheckForFirstEquipmentInputPointsEventCommand: CheckForPointsEventCommand
	{
		public override string KeyToCheck {
			get {
				return BibaPointEvents.first_equip_input;
			}
		}
	}

	public class CheckForFirstRoundCompletedPointsEventCommand: CheckForPointsEventCommand
	{
		public override string KeyToCheck {
			get {
				return BibaPointEvents.first_round_completed;
			}
		}
	}

	public class CheckForFirstGameCompletedPointsEventCommand: CheckForPointsEventCommand
	{
		public override string KeyToCheck {
			get {
				return BibaPointEvents.first_game_completed;
			}
		}
	}

	public class CheckForGameCompletedPointsEventCommand: CheckForPointsEventCommand
	{
		public override string KeyToCheck {
			get {
				return BibaPointEvents.game_completed;
			}
		}
	}

	public class CheckForRoundCompletedPointsEventCommand: CheckForPointsEventCommand
	{
		public override string KeyToCheck {
			get {
				return BibaPointEvents.round_completed;
			}
		}
	}

	public class CheckForFirstScanCompletedPointsEventCommand: CheckForPointsEventCommand
	{
		public override string KeyToCheck {
			get {
				return BibaPointEvents.first_successful_scan;
			}
		}
	}

	public class CheckForScanCompletedPointsEventCommand: CheckForPointsEventCommand
	{
		public override string KeyToCheck {
			get {
				return BibaPointEvents.scan_completed;
			}
		}
	}

	public class CheckForSuccessfulPairedPointsEventCommand: CheckForPointsEventCommand
	{
		public override string KeyToCheck {
			get {
				return BibaPointEvents.successfully_paired;
			}
		}
	}
}