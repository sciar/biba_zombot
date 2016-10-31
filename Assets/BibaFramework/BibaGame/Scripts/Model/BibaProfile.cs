using System;
using UnityEngine;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
	[Serializable]
	public class BibaProfile : IResetModel
	{
		public string Id;
		public string Nickname;
		public byte[] Avatar;
		public Gender Gender;
		public DateTime Birthday;
		public int Points;
		public List<string> CompletedPointEvents;

		public BibaProfileSession BibaProfileSession { get; set; }

		public BibaProfile()
		{
			Reset ();
		}

		public void Reset()
		{
			Id = Guid.NewGuid().ToString ();
			Nickname = string.Empty;
			Gender = Gender.na;

			Points = 0;
			CompletedPointEvents = new List<string> ();
			BibaProfileSession = new BibaProfileSession ();
		}
	}

	[Serializable]
	public enum Gender
	{
		male,
		female,
		na
	}
}