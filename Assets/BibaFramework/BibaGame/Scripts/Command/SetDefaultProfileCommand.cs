using System;
using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
	public class SetDefaultProfileCommand : Command
	{
		[Inject]
		public SetProfileSignal SetProfileSignal { get; set; }

		[Inject]
		public BibaAccount BibaAccount { get; set; }

		public override void Execute ()
		{
			SetProfileSignal.Dispatch (BibaAccount.BibaProfiles [0].Id);
		}
	}
}