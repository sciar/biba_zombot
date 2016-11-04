using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Prizes : MonoBehaviour {

    public GameObject prizeSelectionScreen;
    public GameObject prizeInstructionScreen;

    public GameObject inputField1;
    public GameObject inputField2;
    public GameObject inputField3;
    public GameObject inputField4;

    private Color defaultColor;
    private Color disableColor;

    private void OnEnable()
    {
        defaultColor = inputField1.GetComponentInChildren<Image>().color;
        disableColor = new Color(0, 0, 0, 255);

        inputField2.GetComponentInChildren<Image>().color = disableColor;
        inputField3.GetComponentInChildren<Image>().color = disableColor;
        inputField4.GetComponentInChildren<Image>().color = disableColor;
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
        var iText1 = inputField1.GetComponentInChildren<Text>().text;
        var iText2 = inputField2.GetComponentInChildren<Text>().text;
        var iText3 = inputField3.GetComponentInChildren<Text>().text;
        var iText4 = inputField4.GetComponentInChildren<Text>().text;

        // Send iText1-4 to the prizes page 

    }

    void Update()
    {
        if (prizeSelectionScreen.activeSelf)
        {
            if (inputField1.GetComponent<InputField>().text != "") // If input 1 is used unlock input2
            {
                inputField2.GetComponentInChildren<Image>().color = defaultColor;
            }
            else
                inputField2.GetComponentInChildren<Image>().color = disableColor;
            
            if (inputField2.GetComponent<InputField>().text != "") // If Input 2 is used unlock input 3
            {
                inputField3.GetComponentInChildren<Image>().color = defaultColor;
            }
            else
                inputField3.GetComponentInChildren<Image>().color = disableColor;

            if (inputField3.GetComponent<InputField>().text != "") // If Input 2 is used unlock input 3
            {
                inputField4.GetComponentInChildren<Image>().color = defaultColor;
            }
            else
                inputField4.GetComponentInChildren<Image>().color = disableColor;

        }
    }

}
