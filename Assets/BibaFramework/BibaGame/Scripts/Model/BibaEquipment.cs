using BibaFramework.BibaTag;
using System.Linq;
using System;

namespace BibaFramework.BibaGame
{
    public abstract class BibaEquipment
    {
        public BibaTagType EntryTag { get { return FindBibaTagType(BibaTagConstants.BIBA_ENTRY_TAG_PREFIX); } }
        public BibaTagType SatelliteTag { get { return FindBibaTagType(BibaTagConstants.BIBA_SATELLITE_TAG_PREFIX); } }

        BibaTagType FindBibaTagType(string enumPrefix)
        {
            return Enum.GetValues(typeof(BibaTagType)).Cast<BibaTagType>().ToList().Find(tagType => tagType.ToString().StartsWith(enumPrefix + this.GetType().Name, StringComparison.InvariantCultureIgnoreCase));
        }
    }

}