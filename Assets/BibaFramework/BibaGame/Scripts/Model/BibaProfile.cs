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

		public List<BibaEquipment> PlayedEquipments { get; set; }
		public List<string> CompletedPointEvents { get; set; }

		public BibaProfile()
		{
			PlayedEquipments = DEFAULT_EQUIPENT_LIST;
			CompletedPointEvents = new List<string> ();
		}

		private static List<BibaEquipment> DEFAULT_EQUIPENT_LIST 
		{
			get 
			{
				var defaults = new List<BibaEquipment> ();
				Array.ForEach((BibaEquipmentType[])Enum.GetValues(typeof(BibaEquipmentType)), eq => defaults.Add(new BibaEquipment(eq)));
				return defaults;
			}
		}
	}

	public enum Gender
	{
		male,
		female
	}
}