using System;
using System.IO;
using System.Text;
using UnityEngine;
using BestHTTP;
using strange.extensions.command.impl;

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
        public BibaSessionModel BibaSessionModel { get; set; }

        public override void Execute ()
        {
            //TODO: extract the http request if more requests are needed
            if (BibaSessionModel.SessionInfo != null && !string.IsNullOrEmpty(BibaSessionModel.SessionInfo.QuadTileId))
            {
                var jsonData = "{\"" + QUADTILE_ID + "\":\"" + BibaSessionModel.SessionInfo.QuadTileId + "\"}";

                var requestURI = string.Format(BACKEND_REGION_QUADTILE_REQUEST, BibaSessionModel.SessionInfo.QuadTileId);
                var request = new HTTPRequest(new Uri(requestURI), HTTPMethods.Put, false, RequestCompleted);
                request.SetHeader("Content-Type","application/json");
                request.RawData = Encoding.ASCII.GetBytes(jsonData);

                #if UNITY_EDITOR
                request.UseAlternateSSL = true;
                #endif

                request.DisableRetry = false;
                request.Send();

                Retain();
            }
        }

        void RequestCompleted(HTTPRequest request, HTTPResponse response)
        {
            //TODO: do something with the response
            Release();
        }
    }
}