using UnityEngine;

namespace BibaFramework.BibaMenu
{
	public class ObjectMenuState : BaseMenuState 
	{
		public override string SceneName {
			get {
				return MenuStateGameObject.name;
			}
		}
        public GameObject MenuStateGameObject;   
	}
}