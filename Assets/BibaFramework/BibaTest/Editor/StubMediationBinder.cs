using System;
using strange.extensions.injector.api;
using strange.extensions.mediation.api;
using strange.extensions.mediation.impl;
using strange.framework.api;
using strange.framework.impl;
using UnityEngine;

namespace BibaFramework.BibaTest
{
	public class StubMediationBinder : Binder, IMediationBinder 
	{
		[Inject]
		public IInjectionBinder injectionBinder { get; set; }

		public override IBinding GetRawBinding() 
		{
			return new MediationBinding(resolver) as IBinding;
		}

		public void Trigger(MediationEvent evt, IView view) 
		{
			MockMediator mediator = new MockMediator();
			mediator.PreRegister();
			injectionBinder.Bind<MockView>().ToValue(view);
			injectionBinder.injector.Inject(mediator);
			injectionBinder.Unbind<MockView>();
			mediator.OnRegister();
		}

		public IMediationBinding BindView<T>() where T : MonoBehaviour
		{
			return base.Bind<T> () as IMediationBinding;
		}
	}
}