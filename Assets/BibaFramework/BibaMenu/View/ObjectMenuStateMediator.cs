using strange.extensions.mediation.impl;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
	public class ObjectMenuStateMediator : BaseObjectMenuStateMediator 
	{
		[Inject]
        public ObjectMenuStateView ObjectMenuStateView { get; set; }

        public override BaseObjectMenuStateView BaseObjectMenuStateView { get { return ObjectMenuStateView; } }
	} 
}