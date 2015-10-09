using BibaFramework.BibaGame;

namespace BibaFramework.BibaTag
{
    public interface IBibaTagService 
    {
        TagScannedSignal TagScannedSignal { get; set; }

        void StartScan();
        void StopScan();
    }
}