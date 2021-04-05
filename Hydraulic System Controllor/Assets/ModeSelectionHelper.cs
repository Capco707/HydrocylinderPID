using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

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
        updateButtonEnabled.SetActive(false);
        updateButtonDisabled.SetActive(true);
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
        updateButtonEnabled.SetActive(GlobalVariablesController.startUpdate);
        updateButtonDisabled.SetActive(!GlobalVariablesController.startUpdate);

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

    public void StepButtonClicked()
    {
        amplitudeInputText.text =
            Convert.ToString(GlobalVariablesController.sineAmplitude, CultureInfo.InvariantCulture);
        frequencyInputText.text =
            Convert.ToString(GlobalVariablesController.sineFrequency, CultureInfo.InvariantCulture);
        GlobalVariablesController.sineMode = true;
    }
    
    public void SineButtonClicked()
    {
        amplitudeInputText.text =
            Convert.ToString(GlobalVariablesController.stepAmplitude, CultureInfo.InvariantCulture);
        frequencyInputText.text =
            Convert.ToString(GlobalVariablesController.squareWaveFrequency, CultureInfo.InvariantCulture);
        GlobalVariablesController.sineMode = false;
    }
    
    public void squareWaveOnButtonClicked()
    {
        amplitudeInputText.text =
            Convert.ToString(GlobalVariablesController.stepAmplitude, CultureInfo.InvariantCulture);
        GlobalVariablesController.squareWaveMode = false;
    }
    
    public void squareWaveOffButtonClicked()
    {
        amplitudeInputText.text =
            Convert.ToString(GlobalVariablesController.stepAmplitude, CultureInfo.InvariantCulture);
        frequencyInputText.text =
            Convert.ToString(GlobalVariablesController.squareWaveFrequency, CultureInfo.InvariantCulture);
        GlobalVariablesController.squareWaveMode = true;
    }

    public void confirmButtonClicked()
    {
        if (!GlobalVariablesController.sineMode)
        {
            GlobalVariablesController.stepAmplitude =
                Convert.ToSingle(amplitudeInputText.text);
            if (GlobalVariablesController.squareWaveMode)
            {
                GlobalVariablesController.squareWaveFrequency =
                    Convert.ToSingle(frequencyInputText.text);
            }
        }
        else
        {
            GlobalVariablesController.sineAmplitude =
                Convert.ToSingle(amplitudeInputText.text);
            GlobalVariablesController.sineFrequency =
                Convert.ToSingle(frequencyInputText.text);
        }
    }

    public void updateOnButtonClicked()
    {
        GlobalVariablesController.startUpdate = false;
    }
    
    public void updateOffButtonClicked()
    {
        GlobalVariablesController.startUpdate = true;
    }

    public void autoButtonClicked()
    {
        Debug.Log("Stop dreaming about peach!\n");
    }
}
