using System;
using UnityEngine;
using strange.extensions.context.impl;

namespace BibaFramework.BibaMenu
{
    public class BibaRootContextView : ContextView
    {
        public GameObject EventSystemObject;
        private static BibaRootContextView _instance;

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                context = new BibaRootContext(this);

                //Delay the initialization of the input system since it creates buggy behaviour due to other BibaRoot in the samescene
                EventSystemObject.gameObject.SetActive(true);
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