namespace BibaFramework.BibaGame
{
    public interface IBibaTagService 
    {
		TagFoundSignal TagFoundSignal { get; set; }
		TagLostSignal TagLostSignal { get; set; }
		TagInitFailedSignal TagInitFailedSignal { get; set; }

        void StartScan();
        void StopScan();
    }
}