using System.Collections.Generic;
using System;
using LitJson;

namespace BibaFramework.BibaGame
{
    public class BibaSessionModel
    {
		public SessionInfo SessionInfo { get; set; }
        public bool TagScanned { get; set; }

		public override string ToString ()
    	{
    		return string.Format ("[BibaSessionModel: SessionInfo={0}, TagScanned={1}]", SessionInfo, TagScanned);
    	}
    }

	public class SessionInfo	
	{
		public string UUID { get; set; }
		public string DeviceModel { get; set; }
		public string DeviceOS { get; set; }

		public override string ToString ()
		{
			return string.Format ("[SessionInfo: UUID={0}, DeviceModel={1}, DeviceOS={2}]", UUID, DeviceModel, DeviceOS);
		}
	}
}