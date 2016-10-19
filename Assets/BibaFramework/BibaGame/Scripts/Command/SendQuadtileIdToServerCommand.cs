using strange.extensions.command.impl;
using BibaFramework.Utility;
using System.Collections;
using UnityEngine.Experimental.Networking;
using UnityEngine;

namespace BibaFramework.BibaGame
{
    public class SendQuadtileIdToServerCommand : Command
    {
        /*
         * The list of backend calls is located here https://github.com/Z2hMedia/biba_django_backend/blob/master/biba_backend/biba_backend/urls.py
         */
        #if UNITY_DEBUG
        private const string BACKEND_END_POINT = "https://staging.playbiba.com";
        #endif
        private const string BACKEND_API_KEY = "3LDTzzfeGQ49bt7bnjqpzaNE";
        private const string BACKEND_END_POINT = "https://api.playbiba.com";
        private const string BACKEND_REGION_QUADTILE_REQUEST = BACKEND_END_POINT + "/v1/" + BACKEND_API_KEY + "/region/quadtile/{0}";
        private const string QUADTILE_ID = "quadtile_id";

        [Inject]
		public BibaDeviceSession BibaSession { get; set; }

        public override void Execute ()
        {
          	#if !UNITY_EDITOR
			if (!string.IsNullOrEmpty(BibaSession.QuadTileId))
            {
				Retain();
				new Task (SendPutRequest (), true);
            }
          	#endif
        }

		IEnumerator SendPutRequest()
		{
			var jsonData = "{\"" + QUADTILE_ID + "\":\"" + BibaSession.QuadTileId + "\"}";

			var requestURI = string.Format(BACKEND_REGION_QUADTILE_REQUEST, BibaSession.QuadTileId);

			var www = UnityWebRequest.Put (requestURI, jsonData);

			yield return www.Send ();

			if (www.isError) 
			{
				Debug.LogWarning (www.error);
			}
			else 
			{
				Debug.Log (www.downloadHandler.text);
			}

			Release ();
		}
    }
}