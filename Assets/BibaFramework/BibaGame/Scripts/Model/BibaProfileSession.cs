using System;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
	public class BibaProfileSession
	{
		//LMV Scores = Light, Moderate, Vigorous Activity Scores
		public DateTime LScoreStart { get; set; }
		public DateTime MScoreStart { get; set; }
		public DateTime VScoreStart { get; set; }

		public float SessionLScore { get; set; }
		public float SessionMScore { get; set; }
		public float SessionVScore { get; set; }

		public List<BibaEquipment> SessionEquipments { get; set; }

		public BibaProfileSession ()
		{
			SessionEquipments = BibaGameConstants.DEFAULT_EQUIPENT_LIST;
		}
	}
}