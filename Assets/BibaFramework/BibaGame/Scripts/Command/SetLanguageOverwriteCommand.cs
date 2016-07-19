using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
	public class SetLanguageOverwriteCommand : Command
    {
        [Inject]
		public BibaDevice BibaDevice { get; set; }

        [Inject]
		public SystemLanguage SystemLanguage { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

		[Inject]
		public SystemUpdatedSignal LanguageUpdatedSignal { get; set; }

        public override void Execute ()
        {
			BibaDevice.LanguageOverwrite = SystemLanguage;
			DataService.Save ();
			LanguageUpdatedSignal.Dispatch();
		}
    }
}