using System;
using BibaFramework.BibaGame;
using UnityEngine;

namespace BibaCops
{
    public class CopsTagFoundCommand : TagFoundCommand
    {
        protected override bool IsCorrectTag {
            get {
                return true;
            }
        }
    }
}

