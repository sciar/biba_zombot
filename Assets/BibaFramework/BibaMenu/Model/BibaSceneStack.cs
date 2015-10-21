using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    public class BibaSceneStack : Stack<BaseMenuState>
    {
        private Dictionary<BaseMenuState, GameObject> _stateGameObjectDictionary = new Dictionary<BaseMenuState, GameObject>();

        public void LinkMenuStateWithGameObject(BaseMenuState menuState, GameObject go)
        {
            _stateGameObjectDictionary[menuState] = go;
        }

        public GameObject GetTopGameObjectForTopMenuState()
        {
            return _stateGameObjectDictionary[this.Peek()];
        }

        public GameObject RemoveMenuStateGameObject(BaseMenuState menuState)
        {
            var go = _stateGameObjectDictionary [menuState];
            _stateGameObjectDictionary.Remove(menuState);
            return go;
        }

		public override string ToString ()
    	{
			var sb = new StringBuilder();
			sb.Append("BibaSceneStack: ");
			foreach(var state in this)
			{
				sb.AppendLine("MenuState:" + state.SceneName + " GameObject:" + _stateGameObjectDictionary[state].name);
			}
    		return sb.ToString();
    	}
    }
}