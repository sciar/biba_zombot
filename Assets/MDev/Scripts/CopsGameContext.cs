using System;
using BibaFramework.BibaGame;
using UnityEngine;
using strange.extensions.context.api;

namespace BibaCops
{
    public class CopsGameContext : GameContext
    {
        public CopsGameContext(MonoBehaviour view)
            : base(view)
        {
        }

        public CopsGameContext(MonoBehaviour view, ContextStartupFlags flags)
            : base(view, flags)
        {
        }

        protected override void BindCommands()
        {   
            base.BindCommands();
            commandBinder.GetBinding<TagFoundSignal>().To<CopsTagFoundCommand>();
        }
    }
}

