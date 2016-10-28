using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Director;

namespace BibaFramework.BibaTest
{
	public class StubAnimator : DirectorPlayer, IAnimatorControllerPlayable
	{
		private Dictionary<string, bool> _boolDict = new Dictionary<string,bool>();

		public float GetFloat (string name)
		{
			throw new NotImplementedException ();
		}

		public float GetFloat (int id)
		{
			throw new NotImplementedException ();
		}

		public void SetFloat (string name, float value)
		{
			throw new NotImplementedException ();
		}

		public void SetFloat (int id, float value)
		{
			throw new NotImplementedException ();
		}

		public bool GetBool (string name)
		{
			if (!_boolDict.ContainsKey (name)) 
			{
				return false;
			}

			return _boolDict [name];
		}

		public bool GetBool (int id)
		{
			throw new NotImplementedException ();
		}

		public void SetBool (string name, bool value)
		{
			if (!_boolDict.ContainsKey (name)) 
			{
				_boolDict.Add (name, value);
			}
			_boolDict [name] = value;
		}

		public void SetBool (int id, bool value)
		{
			throw new NotImplementedException ();
		}

		public int GetInteger (string name)
		{
			throw new NotImplementedException ();
		}

		public int GetInteger (int id)
		{
			throw new NotImplementedException ();
		}

		public void SetInteger (string name, int value)
		{
			throw new NotImplementedException ();
		}

		public void SetInteger (int id, int value)
		{
			throw new NotImplementedException ();
		}

		public void SetTrigger (string name)
		{
			throw new NotImplementedException ();
		}

		public void SetTrigger (int id)
		{
			throw new NotImplementedException ();
		}

		public void ResetTrigger (string name)
		{
			throw new NotImplementedException ();
		}

		public void ResetTrigger (int id)
		{
			throw new NotImplementedException ();
		}

		public bool IsParameterControlledByCurve (string name)
		{
			throw new NotImplementedException ();
		}

		public bool IsParameterControlledByCurve (int id)
		{
			throw new NotImplementedException ();
		}

		public string GetLayerName (int layerIndex)
		{
			throw new NotImplementedException ();
		}

		public int GetLayerIndex (string layerName)
		{
			throw new NotImplementedException ();
		}

		public float GetLayerWeight (int layerIndex)
		{
			throw new NotImplementedException ();
		}

		public void SetLayerWeight (int layerIndex, float weight)
		{
			throw new NotImplementedException ();
		}

		public AnimatorStateInfo GetCurrentAnimatorStateInfo (int layerIndex)
		{
			throw new NotImplementedException ();
		}

		public AnimatorStateInfo GetNextAnimatorStateInfo (int layerIndex)
		{
			throw new NotImplementedException ();
		}

		public AnimatorTransitionInfo GetAnimatorTransitionInfo (int layerIndex)
		{
			throw new NotImplementedException ();
		}

		public AnimatorClipInfo[] GetCurrentAnimatorClipInfo (int layerIndex)
		{
			throw new NotImplementedException ();
		}

		public AnimatorClipInfo[] GetNextAnimatorClipInfo (int layerIndex)
		{
			throw new NotImplementedException ();
		}

		public bool IsInTransition (int layerIndex)
		{
			throw new NotImplementedException ();
		}

		public AnimatorControllerParameter GetParameter (int index)
		{
			throw new NotImplementedException ();
		}

		public void CrossFadeInFixedTime (string stateName, float transitionDuration, int layer, float fixedTime)
		{
			throw new NotImplementedException ();
		}

		public void CrossFadeInFixedTime (int stateNameHash, float transitionDuration, int layer, float fixedTime)
		{
			throw new NotImplementedException ();
		}

		public void CrossFade (string stateName, float transitionDuration, int layer, float normalizedTime)
		{
			throw new NotImplementedException ();
		}

		public void CrossFade (int stateNameHash, float transitionDuration, int layer, float normalizedTime)
		{
			throw new NotImplementedException ();
		}

		public void PlayInFixedTime (string stateName, int layer, float fixedTime)
		{
			throw new NotImplementedException ();
		}

		public void PlayInFixedTime (int stateNameHash, int layer, float fixedTime)
		{
			throw new NotImplementedException ();
		}

		public void Play (string stateName, int layer, float normalizedTime)
		{
			throw new NotImplementedException ();
		}

		public void Play (int stateNameHash, int layer, float normalizedTime)
		{
			throw new NotImplementedException ();
		}

		public bool HasState (int layerIndex, int stateID)
		{
			throw new NotImplementedException ();
		}

		public int layerCount {
			get {
				throw new NotImplementedException ();
			}
		}

		public int parameterCount {
			get {
				throw new NotImplementedException ();
			}
		}
	}
}