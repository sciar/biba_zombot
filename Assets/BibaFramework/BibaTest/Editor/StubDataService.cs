using System;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaTest
{
	public class StubDataService : IDataService
	{
		public void WriteToDisk<T> (T objectToWrite, string path)
		{
			
		}

		public T ReadFromDisk<T> (string path)
		{
			return default(T);
		}

		public void Save ()
		{
			
		}

		public BibaDevice LoadDeviceModel ()
		{
			return new BibaDevice ();
		}

		public BibaAccount LoadAccountModel ()
		{
			return new BibaAccount ();
		}
	}
}