namespace BibaFramework.BibaNetwork
{
    public interface IContentUpdated 
    {
        bool ShouldLoadFromResources { get; }
        string ContentFilePath { get; }
        void ReloadContent();
    }
}