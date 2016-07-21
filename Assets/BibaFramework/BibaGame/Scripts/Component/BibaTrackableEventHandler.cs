using UnityEngine;
using Vuforia;
using strange.extensions.signal.impl;

namespace BibaFramework.BibaGame
{
    public class BibaTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
		public Signal<string,Transform> TrackingFoundSignal = new Signal<string,Transform>();
		public Signal<string,Transform> TrackingLostSignal = new Signal<string,Transform>();

        #region PRIVATE_MEMBER_VARIABLES
        private TrackableBehaviour mTrackableBehaviour;
        #endregion // PRIVATE_MEMBER_VARIABLES

        #region UNTIY_MONOBEHAVIOUR_METHODS
        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }
        #endregion // UNTIY_MONOBEHAVIOUR_METHODS

        #region PUBLIC_METHODS
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }
        #endregion // PUBLIC_METHODS


        #region PRIVATE_METHODS
        private void OnTrackingFound()
        {
            Debug.Log(mTrackableBehaviour.TrackableName + " tag is found.");
			TrackingFoundSignal.Dispatch(mTrackableBehaviour.TrackableName,transform);
        }

        private void OnTrackingLost()
        {
			Debug.Log(mTrackableBehaviour.TrackableName + " tag is lost.");
			TrackingLostSignal.Dispatch(mTrackableBehaviour.TrackableName,transform);
        }
        #endregion // PRIVATE_METHODS
    }
}
