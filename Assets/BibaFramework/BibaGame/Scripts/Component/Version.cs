using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;
using BibaFramework.BibaNetwork;

[RequireComponent(typeof(Text))]
public class Version : MonoBehaviour 
{
	void Start () 
	{
		GetComponent<Text> ().text = (Application.platform == RuntimePlatform.Android) ? PlayerSettings.Android.bundleVersionCode.ToString() : PlayerSettings.iOS.buildNumber + "." 
			+ BibaContentConstants.ENVIRONMENT.ToString().Substring(0, 3);
	}
}