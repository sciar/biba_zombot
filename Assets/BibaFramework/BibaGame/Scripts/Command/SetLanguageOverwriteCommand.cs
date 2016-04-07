using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
	public class SetLanguageOverwriteCommand : Command
    {
        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
		public SystemLanguage SystemLanguage { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

		[Inject]
		public LanguageUpdatedSignal LanguageUpdatedSignal { get; set; }

        public override void Execute ()
        {
			BibaGameModel.LanguageOverwrite = SystemLanguage;
			DataService.WriteGameModel ();
			LanguageUpdatedSignal.Dispatch();
		}
    }
}