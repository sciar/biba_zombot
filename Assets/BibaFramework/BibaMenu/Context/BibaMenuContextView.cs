using strange.extensions.context.impl;
using System;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    public class BibaMenuContextView : ContextView
    {
        [SerializeField]
        public string ContextTypeString;

        void Awake()
        {
            context = (BaseBibaMenuContext)Activator.CreateInstance(Type.GetType(ContextTypeString), new object[] { this });
        }
    }
}
