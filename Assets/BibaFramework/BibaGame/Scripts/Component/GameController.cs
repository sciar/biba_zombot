using UnityEngine;
using BibaFramework.BibaAnalytic;

namespace BibaFramework.BibaGame
{
	public class GameController : MonoBehaviour 
	{
		public BibaAccount BibaAccount { get; set; }
		public BibaProfile BibaProfile { get; set; }
		public BibaDevice BibaDevice { get; set; }
		public BibaDeviceSession BibaDeviceSession { get; set; }
		public IDataService DataService { get; set; }
        public LocalizationService LocalizationService { get; set; }
		public ToggleTrackLightActivitySignal ToggleTrackLightActivitySignal { get; set; }
		public ToggleTrackModerateActivitySignal ToggleTrackModerateActivitySignal { get; set; }
		public ToggleTrackVigorousActivitySignal ToggleTrackVigorousActivitySignal { get; set; }
		public EquipmentPlayedSignal EquipmentPlayedSignal { get; set; }

		public virtual void SetupController(){ }
	}
}