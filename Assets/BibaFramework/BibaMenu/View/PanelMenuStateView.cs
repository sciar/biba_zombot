using UnityEngine;

namespace BibaFramework.BibaMenu
{
    [RequireComponent(typeof(Animator))]
    public class PanelMenuStateView : BaseObjectMenuStateView 
	{
        private Animator _anim;
        private Animator anim {
            get 
            {
                if(_anim == null)
                {
                    _anim = GetComponent<Animator>();
                }
                return _anim;
            }
        }
        
        public override void AnimateEntry()
        {
            base.AnimateEntry();
            anim.SetTrigger(BibaMenuConstants.BIBA_MENU_ENTRY_ANIMATION_TRIGGER);
        }

        public override void AnimateExit()
        {
            base.AnimateExit();
            anim.SetTrigger(BibaMenuConstants.BIBA_MENU_EXIT_ANIMATION_TRIGGER);
        }
	} 
}