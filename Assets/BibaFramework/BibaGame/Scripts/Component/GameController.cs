using UnityEngine;

namespace BibaFramework.BibaGame
{
	public class GameController : MonoBehaviour 
	{
		public BibaAccount BibaAccount { get; set; }
		public BibaDeviceSession BibaSession { get; set; }
		public IDataService DataService { get; set; }
	}
}