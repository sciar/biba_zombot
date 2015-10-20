using System.Collections.Generic;
using System.Text;
using System.Linq;
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
            BibaGameModel.SelectedEquipments.Clear();

            foreach (var equipType in EquipmentSelected)
            {
                BibaGameModel.SelectedEquipments.Add(new BibaEquipment(equipType));
                BibaGameModel.TotalPlayedEquipments.Find(equip => equip.EquipmentType == equipType).TimeSelected++;
                BibaAnalyticService.TrackEquipmentSelected(equipType);
            }

            PrintDebugInfo();
            DataService.WriteGameModel();

            foreach (var equip in BibaGameModel.TotalPlayedEquipments)
            {
                Debug.Log(equip.EquipmentType.ToString() + " " + equip.TimeSelected.ToString());
            }
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

