using UnityEngine;
using System.Collections;

public static class ChemaDebug {

	public static bool debugMessagesEnabled = false;

	//Debug Methods
	public static void Log(string title, string message, string color = "red") {
		if (debugMessagesEnabled) {
			Debug.Log ("<color=" + color + ">[" + title + "]</color> " + message);
		}
	}

	/*public static void Log(string title, string message) {
		Debug.Log ("<color=red>"+title+"</color> "+message);
	}*/
}
