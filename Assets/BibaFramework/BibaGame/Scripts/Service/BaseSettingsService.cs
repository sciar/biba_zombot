using System.Collections.Generic;
using System.Linq;
using BibaFramework.BibaNetwork;

namespace BibaFramework.BibaGame
{
    public abstract class BaseSettingsService<T>: IContentUpdated
    {
        [Inject]
        public IDataService DataService { get; set; }

        public abstract string SettingsFileName { get; }

        protected T _settings;
        protected T Settings {
            get 
            {
                if(_settings == null)
                {
                    ReloadContent();
                }
                return _settings;
            }
        }
        #region - IContentUpdated
        public bool ShouldLoadFromResources 
        {
            get 
            {
                var persistedManifest = DataService.ReadFromDisk<BibaManifest>(BibaContentConstants.GetPersistedPath(BibaContentConstants.MANIFEST_FILENAME));
                if(persistedManifest == null)
                {
                    return true;
                }

                var persistedManifestLine = persistedManifest.Lines.Find(line => line.FileName == SettingsFileName);
                if(persistedManifestLine == null)
                {
                    return true;
                }

                var resourceManifest = DataService.ReadFromDisk<BibaManifest>(BibaContentConstants.GetResourceFilePath(BibaContentConstants.MANIFEST_FILENAME));
                var resourceManifestLine = resourceManifest.Lines.Find(line => line.FileName == SettingsFileName);
                if(resourceManifestLine == null)
                {
                    return true;
                }
				return persistedManifestLine.TimeStamp < resourceManifestLine.TimeStamp;
            }
        }

        public string ContentFilePath 
        {
            get 
            {
                return ShouldLoadFromResources ? BibaContentConstants.GetResourceFilePath(SettingsFileName) :
                    BibaContentConstants.GetPersistedPath(SettingsFileName);
            }
        }

        public abstract void ReloadContent();
        #endregion
    }
}