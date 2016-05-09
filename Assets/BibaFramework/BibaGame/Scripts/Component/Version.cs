using UnityEngine;
using UnityEngine.UI;
using BibaFramework.BibaNetwork;
using BibaFramework.BibaGame;

[RequireComponent(typeof(Text))]
public class Version : MonoBehaviour 
{
	void Start () 
	{
		if (BibaContentConstants.ENVIRONMENT == Environment.Development) 
		{
			var jsonService = new JSONDataService ();
			var version = jsonService.ReadFromDisk<BibaVersion> (BibaContentConstants.GetResourceFilePath (BibaContentConstants.BIBAVERSION_FILE));
			
			GetComponent<Text> ().text = version.Version + "." +
				version.BuildNumber + " " + 
				BibaContentConstants.ENVIRONMENT.ToString().Substring(0, 3);
		}
	}
}