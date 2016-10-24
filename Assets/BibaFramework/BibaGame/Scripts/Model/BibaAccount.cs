﻿using System.Collections.Generic;
using LitJson;
using System;

namespace BibaFramework.BibaGame
{
	public class BibaAccount : IResetModel
	{
		public string Id { get; set; }
		public string EmailAddress { get; set; }
		public string Password { get; set; }

		[JsonIgnore]
		public int TotalPoints {
			get {
				var result = 0;
				foreach (var profile in BibaProfiles) 
				{
					result += profile.Points;
				}
				return result;
			}
		}

		public List<BibaProfile> BibaProfiles { get; set; }

		public BibaAccount()
		{
			Reset ();
		}

		public void Reset()
		{
			Id = Guid.NewGuid().ToString ();
			EmailAddress = string.Empty;
			Password = string.Empty;

			BibaProfiles = new List<BibaProfile> ();
			BibaProfiles.Add(new BibaProfile ());
		}
	}
}