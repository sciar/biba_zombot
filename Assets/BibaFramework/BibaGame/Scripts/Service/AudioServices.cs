using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace BibaFramework.BibaGame
{
	public class AudioServices : MonoBehaviour
	{
		public Dictionary<string, AudioClip> dict_SoundEffects;
		public Dictionary<string, AudioClip> dict_BackgroundMusic;
		
		private GameObject go_sfxPrefab;
		private GameObject go_lsfxPrefab;
		private GameObject go_bgmPrefab;
		
		private GameObject go_bgmSource;
		private string s_bgmPlaying;
		private Dictionary<string, GameObject> dict_go_activeLoopingSFX;
		
		private bool b_sfxIntroPlaying = false;
		private bool b_sfxIntroInterrupted = false;
		private string s_pendingOutro = "";
		
		void Awake() {
			go_sfxPrefab = Resources.Load ("Audio/Prefabs/SFXPrefab") as GameObject;
			go_lsfxPrefab = Resources.Load ("Audio/Prefabs/LSFXPrefab") as GameObject;
			go_bgmPrefab = Resources.Load ("Audio/Prefabs/BGMPrefab") as GameObject;
			
			// Populate Resources/Audio/SFX into a Dictionary
			dict_SoundEffects = new Dictionary<string, AudioClip> ();
			UnityEngine.Object[] objectsInSFXFolder = Resources.LoadAll ("Audio/SFX");
			foreach (UnityEngine.Object obj in objectsInSFXFolder) {
				AudioClip clip = obj as AudioClip;
				dict_SoundEffects.Add(obj.name, clip);
			}
			
			// Populate Resources/Audio/BGM into a Dictionary
			dict_BackgroundMusic = new Dictionary<string, AudioClip> ();
			UnityEngine.Object[] objectsInBGMFolder = Resources.LoadAll ("Audio/BGM");
			foreach (UnityEngine.Object obj in objectsInBGMFolder) {
				AudioClip clip = obj as AudioClip;
				dict_BackgroundMusic.Add(obj.name, clip);
			}
			
			dict_go_activeLoopingSFX = new Dictionary<string, GameObject> ();
		}
		
		public void PlaySFX(AudioClip ac_clip) {
			GameObject newSFX = Instantiate (go_sfxPrefab, Camera.main.transform.position, Quaternion.identity) as GameObject;
			newSFX.transform.parent = gameObject.transform;
			newSFX.GetComponent<AudioSource> ().clip = ac_clip;
			newSFX.GetComponent<AudioSource>().Play();
		}
		
		public void PlaySFX(string s_sfxName) {
			if (dict_SoundEffects.ContainsKey(s_sfxName)) {
				AudioClip clip = dict_SoundEffects [s_sfxName];
				GameObject newSFX = Instantiate (go_sfxPrefab, Camera.main.transform.position, Quaternion.identity) as GameObject;
				newSFX.transform.parent = gameObject.transform;
				newSFX.GetComponent<AudioSource> ().clip = clip;
				newSFX.GetComponent<AudioSource>().Play();
			} else {
				Debug.LogWarning("AudioClip with name: '"+s_sfxName+"' doesn't exist.");
			}
		}
		
		public void PlaySFX(string s_sfxName, float f_pitch) {
			if (dict_SoundEffects.ContainsKey(s_sfxName)) {
				AudioClip clip = dict_SoundEffects [s_sfxName];
				GameObject newSFX = Instantiate (go_sfxPrefab, Camera.main.transform.position, Quaternion.identity) as GameObject;
				newSFX.transform.parent = gameObject.transform;
				newSFX.GetComponent<AudioSource> ().clip = clip;
				newSFX.GetComponent<AudioSource> ().pitch = f_pitch;
				newSFX.GetComponent<AudioSource>().Play();
			} else {
				Debug.LogWarning("AudioClip with name: '"+s_sfxName+"' doesn't exist.");
			}
		}
		
		public IEnumerator PlaySFX(string s_sfxName, Action<string> callback, string s_nextSfxName) {
			if (dict_SoundEffects.ContainsKey(s_sfxName)) {
				AudioClip clip = dict_SoundEffects [s_sfxName];
				GameObject newSFX = Instantiate (go_sfxPrefab, Camera.main.transform.position, Quaternion.identity) as GameObject;
				newSFX.transform.parent = gameObject.transform;
				newSFX.GetComponent<AudioSource> ().clip = clip;
				newSFX.GetComponent<AudioSource>().Play();
				yield return new WaitForSeconds(clip.length-0.1f);
				b_sfxIntroPlaying = false;
				if (!b_sfxIntroInterrupted) {
					callback(s_nextSfxName);
				} else {
					b_sfxIntroInterrupted = false;
					if (s_pendingOutro != "") {
						PlaySFX(s_pendingOutro);
						s_pendingOutro = "";
					}
				}
			} else {
				Debug.LogWarning("AudioClip with name: '"+s_sfxName+"' doesn't exist.");
			}
		}
		
		public void StartSFXLoop(string s_sfxNameLoop) {
			if (dict_SoundEffects.ContainsKey(s_sfxNameLoop)) {
				AudioClip clip = dict_SoundEffects [s_sfxNameLoop];
				GameObject newSFX = Instantiate (go_lsfxPrefab, Camera.main.transform.position, Quaternion.identity) as GameObject;
				newSFX.transform.parent = gameObject.transform;
				newSFX.GetComponent<AudioSource> ().clip = clip;
				newSFX.GetComponent<AudioSource>().Play();
				dict_go_activeLoopingSFX.Add(s_sfxNameLoop, newSFX);
			} else {
				//Debug.LogWarning("AudioClip with name: '"+s_sfxNameLoop+"' doesn't exist.");
			}
		}
		
		public void StartSFXLoopWithIntro(string s_sfxNameLoop, string s_sfxNameIntro) {
			b_sfxIntroPlaying = true;
			StartCoroutine(PlaySFX (s_sfxNameIntro, StartSFXLoop, s_sfxNameLoop));
		}
		
		public void EndSFXLoop(string s_sfxNameLoop) {
			if (dict_go_activeLoopingSFX.ContainsKey(s_sfxNameLoop)) {
				GameObject sfxObject = dict_go_activeLoopingSFX[s_sfxNameLoop];
				AudioSource source = sfxObject.GetComponent<AudioSource>();
				dict_go_activeLoopingSFX.Remove(s_sfxNameLoop);
				StartCoroutine(FadeOutAndDestroy(source, 1f));
			} else {
				if (b_sfxIntroPlaying) {
					b_sfxIntroInterrupted = true;
				}
				//Debug.LogWarning("AudioClip with name: '"+s_sfxNameLoop+"' doesn't exist.");
			}
		}
		
		public void EndSFXLoopWithOutro(string s_sfxNameLoop, string s_sfxNameOutro) {
			if (dict_go_activeLoopingSFX.ContainsKey(s_sfxNameLoop)) {
				GameObject sfxObject = dict_go_activeLoopingSFX[s_sfxNameLoop];
				AudioSource source = sfxObject.GetComponent<AudioSource>();
				dict_go_activeLoopingSFX.Remove(s_sfxNameLoop);
				StartCoroutine(FadeOutAndDestroy(source, 0.2f, s_sfxNameOutro));
			} else {
				if (b_sfxIntroPlaying) {
					b_sfxIntroInterrupted = true;
					s_pendingOutro = s_sfxNameOutro;
				}
				//Debug.LogWarning("AudioClip with name: '"+s_sfxNameLoop+"' doesn't exist.");
			}
		}
		
		public void PlayBGM(string s_bgmName) {
			if (dict_BackgroundMusic.ContainsKey(s_bgmName) && s_bgmPlaying != s_bgmName) {
				AudioClip clip = dict_BackgroundMusic [s_bgmName];
				s_bgmPlaying = s_bgmName;
				if (go_bgmSource == null) {
					GameObject newBGM = Instantiate(go_bgmPrefab, Camera.main.transform.position, Quaternion.identity) as GameObject;
					newBGM.transform.parent = gameObject.transform;
					newBGM.GetComponent<AudioSource>().clip = clip;
					newBGM.GetComponent<AudioSource>().Play();
					go_bgmSource = newBGM;
				} else {
					AudioSource source = go_bgmSource.GetComponent<AudioSource>();
					if (source.isPlaying) {
						StartCoroutine(FadeOutAndPlay(source, clip, 1f));
					}
				}
			}
		}
		
		public void PlayBGMWithIntro(string s_bgmName, string s_introName) {
			if (dict_BackgroundMusic.ContainsKey (s_bgmName) && dict_BackgroundMusic.ContainsKey (s_introName) && s_bgmPlaying != s_bgmName) {
				AudioClip clip = dict_BackgroundMusic [s_bgmName];
				AudioClip intro = dict_BackgroundMusic [s_introName];
				s_bgmPlaying = s_bgmName;
				if (go_bgmSource == null) {
					GameObject newBGM = Instantiate (go_bgmPrefab, Camera.main.transform.position, Quaternion.identity) as GameObject;
					newBGM.transform.parent = gameObject.transform;
					AudioSource source = newBGM.GetComponent<AudioSource> ();
					StartCoroutine (PlayIntroThenLoop (source, intro, clip));
					go_bgmSource = newBGM;
				} else {
					AudioSource source = go_bgmSource.GetComponent<AudioSource> ();
					//					if (source.isPlaying) {
					//						StartCoroutine (FadeOutAndPlayWithIntro (source, intro, clip, 1f));
					//					}
					StartCoroutine (FadeOutAndPlayWithIntro (source, intro, clip, 1f));
				}
			} else {
				if (!dict_BackgroundMusic.ContainsKey (s_bgmName)) {
					Debug.LogWarning("AudioClip with name: '"+s_bgmName+"' doesn't exist.");
				}
				if (!dict_BackgroundMusic.ContainsKey (s_introName)) {
					Debug.LogWarning("AudioClip with name: '"+s_introName+"' doesn't exist.");
				}
			}
		}
		
		public void PlayBGMIntroOnly(string s_introName) {
			if (dict_BackgroundMusic.ContainsKey (s_introName)) {
				AudioClip intro = dict_BackgroundMusic [s_introName];
				s_bgmPlaying = null;
				if (go_bgmSource == null) {
					GameObject newBGM = Instantiate (go_bgmPrefab, Camera.main.transform.position, Quaternion.identity) as GameObject;
					newBGM.transform.parent = gameObject.transform;
					AudioSource source = newBGM.GetComponent<AudioSource> ();
					StartCoroutine (PlayIntro (source, intro));
					go_bgmSource = newBGM;
				} else {
					AudioSource source = go_bgmSource.GetComponent<AudioSource> ();
					if (source.isPlaying) {
						StartCoroutine (FadeOutAndPlayIntro (source, intro, 1f));
					}
				}
			}
		}
		
		IEnumerator PlayIntroThenLoop(AudioSource source, AudioClip intro, AudioClip newClip) {
			PlaySFX (intro);
			yield return new WaitForSeconds (intro.length-0.1f);
			source.clip = newClip;
			source.Play ();
		}
		
		IEnumerator PlayIntro(AudioSource source, AudioClip intro) {
			PlaySFX (intro);
			yield return new WaitForSeconds (intro.length-0.1f);
		}
		
		IEnumerator FadeOutAndPlayWithIntro(AudioSource source, AudioClip intro, AudioClip newClip, float fadeLength) {
			for (float f = 1f; f >= 0f; f -= Time.deltaTime/fadeLength) {
				source.volume = f;
				yield return null;
			}
			source.Stop ();
			source.volume = 1f;
			yield return StartCoroutine(PlayIntroThenLoop (source, intro, newClip));
		}
		
		IEnumerator FadeOutAndPlayIntro(AudioSource source, AudioClip intro, float fadeLength) {
			for (float f = 1f; f >= 0f; f -= Time.deltaTime/fadeLength) {
				source.volume = f;
				yield return null;
			}
			source.Stop ();
			source.volume = 1f;
			yield return StartCoroutine(PlayIntro (source, intro));
		}
		
		IEnumerator FadeOutAndPlay(AudioSource source, AudioClip newClip, float fadeLength) {
			for (float f = 1; f > 0f; f -= Time.deltaTime/fadeLength) {
				source.volume = f;
				yield return null;
			}
			source.Stop ();
			source.clip = newClip;
			source.volume = 1f;
			yield return new WaitForSeconds (0.1f);
			source.Play ();
		}
		
		IEnumerator FadeOutAndDestroy(AudioSource source, float fadeLength) {
			for (float f = 1; f > 0f; f -= Time.deltaTime/fadeLength) {
				source.volume = f;
				yield return null;
			}
			source.Stop ();
			Destroy (source.gameObject);
		}
		
		IEnumerator FadeOutAndDestroy(AudioSource source, float fadeLength, string s_outtro) {
			PlaySFX (s_outtro);
			for (float f = 1; f > 0f; f -= Time.deltaTime/fadeLength) {
				source.volume = f;
				yield return null;
			}
			source.Stop ();
			Destroy (source.gameObject);
		}
	}
}