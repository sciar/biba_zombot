namespace BibaFramework.BibaGame
{
    public interface IDataService
    {
        void WriteToDisk<T>(T objectToWrite, string path);
        T ReadFromDisk<T>(string path);

		BibaDevice LoadSystemModel();
		BibaAccount LoadAccountModel();

		void Save();
    }
}