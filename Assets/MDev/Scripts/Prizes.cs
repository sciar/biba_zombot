using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using BibaFramework;
using BibaFramework.BibaGame;

public class Prizes : MonoBehaviour {

    public AchievementsView achievementsView;
    public GameObject prizeSelectionScreen;
    public GameObject prizeInstructionScreen;

    public GameObject inputField1;
    public GameObject inputField2;
    public GameObject inputField3;
    public GameObject inputField4;

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;

    private Color defaultColor;
    private Color disableColor;

    private BibaGameState bibaGameState; // Trying to sort out the frameworks variable saving system
    public GameObject menuNav;

    private void OnEnable()
    {
        if (menuNav == null)
            menuNav = GameObject.Find("MenuStateMachine");


        defaultColor = inputField1.GetComponentInChildren<Image>().color;
        disableColor = new Color(0, 0, 0, 255);

        inputField2.GetComponentInChildren<Image>().color = disableColor;
        inputField3.GetComponentInChildren<Image>().color = disableColor;
        inputField4.GetComponentInChildren<Image>().color = disableColor;

        button2.SetActive(false);
        button3.SetActive(false);
        button4.SetActive(false);
    }


    public void TransitionToPrizeScreen()
    {
        if (prizeSelectionScreen.activeSelf == false) // Turns on / off based on current visible screen
            prizeSelectionScreen.SetActive(true);
        else
            prizeSelectionScreen.SetActive(false);

        if (prizeInstructionScreen.activeSelf == true) // Turns on / off based on current visible screen
            prizeInstructionScreen.SetActive(false);
        else
            prizeInstructionScreen.SetActive(true);
    }

    void Start()
    { // Submit
        /*
        var input = gameObject.GetComponent<InputField>();
        var se = new InputField.SubmitEvent();
        se.AddListener(SavePrizeData);
        input.onEndEdit = se;
        */
    }

    public void SavePrizeData()
    {
        string iText1 = inputField1.GetComponentInChildren<Text>().text;
        string iText2 = inputField2.GetComponentInChildren<Text>().text;
        string iText3 = inputField3.GetComponentInChildren<Text>().text;
        string iText4 = inputField4.GetComponentInChildren<Text>().text;

        // Sending this info to bibaDeviceSession script!
        achievementsView.bibaDeviceSession.prize1 = iText1;
        achievementsView.bibaDeviceSession.prize2 = iText2;
        achievementsView.bibaDeviceSession.prize3 = iText3;
        achievementsView.bibaDeviceSession.prize4 = iText4;
       
        // If we have zero prize data we turn the custom prizes off so you don't get an empty prize page
        if (iText1 == "" && iText2 == "" && iText3 == "" && iText4 == "")
        {
            //menuNav.GetComponent<Animator>().SetBool(CustomPrizes, true); - giving errors for some reason
        }
        // Send iText1-4 to the prizes page 

    }

    public void ClearPrizeData(int i)
    {
        if (i == 1)
            inputField1.GetComponent<InputField>().text = "";
        else if (i == 2)
            inputField2.GetComponent<InputField>().text = "";
        else if (i == 3)
            inputField3.GetComponent<InputField>().text = "";
        else if (i == 4)
            inputField4.GetComponent<InputField>().text = "";
    }

    void Update()
    {
        if (prizeSelectionScreen.activeSelf)
        {
            if (inputField1.GetComponent<InputField>().text != "") // If input 1 is used unlock input2
            {
                inputField2.GetComponentInChildren<Image>().color = defaultColor;
                button1.SetActive(true);
            }
            else
            {
                if (inputField2.GetComponent<InputField>().text == "")
                    inputField2.GetComponentInChildren<Image>().color = disableColor;
                button1.SetActive(false);
            }
                
            
            if (inputField2.GetComponent<InputField>().text != "") // If Input 2 is used unlock input 3
            {
                inputField3.GetComponentInChildren<Image>().color = defaultColor;
                button2.SetActive(true);

            }
            else
            {
                if (inputField3.GetComponent<InputField>().text == "")
                    inputField3.GetComponentInChildren<Image>().color = disableColor;
                button2.SetActive(false);
            }
                

            if (inputField3.GetComponent<InputField>().text != "") // If Input 2 is used unlock input 3
            {
                inputField4.GetComponentInChildren<Image>().color = defaultColor;
                button3.SetActive(true);
            }
            else
            {
                if (inputField4.GetComponent<InputField>().text == "")
                    inputField4.GetComponentInChildren<Image>().color = disableColor;
                button3.SetActive(false);
            }
                
            if (inputField4.GetComponent<InputField>().text != "")
            {
                button4.SetActive(true);
            }
            else
            {
                button4.SetActive(false);
            }

        }
    }

}
