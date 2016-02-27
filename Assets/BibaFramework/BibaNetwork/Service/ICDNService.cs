using System;

namespace BibaFramework.BibaNetwork
{
	public interface ICDNService
    {
        bool ShouldLoadFromResources { get; }
        void UpdateFromCDN();
	}
}