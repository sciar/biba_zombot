using BibaFramework.BibaGame;

namespace BibaFramework.BibaData
{
    public interface IDataService
    {
        void WriteToDisk<T>(T objectToWrite, string path);
        T ReadFromDisk<T>(string path);      

        void WriteGameModel();
        BibaGameModel ReadGameModel();
    }
}