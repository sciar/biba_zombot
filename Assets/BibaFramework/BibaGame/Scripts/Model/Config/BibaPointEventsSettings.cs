using System;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
	public class BibaPointEventSettings
	{
		public List<BibaPointEvent> BibaPointSettings = new List<BibaPointEvent>();
	}

	public class BibaPointEvent
	{
		public string Id;
		public int Points;
		public bool Repeat;
	}

	public class BibaLMVPointEvent : BibaPointEvent
	{
		public LMVScoreType LMVScoreType;
		public int ScoreRequired;
	}
}