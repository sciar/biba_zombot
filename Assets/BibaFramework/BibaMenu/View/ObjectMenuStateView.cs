using strange.extensions.mediation.impl;

namespace BibaFramework.BibaMenu
{
	public class ObjectMenuStateView : View 
	{
		protected override void Start ()
		{
			base.Start();
			gameObject.SetActive(false);
		}
	} 
}