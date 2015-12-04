using strange.extensions.mediation.impl;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    public class PanelMenuStateMediator : BaseObjectMenuStateMediator 
	{
		[Inject]
        public PanelMenuStateView PanelMenuStateView { get; set; }

        public override BaseObjectMenuStateView BaseObjectMenuStateView { get { return PanelMenuStateView; } }

        protected override void MenuStateObjectEnabled(){ }
        protected override void MenuStateObjectDisabled(){ }
    } 
}