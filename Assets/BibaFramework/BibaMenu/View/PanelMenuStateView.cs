using strange.extensions.mediation.impl;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    [RequireComponent(typeof(Animator))]
    public class PanelMenuStateView : BaseObjectMenuStateView 
	{
        public override void AnimateEntry ()
        {
            base.AnimateEntry ();
        }

        public override void AnimateExit ()
        {
            base.AnimateExit ();
        }
	} 
}