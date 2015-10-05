using System;
using UnityEngine;
using strange.extensions.context.impl;
using BibaFramework.BibaAnalytic;

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

                SetupMenuStateMachine();
                SetupAnalytic();
                DontDestroyOnLoad(gameObject);

            }
            else
            {
                gameObject.SetActive(false);
                DestroyImmediate(this.gameObject);
            }
        }

        void SetupMenuStateMachine()
        {
            var anim = GetComponentInChildren<Animator>();
            ((BibaRootContext)context).injectionBinder.Bind<Animator>().To(anim).ToName(BibaMenuConstants.BIBA_STATE_MACHINE).ToSingleton().CrossContext();
            anim.CrossFade(Application.loadedLevelName, 0);
        }

        void SetupAnalytic()
        {
            var flurryConfig = GetComponent<FlurryConfigs>();

            var flurryAnalytics = new FlurryAnalyticService(flurryConfig.FlurryIosKey, flurryConfig.FlurryAndroidKey);
            ((BibaRootContext)context).injectionBinder.Bind<IBibaAnalyticService>().To(flurryAnalytics).ToSingleton().CrossContext();

            Destroy(flurryConfig);
        }
    }
}