using strange.extensions.mediation.impl;

namespace BibaFramework.BibaMenu
{
    public class LoadingView : View
    {
        public void Enable(bool status)
        {
            gameObject.SetActive(status);
        }
    }
}