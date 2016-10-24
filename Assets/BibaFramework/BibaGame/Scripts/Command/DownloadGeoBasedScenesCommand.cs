using strange.extensions.command.impl;
using BibaFramework.BibaNetwork;

namespace BibaFramework.BibaGame
{
    public class DownloadGeoBasedScenesCommand : Command
    {
        [Inject]
        public SpecialSceneService SpecialSceneService { get; set; }

        [Inject]
        public ICDNService BibaCDNService { get; set; }

        public override void Execute ()
        {
            var scenesToDownload = SpecialSceneService.GetGeoBasedScenes();
            foreach (var sceneId in scenesToDownload)
            {
                BibaCDNService.DownloadFileFromCDN(sceneId + BibaContentConstants.UNITY3D_EXTENSION);
            }
        }
    }
}