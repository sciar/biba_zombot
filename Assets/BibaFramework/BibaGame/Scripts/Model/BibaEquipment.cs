using System;
using System.Linq;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
	[Serializable]
    public class BibaEquipment
    {
		public BibaEquipmentType EquipmentType;
        public BibaTagType TagType { get { return TagMapDict[EquipmentType]; } }

		public List<DateTime> TimesPlayed;
        public int NumberOfTimePlayed { get { return TimesPlayed.Count; } }

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

	[Serializable]
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