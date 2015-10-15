using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BibaFramework.BibaMenu
{
    public class BibaSceneStack : Stack<BaseMenuState>
    {
		public override string ToString ()
    	{
			var sb = new StringBuilder();
			sb.Append("BibaSceneStack: ");
			foreach(var state in this)
			{
				sb.Append(state.SceneName + ", ");
			}
    		return sb.ToString();
    	}
    }
}