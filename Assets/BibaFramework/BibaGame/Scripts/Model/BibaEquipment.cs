using BibaFramework.BibaTag;
using System;
using System.Linq;

namespace BibaFramework.BibaGame
{
    public class BibaEquipment
    {
        public BibaEquipmentType EquipmentType { get; private set; }
        public BibaTagType TagType { 
            get {
                if(Enum.IsDefined(typeof(BibaTagType), EquipmentType.ToString()))
                {
                    return ((BibaTagType)Enum.Parse(typeof(BibaTagType), EquipmentType.ToString()));
                }
                return BibaTagType.slide;
            }
        }
        public int Played { get; set; }

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

        public override bool Equals (object obj)
        {
            return ((BibaEquipment)obj).EquipmentType == this.EquipmentType;
        }
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