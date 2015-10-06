using strange.extensions.command.impl;
using UnityEngine;
using System.Collections.Generic;
using BibaFramework.BibaAnalytic;
using System.Text;

namespace BibaFramework.BibaGame
{
    public class EquipmentSelectedCommand : Command
    {
        [Inject]
        public List<BibaEquipmentType> EquipmentSelected { get; set; }

        [Inject]
        public BibaGameModel BibaGameModel { get; set; }

        [Inject]
        public IBibaAnalyticService BibaAnalyticService { get; set; }

        public override void Execute ()
        {
            foreach (var equipType in EquipmentSelected)
            {
                BibaGameModel.Equipments.Add(new BibaEquipment(equipType));
                BibaAnalyticService.TrackEquipmentSelected(equipType);
            }

            PrintDebugInfo();
        }

        void PrintDebugInfo()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Selected Equipment: ");
            
            foreach (var equipType in EquipmentSelected)
            {
                stringBuilder.Append(string.Format("{0}, ", equipType.ToString()));
            }
            
            Debug.Log(stringBuilder.ToString());
        }
    }
}

