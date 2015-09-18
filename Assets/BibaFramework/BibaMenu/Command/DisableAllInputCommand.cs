using strange.extensions.command.impl;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace BibaFramework.BibaMenu
{
    public class DisableAllInputCommand : Command 
    {
        public override void Execute ()
        {
            var allRayCasters = GameObject.FindObjectsOfType<GraphicRaycaster>();
            Array.ForEach(allRayCasters, caster => caster.enabled = false);
        }
    }
}