using System;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
	[Serializable]
	public class BibaPointEventSettings
	{
		public List<BibaPointEvent> BibaPointSettings = new List<BibaPointEvent>();
	}

	[Serializable]
	public class BibaPointEvent
	{
		public string Id;
		public int Points;
		public bool Repeat;
	}

	[Serializable]
	public class BibaLMVPointEvent : BibaPointEvent
	{
		public LMVScoreType LMVScoreType;
		public int ScoreRequired;
	}
}