using System.Collections.Generic;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaAnalytic
{
	public interface IBibaAnalyticService
	{
		//Track the start of an app session, and other relevant informations
		void TrackStartSession();
		//Track the end of an app session, session length, the last game scene the user is on
		void TrackEndSession();
		//Track tag scanned
		void TrackTagScanned();
	}
}