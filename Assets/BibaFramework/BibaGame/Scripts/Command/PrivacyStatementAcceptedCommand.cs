using strange.extensions.command.impl;
using BibaFramework.BibaData;

namespace BibaFramework.BibaGame
{
    public class PrivacyStatementAcceptedCommand : Command
    {
        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        public override void Execute ()
        {
            BibaGameModel.PrivacyPolicyAccepted = true;
            DataService.WriteGameModel();
        }
    }
}