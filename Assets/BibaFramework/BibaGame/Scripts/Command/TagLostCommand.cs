using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
	public class TagLostCommand : Command
	{
		[Inject]
		public BibaTagType TagLost { get; set; }

		[Inject]
		public GameObject TagObject { get; set; }

		[Inject]
		public BibaDeviceSession BibaDeviceSession { get; set; }

		public override void Execute ()
		{
			if (TagLost != BibaDeviceSession.TagToScan) 
			{
				TagObject.GetComponent<Animator>().SetTrigger(MenuStateTrigger.Reset);
			}
		}
	}
}