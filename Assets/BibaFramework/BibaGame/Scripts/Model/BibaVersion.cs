using System;
using UnityEngine;
using LitJson;

namespace BibaFramework.BibaGame
{
	public class BibaVersion
	{
		[JsonIgnore]
		public string Version { get { return Application.version; } }

		public string BuildNumber { get; set; }
	}
}