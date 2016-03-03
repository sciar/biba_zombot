using strange.extensions.command.impl;
using BibaFramework.BibaNetwork;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class CheckForDownloadContentCommand : Command
    {
        [Inject]
        public SpecialSceneService SpecialSceneService { get; set; }

        [Inject]
        public SetMenuStateTriggerSignal SetMenuStateTriggerSignal { get; set; }

        [Inject]
        public ICDNService CDNService { get; set; }

        public override void Execute ()
        {
            var geoScenes = SpecialSceneService.GetGeoBasedScenes();
            foreach (var sceneId in geoScenes)
            {
                if(CDNService.ShouldDownloadOptionalFile(sceneId + BibaContentConstants.UNITY3D_EXTENSION))
                {
                    SetMenuStateTriggerSignal.Dispatch(MenuStateTrigger.Download);
                    Fail();
                }
            }
        }
    }
}