using System;
using strange.extensions.command.impl;

namespace BibaFramework.BibaGame
{
	public class SetProfileCommand : Command
	{
		[Inject]
		public string PlayerId { get; set; }

		[Inject]
		public BibaAccount BibaAccount { get; set; }

		public override void Execute ()
		{
			var profile = BibaAccount.BibaProfiles.Find (player => player.Id == PlayerId);
			if (profile == null) 
			{
				profile = BibaAccount.BibaProfiles [0];
			}

			injectionBinder.Unbind<BibaProfile>();
			injectionBinder.Bind<BibaProfile>().To(profile).ToSingleton().CrossContext();
		}
	}
}