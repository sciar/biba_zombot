using UnityEngine;
using System.Collections;

public class ComscoreCMFAnalytics : MonoBehaviour
{
    private static Coroutine waitForRequest;
    private int coldStartCount;
    private float startTime;

    private static bool initialised;
    private static bool requestSent;
    private static bool firstRequestSent;

    private string GetUrl() {
        return "http://b.scorecardresearch.com/p?c1=2&c2=14990625" +
               "&ns_site=cmf-fmc" +
               "&ns_ap_cs=" + coldStartCount +
               "&ns_ap_dft=" + PlayerPrefs.GetInt("LastDurationActive") +
               "&application_id=1314.21016.137260" +
               "&name=" + (firstRequestSent ? "startup" : "focus") +
               "&c8=biba_mobile";
    }

    void Awake() {
        if (initialised) return;
        initialised = true;
        startTime = Time.unscaledTime;
        DontDestroyOnLoad(gameObject);
        coldStartCount = PlayerPrefs.GetInt("ColdStartCount") + 1;
        PlayerPrefs.SetInt("ColdStartCount", coldStartCount);
    }

    void Update() {
        if (waitForRequest != null) return;
        if (requestSent) return;
        requestSent = true;
        SendRequest();
    }

    void OnApplicationQuit() {
        PlayerPrefs.SetInt("LastDurationActive", Mathf.FloorToInt((Time.unscaledTime - startTime) * 1000));
    }

    void OnApplicationFocus(bool focusStatus) {
        if (!focusStatus) {
            PlayerPrefs.SetInt("LastDurationActive", Mathf.FloorToInt((Time.unscaledTime - startTime) * 1000));
        } else {
            startTime = Time.unscaledTime;
            requestSent = false;
        }
    }

    public void SendRequest() {
        if (waitForRequest != null) return;
        WWW www = new WWW(GetUrl());
        waitForRequest = StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www) {
        yield return www;
        if (www.error == null) {
            Debug.LogWarning("WWW Was sent to the CMF server for analytics!: " + www.text);
            firstRequestSent = true;
        } else {
			Debug.LogWarning("WWW Error: it did not make it to the CMF server " + www.error);
            yield return new WaitForSeconds(60);
            requestSent = false;
        }
        waitForRequest = null;
    }
}