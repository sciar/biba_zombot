using strange.extensions.command.impl;

namespace BibaFramework.BibaMenu
{
    public class LoadGameSceneCommand : Command 
    {
        [Inject]
        public BibaMenuState BibaMenuState { get; set; }

        [Inject]
        public BibaSceneModel BibaSceneModel { get; set; }

        public override void Execute ()
        {
            BibaSceneModel.LastMenuState = BibaSceneModel.NextMenuState;
            BibaSceneModel.NextMenuState = BibaMenuState;
        }
    }
}