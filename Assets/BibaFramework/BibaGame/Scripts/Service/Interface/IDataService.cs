namespace BibaFramework.BibaGame
{
    public interface IDataService
    {
        void WriteToDisk<T>(T objectToWrite, string path);
        T ReadFromDisk<T>(string path);

		BibaSystem LoadSystemModel();
		BibaAccount LoadAccountModel();

		void Save();
    }
}