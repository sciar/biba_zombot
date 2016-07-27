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

		public override void Execute ()
		{
			TagObject.GetComponent<Animator>().SetTrigger(MenuStateTrigger.Reset);
		}
	}
}