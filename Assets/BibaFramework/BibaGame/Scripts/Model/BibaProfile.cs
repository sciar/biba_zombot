using System;
using UnityEngine;
using System.Collections.Generic;
using LitJson;

namespace BibaFramework.BibaGame
{
	public class BibaProfile
	{
		public string Id { get; set; }
		public string Nickname { get; set; }
		public Gender Gender { get; set; }
		public DateTime Birthday { get; set; }
	
		public byte[] Avatar { get; set; }

		public int Points { get; set; }

		public List<string> CompletedPointEvents { get; set; }

		[JsonIgnore]
		public BibaProfileSession BibaProfileSession { get; set; }

		public BibaProfile()
		{
			Id = Guid.NewGuid().ToString ();
			CompletedPointEvents = new List<string> ();
			BibaProfileSession = new BibaProfileSession ();
		}
	}

	public enum Gender
	{
		male,
		female
	}
}