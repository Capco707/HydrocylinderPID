using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariablesController : MonoBehaviour
{
    public static bool startUpdate = false;
    public static bool sineMode = false;
    public static bool squareWaveMode = false;
    public static bool fullScreen = false;
    public static float sineAmplitude = 0;
    public static float sineFrequency = 0;
    public static float stepAmplitude = 0;
    public static float squareWaveFrequency = 0;
    public static float stepKp = 0;
    public static float stepKi = 0;
    public static float stepKd = 0;
    public static float stepKpBreakPoint = 0;
    public static float stepKiBreakPoint = 0;
    public static float stepKdBreakPoint = 0;
    public static float sineKp = 0;
    public static float sineKi = 0;
    public static float sineKd = 0;
    public static float sineKpBreakPoint = 0;
    public static float sineKiBreakPoint = 0;
    public static float sineKdBreakPoint = 0;
    public static int sineKpScaleType = 1;
    public static int sineKiScaleType = 1;
    public static int sineKdScaleType = 1;
    public static int stepKpScaleType = 1;
    public static int stepKiScaleType = 1;
    public static int stepKdScaleType = 1;
    public static readonly float scaleI = (float)0.1;
    public static readonly float scaleII = 1;
    public static readonly float scaleIII = 5;
    public static readonly float scaleIV = 10;
    
    
    
    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
