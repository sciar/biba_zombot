using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
	public class StopSFXLoopsCommand : Command
	{
		[Inject]
		public AudioServices AudioServices { get; set; }

		public override void Execute ()
		{
			AudioServices.StopAllSFXLoops();
		}
	}
}