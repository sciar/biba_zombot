using BibaFramework.BibaNetwork;

namespace BibaFramework.BibaGame
{
	public interface IAchievementService : IContentUpdated
	{
		BibaGameModel BibaGameModel { get; set; }
		ILocalizationService LocalizationService { get; set; }

		string GetAchievementText (string achievementId);
		void CheckAndCompleteAchievements ();
	}
}