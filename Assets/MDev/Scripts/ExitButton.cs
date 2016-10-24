using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using BibaFramework.BibaMenu;


public class ExitButton : MonoBehaviour {

    public void ExitGame() {
        Animator menuStateMachine = GameObject.FindObjectOfType<MenuStateMachineView>().GetComponent<Animator>();
        if (menuStateMachine)
        {
            menuStateMachine.SetTrigger("Next");
        }
    }
}