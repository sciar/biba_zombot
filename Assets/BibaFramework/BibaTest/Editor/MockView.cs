using System;
using strange.extensions.mediation.api;

namespace BibaFramework.BibaTest
{
	public class MockView : IView 
	{
		public bool requiresContext { get; set; }
		public bool registeredWithContext { get; set; }
		public bool autoRegisterWithContext{ get { return true; } }
	}
}