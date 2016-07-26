using UnityEngine;
using Vuforia;
using strange.extensions.signal.impl;
using strange.extensions.mediation.impl;

namespace BibaFramework.BibaGame
{
    public class BibaTagEventHandlerView : View, ITrackableEventHandler
    {
		public Signal<string> TrackingFoundSignal = new Signal<string>();
		public Signal<string> TrackingLostSignal = new Signal<string>();

        #region PRIVATE_MEMBER_VARIABLES
        private TrackableBehaviour mTrackableBehaviour;
        #endregion // PRIVATE_MEMBER_VARIABLES

        #region UNTIY_MONOBEHAVIOUR_METHODS
        protected override void Start()
        {
			base.Start ();
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
			TrackingFoundSignal.Dispatch(mTrackableBehaviour.TrackableName);
        }

        private void OnTrackingLost()
        {
			Debug.Log(mTrackableBehaviour.TrackableName + " tag is lost.");
			TrackingLostSignal.Dispatch(mTrackableBehaviour.TrackableName);
        }
        #endregion // PRIVATE_METHODS
    }
}