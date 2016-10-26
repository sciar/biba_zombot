using System.Collections.Generic;
using System;

namespace BibaFramework.BibaGame
{
	[Serializable]
	public class BibaAccount : IResetModel
	{
		public string Id;
		public string EmailAddress;
		public string Password;

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

		public List<BibaProfile> BibaProfiles;

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