using strange.extensions.command.impl;
using UnityEngine;

namespace BibaFramework.BibaMenu
{
    public class EnableObjectBasedMenuStateCommand : Command
    {
        [Inject]
        public BibaMenuState NextMenuState { get; set; }

        public override void Execute ()
        {
            var menuStateGameObject = GameObject.Find(NextMenuState.MenuStateGameObject.name);
            menuStateGameObject.SetActive(true);
        }
    }
}