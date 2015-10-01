using BibaFramework.BibaTag;
using System;
using System.Linq;

namespace BibaFramework.BibaGame
{
    public abstract class BibaEquipment
    {
        public BibaTagType EntryTag { get { return FindBibaTagType(BibaTagConstants.BIBA_ENTRY_TAG_PREFIX); } }
        public BibaTagType SatelliteTag { get { return FindBibaTagType(BibaTagConstants.BIBA_SATELLITE_TAG_PREFIX); } }

        BibaTagType FindBibaTagType(string enumPrefix)
        {
            return Enum.GetValues(typeof(BibaTagType)).Cast<BibaTagType>().ToList().Find(tagType => tagType.ToString().Equals(enumPrefix + this.GetType().Name, StringComparison.InvariantCultureIgnoreCase));
        }
    }

    public class Bridge : BibaEquipment
    {
    }
    public class Climber : BibaEquipment
    {
    }
    public class Overhang : BibaEquipment
    {
    }
    public class Slide : BibaEquipment
    {
    }
    public class Swing : BibaEquipment
    {
    }
    public class Tube : BibaEquipment
    {
    }
}