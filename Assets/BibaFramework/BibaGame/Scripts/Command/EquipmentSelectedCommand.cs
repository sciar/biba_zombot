using System.Collections.Generic;
using System.Text;
using UnityEngine;
using BibaFramework.BibaAnalytic;
using strange.extensions.command.impl;

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

        [Inject]
        public IDataService DataService { get; set; }

        public override void Execute ()
        {
            BibaGameModel.Equipments.Clear();

            foreach (var equipType in EquipmentSelected)
            {
                BibaGameModel.Equipments.Add(new BibaEquipment(equipType));
                BibaAnalyticService.TrackEquipmentSelected(equipType);
            }

            PrintDebugInfo();
            DataService.WriteGameModel();
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

