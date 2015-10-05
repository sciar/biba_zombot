using System.Collections.Generic;

namespace BibaFramework.BibaAnalytic
{
    public interface IBibaAnalyticService
    {
        //Track the start of a game session, and other relevant informations
        void TrackStartGame(Dictionary<string, string> parameters);
        //Track the end of a game session, session length, the last game scene the user is on
        void TrackEndGame(Dictionary<string, string> parameters, string lastScene);
        //Track the start of a round and the equipment used in the round
        void TrackStartRound(Dictionary<string, string> parameters);
        //Track the end of a round and possibily the players' records
        void TrackEndRound(Dictionary<string, string> parameters);
        //Track when the satellite tag is scanned and recognized
        void TrackSatelliteTagScanned(Dictionary<string, string> parameters);
    }
}