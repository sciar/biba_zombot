using System;
using strange.extensions.mediation.impl;
using BibaFramework.BibaGame;

namespace BibaFramework.BibaMenu
{
	public class BibaPointEventServiceMediator : Mediator
	{
		[Inject]
		public PointEventService PointEventService { get; set; }

		[Inject]
		public BibaPointEventServiceView BibaPointEventServiceView { get; set; }

		public override void OnRegister () 
		{
			BibaPointEventServiceView.PointEventService = PointEventService;
		}

		public override void OnRemove() 
		{

		}
	}
}