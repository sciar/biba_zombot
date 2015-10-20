using System;
using System.Linq;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
    public class BibaEquipment
    {
        public BibaEquipmentType EquipmentType { get; private set; }
        public BibaTagType TagType { get { return TagMapDict[EquipmentType]; } }

        public int TimeSelected { get; set; }
        public int TimePlayed { get; set; }

        public BibaEquipment()
        {
        }

        public BibaEquipment(BibaEquipmentType equipmentType)
        {
            EquipmentType = equipmentType;
        }

        BibaTagType FindBibaTagType(string enumPrefix)
        {
            return Enum.GetValues(typeof(BibaTagType)).Cast<BibaTagType>().ToList().Find(tagType => tagType.ToString().Equals(enumPrefix + this.GetType().Name, StringComparison.InvariantCultureIgnoreCase));
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
            {  BibaEquipmentType.tube, BibaTagType.blue},
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