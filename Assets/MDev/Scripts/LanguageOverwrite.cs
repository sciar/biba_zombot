using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LanguageOverwrite : MonoBehaviour {
	
    public void SetLanguage(string language)
    {
        if (language == "English")
        {
            //BibaDevice.LanguageOverwrite = SystemLanguage.English;
        }
        else if (language == "French")
        {
            //BibaDevice.LanguageOverwrite = SystemLanguage.French;
        }
        else if (language == "Spanish")
        {
            //BibaDevice.LanguageOverwrite = SystemLanguage.Spanish;
        }
    }
}
