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