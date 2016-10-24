namespace BibaFramework.BibaGame
{
    public interface IDataService
    {
        void WriteToDisk<T>(T objectToWrite, string path);
        T ReadFromDisk<T>(string path);

		BibaDevice LoadDeviceModel();
		BibaAccount LoadAccountModel();

		void Save();
    }
}