using BibaFramework.BibaTag;
using System;
using System.Linq;

namespace BibaFramework.BibaGame
{
    public class BibaEquipment
    {
        public BibaEquipmentType EquipmentType { get; private set; }
        public int Played { get; set; }
        public BibaTagType EntryTag { get { return FindBibaTagType(BibaTagConstants.BIBA_ENTRY_TAG_PREFIX); } }
        public BibaTagType SatelliteTag { get { return FindBibaTagType(BibaTagConstants.BIBA_SATELLITE_TAG_PREFIX); } }

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
            return string.Format ("[BibaEquipment: EquipmentType={0}]", EquipmentType);
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