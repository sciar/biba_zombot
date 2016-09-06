using strange.extensions.signal.impl;
using strange.extensions.mediation.impl;
using UnityEngine;
using System.Collections;

namespace BibaFramework.BibaMenu
{
    public class MenuStateMachineView : View
    {
		public Signal<BaseMenuState> MenuStateEnteredSignal  = new Signal<BaseMenuState>(); 

        public void EnteredMenuState(BaseMenuState menuState)
        {
            MenuStateEnteredSignal.Dispatch(menuState);
        }

		/// <summary>
		/// Sets "Back" trigger ON and resets it after a fraction of a second.
		/// This gives the functionality to BACK buttons on Android.
		/// It has a 3 second cooldown to minimize the menu state from freaking out.
		/// </summary>

		#region BackButton
		private Animator animator;
		private const float cooldownLength = 3f;
		private float cooldown = 0f;

		void Update()
		{
			cooldown -= Time.deltaTime;
			if (cooldown <= 0f)
				cooldown = 0f;
			if (Input.GetKeyDown (KeyCode.Escape) && cooldown <= 0f)
			{
				cooldown = cooldownLength;
				StartCoroutine (EscapeOrBackButtonPressed());
			}
		}

		IEnumerator EscapeOrBackButtonPressed()
		{
			if (animator == null) animator = GetComponent<Animator> ();
			animator.SetTrigger ("Back");
			yield return new WaitForSeconds(0.1f);
			animator.ResetTrigger ("Back");
		}
		#endregion
    }
}