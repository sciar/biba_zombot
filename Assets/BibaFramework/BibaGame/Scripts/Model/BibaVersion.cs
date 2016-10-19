using System;
using UnityEngine;

namespace BibaFramework.BibaGame
{
	[Serializable]
	public class BibaVersion
	{
		public string Version { get { return Application.version; } }

		public string BuildNumber;
	}
}