using strange.extensions.command.impl;
using BibaFramework.BibaAnalytic;
using System.Text;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class SendEquipmentSelectedTracking : Command
    {
        [Inject]
		public BibaSession BibaSession { get; set; }

        [Inject]
        public IAnalyticService AnalyticService { get; set; }

        public override void Execute ()
        {
			foreach (var equip in BibaSession.SelectedEquipments)
            {
                AnalyticService.TrackEquipmentSelected(equip.EquipmentType);
            }

            PrintDebugInfo();
        }

        void PrintDebugInfo()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Selected Equipment: ");
            
			foreach (var equipType in BibaSession.SelectedEquipments)
            {
                stringBuilder.Append(string.Format("{0}, ", equipType.ToString()));
            }
            
            Debug.Log(stringBuilder.ToString());
        }
    }
}