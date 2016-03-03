using strange.extensions.command.impl;

namespace BibaFramework.BibaNetwork
{
    public class UpdateFromCDNCommand : Command
    {
        [Inject]
        public ICDNService CDNService { get; set; }

        public override void Execute ()
        {
            CDNService.DownloadFilesFromCDN();
        }
    }
}