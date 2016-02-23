using System;
using System.IO;
using UnityEngine;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using BibaFramework.BibaGame;
using LitJson;

namespace BibaFramework.BibaNetwork
{
    public class BibaCDNService : ICDNService
    {
        [Inject]
        public IDataService DataService { get; set; }
       
        private AWSCredentials _credentials;
        private AWSCredentials Credentials
        {
            get
            {
                if (_credentials == null)
                {
                    _credentials = new CognitoAWSCredentials(BibaContentConstants.AWS_IDENTITY_POOL_ID, RegionEndpoint.USEast1);
                }
                return _credentials;
            }
        }
        
        private IAmazonS3 _s3Client;
        private IAmazonS3 Client
        {
            get
            {
                if (_s3Client == null)
                {
                    _s3Client = new AmazonS3Client(Credentials, RegionEndpoint.USEast1);
                }
                return _s3Client;
            }
        }
       
        private string S3BucketName { get { return BibaContentConstants.CI_GAME_ID; } }

        private BibaManifest _localManifest;
        private BibaManifest LocalManifest 
        {
            get 
            {
                if(_localManifest == null)
                {
                    var persistedManifest = DataService.ReadFromDisk<BibaManifest>(BibaContentConstants.GetPersistedContentFilePath(BibaContentConstants.MANIFEST_FILENAME));
                    var resourceManifest = DataService.ReadFromDisk<BibaManifest>(BibaContentConstants.GetResourceContentFilePath(BibaContentConstants.MANIFEST_FILENAME));
                    _localManifest = (persistedManifest == null || persistedManifest.Version < resourceManifest.Version) ? resourceManifest : persistedManifest;
                }
                return _localManifest;
            }
        }

        public void DownloadFromCDN()
        {
            GetObject(BibaContentConstants.GetContentRelativePath(BibaContentConstants.MANIFEST_FILENAME), ManifestRetrieved);
        }

        void ManifestRetrieved(string remoteManifestString)
        {
            if (string.IsNullOrEmpty(remoteManifestString))
            {
                return;
            }

            var remoteManifest = JsonMapper.ToObject<BibaManifest>(remoteManifestString);
            if (remoteManifest != null && remoteManifest.Version > LocalManifest.Version)
            {
                foreach(var remoteLine in remoteManifest.Lines)
                {
                    var remoteFileName = remoteLine.FileName;
                    var localLine = LocalManifest.Lines.Find(line => line.FileName == remoteFileName);

                    if((localLine == null || localLine.Version < remoteLine.Version) && !remoteLine.OptionalDownload)
                    {
                        GetObject(BibaContentConstants.GetContentRelativePath(remoteFileName), (dataString) => {
                            if(!string.IsNullOrEmpty(dataString))
                            {
                                File.WriteAllText(BibaContentConstants.GetPersistedContentFilePath(remoteFileName), dataString);
                            }
                        });
                    }
                }
                _localManifest  = remoteManifest;
                DataService.WriteToDisk<BibaManifest>(_localManifest, BibaContentConstants.GetPersistedContentFilePath(BibaContentConstants.MANIFEST_FILENAME));
            }
        }
        
        void GetObject(string objectFileName, Action<string> callBack)
        {
            Debug.Log(string.Format("fetching {0} from bucket {1}", objectFileName, S3BucketName));
            
            Client.GetObjectAsync(S3BucketName, objectFileName, (responseObj) => {
                string data = null;
                var response = responseObj.Response;
                if (response.ResponseStream != null)
                {
                    using (var reader = new StreamReader(response.ResponseStream))
                    {
                        data = reader.ReadToEnd();
                    }
                }
                callBack(data);
            });
        }
    }
}