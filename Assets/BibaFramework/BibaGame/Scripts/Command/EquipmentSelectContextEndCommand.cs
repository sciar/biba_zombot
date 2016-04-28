using strange.extensions.command.impl;
using BibaFramework.BibaAnalytic;
using System.Text;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class EquipmentSelectContextEndCommand : Command
    {
        [Inject]
		public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public IAnalyticService AnalyticService { get; set; }

        public override void Execute ()
        {
			foreach (var equip in BibaGameModel.SelectedEquipments)
            {
                AnalyticService.TrackEquipmentSelected(equip.EquipmentType);
            }

            PrintDebugInfo();
        }

        void PrintDebugInfo()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Selected Equipment: ");
            
			foreach (var equipType in BibaGameModel.SelectedEquipments)
            {
                stringBuilder.Append(string.Format("{0}, ", equipType.ToString()));
            }
            
            Debug.Log(stringBuilder.ToString());
        }
    }
}