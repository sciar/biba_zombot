using System.Collections.Generic;
using System;
using BibaFramework.BibaMenu;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaTag
{
    public interface IBibaTagService 
    {
        TagScannedSignal TagScannedSignal { get; set; }
        ToggleScanSignal ToggleScanSignal { get; set; }

        HashSet<BibaTagType> LastScannedTags { get; set; }

        void StartScan();
        void StopScan();
        void StartScanWithCompleteHandler(Func<int,bool> isCompleted, Action onCompleted);
    }
}