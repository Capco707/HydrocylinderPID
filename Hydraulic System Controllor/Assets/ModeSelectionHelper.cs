using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.UI;
using MultithreadingApplication;

public class ModeSelectionHelper : MonoBehaviour
{
    public GameObject autoButton;
    public GameObject stepButtonEnabled;
    public GameObject stepButtonDisabled;
    public GameObject sineButtonEnabled;
    public GameObject sineButtonDisabled;
    public GameObject updateButtonEnabled;
    public GameObject updateButtonDisabled;
    public GameObject squareWaveButtonEnabled;
    public GameObject squareWaveButtonDisabled;
    public GameObject confirmButton;
    public GameObject amplitudeInput;
    public GameObject frequencyInput;
    public GameObject squareWaveButton;
    public InputField amplitudeInputText;
    public InputField frequencyInputText;

    // Start is called before the first frame update
    private void Start()
    {
        autoButton.SetActive(true);
        stepButtonEnabled.SetActive(true);
        stepButtonDisabled.SetActive(false);
        sineButtonEnabled.SetActive(false);
        sineButtonDisabled.SetActive(true);
        updateButtonEnabled.SetActive(true);
        updateButtonDisabled.SetActive(false);
        squareWaveButtonDisabled.SetActive(true);
        squareWaveButtonEnabled.SetActive(false);
        confirmButton.SetActive(true);
        amplitudeInput.SetActive(true);
        frequencyInput.SetActive(false);
        amplitudeInputText.text = GlobalVariablesController.stepAmplitude.ToString();
    }

    // Update is called once per frame
    private void Update()
    {
        updateButtonEnabled.SetActive(!GlobalVariablesController.startUpdate);
        updateButtonDisabled.SetActive(GlobalVariablesController.startUpdate);

        if (Input.GetKeyUp(KeyCode.Return))
        {
            //Debug.Log("clicked\n");
            confirmButtonClicked();
        }

        if (GlobalVariablesController.TempMode)
        {
            if(!GlobalVariablesController.TempSineMode)
            {
                stepButtonEnabled.SetActive(true);
                stepButtonDisabled.SetActive(false);
                sineButtonEnabled.SetActive(false);
                sineButtonDisabled.SetActive(true);
                squareWaveButton.SetActive(true);
                if (!GlobalVariablesController.TempSquareWaveMode)
                {
                    squareWaveButtonDisabled.SetActive(true);
                    squareWaveButtonEnabled.SetActive(false);
                    frequencyInput.SetActive(false);
                }
                else
                {
                    squareWaveButtonDisabled.SetActive(false);
                    squareWaveButtonEnabled.SetActive(true);
                    frequencyInput.SetActive(true);
                }
            }
            else
            {
                stepButtonEnabled.SetActive(false);
                stepButtonDisabled.SetActive(true);
                sineButtonEnabled.SetActive(true);
                sineButtonDisabled.SetActive(false);
                frequencyInput.SetActive(true);
                squareWaveButton.SetActive(false);
            }
        }
        else
        {
            if(!GlobalVariablesController.sineMode)
            {
                stepButtonEnabled.SetActive(true);
                stepButtonDisabled.SetActive(false);
                sineButtonEnabled.SetActive(false);
                sineButtonDisabled.SetActive(true);
                squareWaveButton.SetActive(true);
                if (!GlobalVariablesController.squareWaveMode)
                {
                    squareWaveButtonDisabled.SetActive(true);
                    squareWaveButtonEnabled.SetActive(false);
                    frequencyInput.SetActive(false);
                }
                else
                {
                    squareWaveButtonDisabled.SetActive(false);
                    squareWaveButtonEnabled.SetActive(true);
                    frequencyInput.SetActive(true);
                }
            }
            else
            {
                stepButtonEnabled.SetActive(false);
                stepButtonDisabled.SetActive(true);
                sineButtonEnabled.SetActive(true);
                sineButtonDisabled.SetActive(false);
                frequencyInput.SetActive(true);
                squareWaveButton.SetActive(false);
            }
        }
        //Debug.Log(GlobalVariablesController.TempSineMode);
        //Debug.Log(GlobalVariablesController.TempSquareWaveMode);
        //Debug.Log(GlobalVariablesController.sineMode);
        //Debug.Log(GlobalVariablesController.squareWaveMode);
    }

    public void StepButtonClicked()
    {
        GlobalVariablesController.TempMode = true;
        amplitudeInputText.text =
            Convert.ToString(GlobalVariablesController.sineAmplitude, CultureInfo.InvariantCulture);
        frequencyInputText.text =
            Convert.ToString(GlobalVariablesController.sineFrequency, CultureInfo.InvariantCulture);
        GlobalVariablesController.TempSineMode = true;
        
    }
    
    public void SineButtonClicked()
    {
        GlobalVariablesController.TempMode = true;
        amplitudeInputText.text =
            Convert.ToString(GlobalVariablesController.stepAmplitude, CultureInfo.InvariantCulture);
        frequencyInputText.text =
            Convert.ToString(GlobalVariablesController.squareWaveFrequency, CultureInfo.InvariantCulture);
        
        GlobalVariablesController.TempSineMode = false;
    }
    
    public void squareWaveOnButtonClicked()
    {
        GlobalVariablesController.TempMode = true;
        amplitudeInputText.text =
            Convert.ToString(GlobalVariablesController.stepAmplitude, CultureInfo.InvariantCulture);
        GlobalVariablesController.TempSquareWaveMode = false;
    }
    
    public void squareWaveOffButtonClicked()
    {
        GlobalVariablesController.TempMode = true;
        amplitudeInputText.text =
            Convert.ToString(GlobalVariablesController.stepAmplitude, CultureInfo.InvariantCulture);
        frequencyInputText.text =
            Convert.ToString(GlobalVariablesController.squareWaveFrequency, CultureInfo.InvariantCulture);
        GlobalVariablesController.TempSquareWaveMode = true;
    }

    public void confirmButtonClicked()
    {
        
        GlobalVariablesController.sineMode = GlobalVariablesController.TempSineMode;
        GlobalVariablesController.squareWaveMode = GlobalVariablesController.TempSquareWaveMode;
        GlobalVariablesController.TempMode = false;
        GlobalVariablesController.sineWaveTimer = 0;
        GlobalVariablesController.squareWaveTimer = 0;
        GlobalVariablesController.MeasureTimeCounter = 0;
        if (!GlobalVariablesController.sineMode)
        {
            GlobalVariablesController.stepAmplitude =
                Convert.ToSingle(amplitudeInputText.text);

            GlobalVariablesController.MeasureTimeCounter = 0;
            GlobalVariablesController.TpMeasuringFlag = true;
            GlobalVariablesController.TsMeasuringFlag = true;
            GlobalVariablesController.DeltaAmp = Math.Abs(GlobalVariablesController.voltageArray.Last() - GlobalVariablesController.stepAmplitude);
            GlobalVariablesController.IsRisingSession = GlobalVariablesController.voltageArray.Last() < GlobalVariablesController.stepAmplitude;
            GlobalVariablesController.tp = -1;
            GlobalVariablesController.ts = -1;
            GlobalVariablesController.ess = -1;

            GlobalVariablesController.consoleText += "Step parameters set successfully.\n";
            if (!GlobalVariablesController.squareWaveMode) return;
            GlobalVariablesController.squareWaveFrequency =
                Convert.ToSingle(frequencyInputText.text);
            GlobalVariablesController.consoleText += "Square wave parameters set successfully.\n";

        }
        else
        {
            GlobalVariablesController.sineAmplitude =
                Convert.ToSingle(amplitudeInputText.text);
            GlobalVariablesController.sineFrequency =
                Convert.ToSingle(frequencyInputText.text);
            GlobalVariablesController.consoleText += "Sine wave parameters set successfully.\n";
        }
    }

    public void updateOnButtonClicked()
    {
        GlobalVariablesController.startUpdate = true;
        ThreadProgram threadProgram = new ThreadProgram();
        threadProgram.StartChildThread();
    }
    
    public void updateOffButtonClicked()
    {
        GlobalVariablesController.startUpdate = false;
    }

    public void autoButtonClicked()
    {
        GlobalVariablesController.consoleText += "Auto setting...\n";
    }
    

}
