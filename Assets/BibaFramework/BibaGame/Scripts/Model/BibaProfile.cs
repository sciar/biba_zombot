using System;
using UnityEngine;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
	public class BibaProfile
	{
		public string Id { get; set; }
		public string Nickname { get; set; }
		public Gender Gender { get; set; }
		public DateTime Birthday { get; set; }
	
		public byte[] PlayerIcon { get; set; }

		public int Points { get; set; }

		//LMV Scores: Seconds spent during Light, Moderate, Vigorous activities
		public float LScore { get; set; }
		public float MScore { get; set; }
		public float VScore { get; set; }

		public List<string> CompletedPointEvents { get; set; }

		public BibaProfile()
		{
			CompletedPointEvents = new List<string> ();
		}
	}

	public enum Gender
	{
		male,
		female
	}
}