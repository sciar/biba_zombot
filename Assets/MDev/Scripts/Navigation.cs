using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Navigation : MonoBehaviour {

    private Button thisButton;
    public GameObject menuNav;
    public string trigger;
    public string boolTrigger;

	// The Biba Button View had a weird error so I Matt'd a new nav script
	void Start () {
        if (menuNav == null)
            menuNav = GameObject.Find("MenuStateMachine");
        
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener( () => Navigate() ); 
        //myButton.GetComponent<Button>().onClick.AddListener(() => { someFunction(); otherFunction(); }); 
	}
	
    public void Navigate()
    {
        if (trigger != null)
            menuNav.GetComponent<Animator>().SetTrigger(trigger.ToString());
        if (boolTrigger != null)
            menuNav.GetComponent<Animator>().SetBool(boolTrigger, true);
    }
    private void OnDestroy()
    {
        thisButton.onClick.RemoveListener( () => Navigate() ); 
    }
}
