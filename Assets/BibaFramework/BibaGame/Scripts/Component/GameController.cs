using UnityEngine;

namespace BibaFramework.BibaGame
{
	public class GameController : MonoBehaviour 
	{
		public BibaAccount BibaAccount { get; set; }
		public BibaProfile BibaProfile { get; set; }
		public BibaDevice BibaDevice { get; set; }
		public BibaDeviceSession BibaDeviceSession { get; set; }
		public IDataService DataService { get; set; }

		public virtual void SetupController(){ }
	}
}