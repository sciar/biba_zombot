using System.Collections.Generic;
using System;
using BibaFramework.BibaMenu;

namespace BibaFramework.BibaTag
{
    public interface IBibaTagService 
    {
        TagScanningCompletedSignal TagScanningCompletedSignal { get; set; }
        HashSet<BibaTag> LastScannedTags { get; set; }
        void StartScanWithCompleteHandler();
    }
}