using System;
using System.Linq;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
    public class BibaEquipment
    {
        public BibaEquipmentType EquipmentType { get; private set; }
        public BibaTagType TagType { get { return TagMapDict[EquipmentType]; } }

        public List<DateTime> TimesPlayed { get; set; }
        public int NumberOfTimePlayed { get { return TimesPlayed.Count; } }

        public int NumberOfTimeSelected { get; set; }

        public BibaEquipment()
        {
            TimesPlayed = new List<DateTime>();
        }

        public BibaEquipment(BibaEquipmentType equipmentType)
        {
            EquipmentType = equipmentType;
            TimesPlayed = new List<DateTime>();
        }

        public void Play()
        {
            TimesPlayed.Add(DateTime.UtcNow);
        }

        public override string ToString ()
        {
            return string.Format ("[BibaEquipment: EquipmentType={0} TagType={1}]", EquipmentType, TagType);
        }

        public new bool Equals (object obj)
        {
            return ((BibaEquipment)obj).EquipmentType == this.EquipmentType;
        }

        private static readonly Dictionary<BibaEquipmentType, BibaTagType> TagMapDict = new Dictionary<BibaEquipmentType, BibaTagType>()
        {
            {  BibaEquipmentType.bridge, BibaTagType.orange },
            {  BibaEquipmentType.climber, BibaTagType.red },
            {  BibaEquipmentType.overhang, BibaTagType.purple },
            {  BibaEquipmentType.slide, BibaTagType.yellow },
            {  BibaEquipmentType.swing, BibaTagType.green },
            {  BibaEquipmentType.tube, BibaTagType.blue },
        };
    }

    public enum BibaEquipmentType
    {
        bridge,
        climber,
        overhang,
        slide,
        swing,
        tube,
    }
}