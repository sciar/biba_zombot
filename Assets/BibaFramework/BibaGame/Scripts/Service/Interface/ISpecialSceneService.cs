using BibaFramework.BibaNetwork;
using System.Collections.Generic;

namespace BibaFramework.BibaGame
{
	public interface ISpecialSceneService : IContentUpdated
	{
		BibaSessionModel BibaSessionModel { get; set; }
		List<string> GeoBasedScenes { get; }
	
		string GetNextSceneToShow (string nextScene);
	}
}