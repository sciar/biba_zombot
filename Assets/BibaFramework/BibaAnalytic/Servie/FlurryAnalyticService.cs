using Analytics;

namespace BibaFramework.BibaAnalytic
{
    public class FlurryAnalyticService : IBibaAnalyticService
    {
        private Flurry _service;

        public FlurryAnalyticService(string iosKey, string androidKey)
        {
            _service = Flurry.Instance;
            _service.SetLogLevel(LogLevel.All);
            _service.StartSession(iosKey, androidKey);

            TrackStartGame(new System.Collections.Generic.Dictionary<string, string>());
        }

        ~FlurryAnalyticService()
        {
            TrackEndRound(new System.Collections.Generic.Dictionary<string, string>());
        }

        public void TrackStartGame (System.Collections.Generic.Dictionary<string, string> parameters)
        {
            //Automatically implemented by Flurry
        }

        public void TrackEndGame (System.Collections.Generic.Dictionary<string, string> parameters, string lastScene)
        {
            //Automatically implemented by Flurry
        }

        public void TrackStartRound (System.Collections.Generic.Dictionary<string, string> parameters)
        {
            throw new System.NotImplementedException ();
        }

        public void TrackEndRound (System.Collections.Generic.Dictionary<string, string> parameters)
        {
            throw new System.NotImplementedException ();
        }

        public void TrackSatelliteTagScanned (System.Collections.Generic.Dictionary<string, string> parameters)
        {
            throw new System.NotImplementedException ();
        }
    }
}

