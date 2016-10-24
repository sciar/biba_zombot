using BibaFramework.BibaNetwork;

namespace BibaFramework.BibaGame
{
	public interface ILocalizationService : IContentUpdated
	{
		BibaGameModel BibaGameModel { get; set; }
		string GetText(string key);
	}
}