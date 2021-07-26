using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariablesController : MonoBehaviour
{
    public static bool startUpdate = false;
    public static bool sineMode = false;
    public static bool squareWaveMode = false;
    public static bool TempSineMode = false;
    public static bool TempSquareWaveMode = false;
    public static bool TempMode = false;
    public static bool fullScreen = false;
    public static bool xScrollable = true;
    public static bool yScrollable = true;
    public static bool measuring = false;
    public static bool referenceOn = true;
    public static bool paused = false;
    public static bool reversedMode = false;
    
    public static float measuredAmp = 0;
    public static float measuredTime = 0;
    public static float referenceAmp = 0;
    public static float referenceTime = 0;
    public static float sineAmplitude = 100;
    public static float sineFrequency = 1;
    public static float stepAmplitude = 100;
    public static float squareWaveFrequency = 1;
    
    public static float stepKp = 5;
    public static float stepKi = (float)0.1;
    public static float stepKd = (float)0.1;
    public static float stepKpBreakPoint = 5;
    public static float stepKiBreakPoint = (float)0.1;
    public static float stepKdBreakPoint = (float)0.1;
    public static float sineKp = 5;
    public static float sineKi = (float)0.1;
    public static float sineKd = (float)0.1;
    public static float sineKpBreakPoint = 5;
    public static float sineKiBreakPoint = (float)0.1;
    public static float sineKdBreakPoint = (float)0.1;
    
    public static float stepKpReverse = 5;
    public static float stepKiReverse = (float)0.1;
    public static float stepKdReverse = (float)0.1;
    public static float stepKpReverseBreakPoint = 5;
    public static float stepKiReverseBreakPoint = (float)0.1;
    public static float stepKdReverseBreakPoint = (float)0.1;
    public static float sineKpReverse = 5;
    public static float sineKiReverse = (float)0.1;
    public static float sineKdReverse = (float)0.1;
    public static float sineKpReverseBreakPoint = 5;
    public static float sineKiReverseBreakPoint = (float)0.1;
    public static float sineKdReverseBreakPoint = (float)0.1;
    
    public static float squareWaveTimer = 0;
    public static float sineWaveTimer = 0;
    public static readonly float scaleI = (float)0.1;
    public static readonly float scaleII = 1;
    public static readonly float scaleIII = 5;
    public static readonly float scaleIV = 10;
    public const float samplingTime = (float) 10;
    public static int sineKpScaleType = 1;
    public static int sineKiScaleType = 1;
    public static int sineKdScaleType = 1;
    public static int stepKpScaleType = 1;
    public static int stepKiScaleType = 1;
    public static int stepKdScaleType = 1;
    public static int systemInput = 0;
    public static List<int> voltageArray = new List<int>();
    public static List<int> voltageArray2 = new List<int>();
    public static List<int> savedVoltageArray = new List<int>();
    public static List<int> savedVoltageArray2 = new List<int>();
    public static string myCuteLittleText = "- - - - - -";
    public static string consoleText = "";
    public static bool savingFinished = false;
    
    
    public static float tp = 0;
    public static float ts = 0;
    public static float ess = 0;
    public static float deltaPhase = 0;
    public static int MeasureTimeCounter = 0;
    public static int TsTimeCounter = 0;
    //public static float MeasuredAmpDerivative = 0;
    public static bool TpMeasuringFlag = false;
    public static bool TsMeasuringFlag = false;
    public static bool IsRisingSession = false;
    public static float DeltaAmp = 0;
    public static bool IsStable = false;

    public static int PhaseCounter = 0;

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
