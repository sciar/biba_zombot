using System;
using UnityEngine;
using strange.extensions.context.impl;

namespace BibaFramework.BibaMenu
{
    public class BibaRootContextView : ContextView
    {
        private static BibaRootContextView _instance;

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                context = new BibaRootContext(this);
                ((BibaRootContext)context).injectionBinder.Bind<Animator>().To(GetComponentInChildren<Animator>()).ToName(BibaConstants.BIBA_STATE_MACHINE).ToSingleton().CrossContext();

                DontDestroyOnLoad(gameObject);

            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}