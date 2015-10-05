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

                var anim = GetComponentInChildren<Animator>();
               ((BibaRootContext)context).injectionBinder.Bind<Animator>().To(anim).ToName(BibaMenuConstants.BIBA_STATE_MACHINE).ToSingleton().CrossContext();
                anim.CrossFade(Application.loadedLevelName, 0);

                DontDestroyOnLoad(gameObject);

            }
            else
            {
                gameObject.SetActive(false);
                DestroyImmediate(this.gameObject);
            }
        }
    }
}