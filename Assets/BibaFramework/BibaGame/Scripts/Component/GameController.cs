using UnityEngine;

namespace BibaFramework.BibaGame
{
	public class GameController : MonoBehaviour 
	{
		public BibaAccount BibaAccount { get; set; }
		public BibaSession BibaSession { get; set; }
		public IDataService DataService { get; set; }
	}
}