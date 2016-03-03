namespace BibaFramework.BibaNetwork
{
	public interface ICDNService
    {
        void DownloadFileFromCDN(string fileName);
        void DownloadFilesFromCDN();
        bool ShouldDownloadOptionalFile(string fileName);
	}
}