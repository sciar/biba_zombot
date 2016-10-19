using System;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
	public class BibaProfileSession
	{
		public List<BibaEquipment> SessionEquipments { get; set; }

		public Dictionary<LMVScoreType, LMVSession> LMVSessionDict { get; set; }
		public List<string> CompletedLMVScoreEvents { get; set; }

		public BibaProfileSession ()
		{
			SessionEquipments = BibaGameConstants.DEFAULT_EQUIPENT_LIST;

			LMVSessionDict = new Dictionary<LMVScoreType, LMVSession> () {
				{ LMVScoreType.light_score, new LMVSession () },
				{ LMVScoreType.moderate_score, new LMVSession () },
				{ LMVScoreType.vigorous_score, new LMVSession () }
			};
			CompletedLMVScoreEvents = new List<string> ();
		}
	}
		
	public class LMVSession
	{
		public DateTime DateStart { get; set; }
		public double SessionScore { get; set; }
	}
		
	public enum LMVScoreType
	{
		light_score,
		moderate_score,
		vigorous_score
	}
}