using System;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputFieldToContent : MonoBehaviour
{
    private EventSystem system;
    public Text cuteLittleText;
    public Text textView;
    public ScrollRect scrollControl;
    public Text tp;
    public Text ts;
    public Text ess;
    public Text deltaPhase;
    private void Update()
    {
        if (!GlobalVariablesController.paused)
        {
            if (!GlobalVariablesController.sineMode)
            {
                tp.text = GlobalVariablesController.tp > 0
                    ? ">>> tp:" + GlobalVariablesController.tp.ToString("f4")
                    : ">>> tp: - - - ";
                ts.text = GlobalVariablesController.ts > 0
                    ? ">>> ts:" + GlobalVariablesController.ts.ToString("f4")
                    : ">>> ts: - - - ";
                ess.text = GlobalVariablesController.ess > 0
                    ? ">>> ess:" + GlobalVariablesController.ess.ToString("f4")
                    : ">>> ess: - - - ";
                deltaPhase.text = "";
            }
            else
            {
                tp.text = "";
                ts.text = "";
                ess.text = "";
                deltaPhase.text = ">>> Delta Phase:\n" + GlobalVariablesController.deltaPhase.ToString("f4");
            }
        }
        cuteLittleText.text = GlobalVariablesController.myCuteLittleText;
        textView.text = GlobalVariablesController.consoleText;
        scrollControl.verticalNormalizedPosition = 0f;

    }
    void Start()
    {
        Application.logMessageReceivedThreaded += HandleLog;
    }
    void HandleLog(string logString, string stackTrace, LogType type)
    {
        GlobalVariablesController.consoleText += logString;
        GlobalVariablesController.consoleText += "\n";
    }
}