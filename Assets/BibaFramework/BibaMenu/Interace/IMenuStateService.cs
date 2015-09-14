using System;
using System.Collections;

namespace BibaFramework.BibaMenu
{
    public interface IMenuStateService
	{
		IEnumerator PlayEntryAnimation();
		IEnumerator PlayExitAnimation();
	}
}