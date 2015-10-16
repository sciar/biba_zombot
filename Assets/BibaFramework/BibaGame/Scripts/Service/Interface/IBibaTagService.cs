namespace BibaFramework.BibaGame
{
    public interface IBibaTagService 
    {
        TagScannedSignal TagScannedSignal { get; set; }

        void StartScan();
        void StopScan();
    }
}