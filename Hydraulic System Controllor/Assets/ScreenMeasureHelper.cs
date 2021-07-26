using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScreenMeasureHelper : MonoBehaviour
{
    public GameObject xScaleButton;
    public GameObject yScaleButton;
    public GameObject xyScaleButton;
    public GameObject referenceOnButton;
    public GameObject referenceOffButton;
    public GameObject xCursor;
    public GameObject yCursor;
    public GameObject yCursor2;
    public GameObject measureTable;
    public Text measureTableText;
    public GameObject referenceTable;
    public Text referenceTableText;
    public GameObject pauseButton;
    public GameObject resumeButton;
    private bool Flagtr = true;
    private bool Flagtd = true;
    private bool Flagts = true;
    private int tempTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        xyScaleButton.SetActive(true);
        xScaleButton.SetActive(false);
        yScaleButton.SetActive(false);
        xCursor.SetActive(false);
        yCursor.SetActive(false);
        measureTable.SetActive(false);
        measureTableText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(GlobalVariablesController.paused)
            {
                ResumeButtonClicked();
            }
            else
            {
                PausedButtonClicked();
            }
        }
        pauseButton.SetActive(GlobalVariablesController.paused);
        resumeButton.SetActive(!GlobalVariablesController.paused);
        switch (GlobalVariablesController.xScrollable)
        {
            case true when GlobalVariablesController.yScrollable:
                xyScaleButton.SetActive(true);
                xScaleButton.SetActive(false);
                yScaleButton.SetActive(false);
                break;
            case false when GlobalVariablesController.yScrollable:
                xyScaleButton.SetActive(false);
                xScaleButton.SetActive(false);
                yScaleButton.SetActive(true);
                break;
            case true when !GlobalVariablesController.yScrollable:
                xyScaleButton.SetActive(false);
                xScaleButton.SetActive(true);
                yScaleButton.SetActive(false);
                break;
        }
        referenceOffButton.SetActive(!GlobalVariablesController.referenceOn);
        referenceOnButton.SetActive(GlobalVariablesController.referenceOn);
        
        xCursor.SetActive(GlobalVariablesController.measuring);
        yCursor.SetActive(GlobalVariablesController.measuring);
        yCursor2.SetActive(GlobalVariablesController.measuring && GlobalVariablesController.referenceOn);
        measureTable.SetActive(GlobalVariablesController.measuring);
        referenceTable.SetActive(GlobalVariablesController.measuring && GlobalVariablesController.referenceOn);
        

        if (Input.GetMouseButton(1))
        {
            GlobalVariablesController.measuring = false;
        }
        
        measureTableText.text = "Amp:" + GlobalVariablesController.measuredAmp.ToString(CultureInfo.InvariantCulture) +
                                "\nTime:" +
                                GlobalVariablesController.measuredTime.ToString(CultureInfo.InvariantCulture);
        referenceTableText.text = "Amp:" + GlobalVariablesController.referenceAmp.ToString(CultureInfo.InvariantCulture) +
                                "\nTime:" +
                                GlobalVariablesController.referenceTime.ToString(CultureInfo.InvariantCulture);

    }
    
    public void ChangeScrollMode()
    {
        switch (GlobalVariablesController.xScrollable)
        {
            case true when GlobalVariablesController.yScrollable:
                GlobalVariablesController.yScrollable = false;
                GlobalVariablesController.xScrollable = true;
                break;
            case true when !GlobalVariablesController.yScrollable:
                GlobalVariablesController.yScrollable = true;
                GlobalVariablesController.xScrollable = false;
                break;
            case false when GlobalVariablesController.yScrollable:
                GlobalVariablesController.yScrollable = true;
                GlobalVariablesController.xScrollable = true;
                break;
        }
    }
    public void ReferenceButtonOnClick()
    {
        GlobalVariablesController.referenceOn = !GlobalVariablesController.referenceOn;
    }

    public void PausedButtonClicked()
    {
        GlobalVariablesController.savedVoltageArray = new List<int>();
        GlobalVariablesController.savedVoltageArray2 = new List<int>();
        GlobalVariablesController.voltageArray.ForEach(i => GlobalVariablesController.savedVoltageArray.Add(i));
        GlobalVariablesController.voltageArray2.ForEach(i => GlobalVariablesController.savedVoltageArray2.Add(i));
        GlobalVariablesController.paused = true;
    }

    public void ResumeButtonClicked()
    {
        GlobalVariablesController.paused = false;
    }
}
