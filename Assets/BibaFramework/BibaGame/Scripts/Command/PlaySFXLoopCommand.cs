﻿using BibaFramework.BibaMenu;
using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
	public class PlaySFXLoopCommand : Command
	{
		[Inject]
		public string SFXName { get; set; }

		[Inject]
		public AudioServices AudioServices { get; set; }

		public override void Execute ()
		{
			if (SFXName == BibaSFX.None)
			{
				return;
			}

			AudioServices.StartSFXLoop(SFXName);
		}
	}
}