using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
public class ParametersSettingHelper : MonoBehaviour
{
    public Canvas parentCanvas;
    public GameObject kpButton;
    public InputField kpValue;
    public GameObject kpScaleButtonI;
    public GameObject kpScaleButtonII;
    public GameObject kpScaleButtonIII;
    public GameObject kpScaleButtonIV;
    public GameObject kpScrollBar;
    public Text kpMinScale;
    public Text kpMaxScale;
    
    public GameObject kiButton;
    public InputField kiValue;
    public GameObject kiScaleButtonI;
    public GameObject kiScaleButtonII;
    public GameObject kiScaleButtonIII;
    public GameObject kiScaleButtonIV;
    public GameObject kiScrollBar;
    public Text kiMinScale;
    public Text kiMaxScale;
    
    public GameObject kdButton;
    public InputField kdValue;
    public GameObject kdScaleButtonI;
    public GameObject kdScaleButtonII;
    public GameObject kdScaleButtonIII;
    public GameObject kdScaleButtonIV;
    public GameObject kdScrollBar;
    public Text kdMinScale;
    public Text kdMaxScale;

    public Text KpTitle;
    public Text KiTitle;
    public Text KdTitle;
    
    public GameObject SaveBreakPointButton;
    public GameObject LoadBreakPointButton;
    
    private bool onDrag = false;
    private bool kpScrollBarOnDrag = false;
    private bool kiScrollBarOnDrag = false;
    private bool kdScrollBarOnDrag = false;
    private bool onEdit = false;
    private float deltaX = 0;
    private float initMousePositionX = 0;
    private float initMousePositionY = 0;
    private int dragType = 1;

    private float stepTempKp = 0;
    private float stepTempKi = 0;
    private float stepTempKd = 0;
    private float sineTempKp = 0;
    private float sineTempKi = 0;
    private float sineTempKd = 0;

    private float scrollInitX;
    private float initScrollXOffset;

    private float scrollStepTempKp = 0;
    private float scrollStepTempKi = 0;
    private float scrollStepTempKd = 0;
    private float scrollSineTempKp = 0;
    private float scrollSineTempKi = 0;
    private float scrollSineTempKd = 0;

    public GameObject mouseArrow;

    // Start is called before the first frame update
    private void Start()
    {
        GlobalVariablesController.consoleText +=
            "System Initializing...\n" +
            "Parameters initialization finished.\n" +
            "User Interface initialization finished.\n" +
            "System initialization finished with 0 errors.\n>>>";
        kpButton.SetActive(true);
        kpScaleButtonI.SetActive(true);
        kpScaleButtonII.SetActive(false);
        kpScaleButtonIII.SetActive(false);
        kpScaleButtonIV.SetActive(false);
        kpScrollBar.SetActive(true);
        
        kiButton.SetActive(true);
        kiScaleButtonI.SetActive(true);
        kiScaleButtonII.SetActive(false);
        kiScaleButtonIII.SetActive(false);
        kiScaleButtonIV.SetActive(false);
        kiScrollBar.SetActive(true);
        
        kdButton.SetActive(true);
        kdScaleButtonI.SetActive(true);
        kdScaleButtonII.SetActive(false);
        kdScaleButtonIII.SetActive(false);
        kdScaleButtonIV.SetActive(false);
        kdScrollBar.SetActive(true);
        
        mouseArrow.SetActive(false);
        KpTitle.text = "Kp+:";
        KiTitle.text = "Ki+:";
        KdTitle.text = "Kd+:";
    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            GlobalVariablesController.reversedMode = !GlobalVariablesController.reversedMode;
            if (!GlobalVariablesController.reversedMode)
            {
                kpValue.text = !GlobalVariablesController.sineMode
                    ? (GlobalVariablesController.stepKp).ToString(CultureInfo.InvariantCulture)
                    : (GlobalVariablesController.sineKp).ToString(CultureInfo.InvariantCulture);
                kiValue.text = !GlobalVariablesController.sineMode
                    ? (GlobalVariablesController.stepKi).ToString(CultureInfo.InvariantCulture)
                    : (GlobalVariablesController.sineKi).ToString(CultureInfo.InvariantCulture);
                kdValue.text = !GlobalVariablesController.sineMode
                    ? (GlobalVariablesController.stepKd).ToString(CultureInfo.InvariantCulture)
                    : (GlobalVariablesController.sineKd).ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                kpValue.text = !GlobalVariablesController.sineMode
                    ? (GlobalVariablesController.stepKpReverse).ToString(CultureInfo.InvariantCulture)
                    : (GlobalVariablesController.sineKpReverse).ToString(CultureInfo.InvariantCulture);
                kiValue.text = !GlobalVariablesController.sineMode
                    ? (GlobalVariablesController.stepKiReverse).ToString(CultureInfo.InvariantCulture)
                    : (GlobalVariablesController.sineKiReverse).ToString(CultureInfo.InvariantCulture);
                kdValue.text = !GlobalVariablesController.sineMode
                    ? (GlobalVariablesController.stepKdReverse).ToString(CultureInfo.InvariantCulture)
                    : (GlobalVariablesController.sineKdReverse).ToString(CultureInfo.InvariantCulture);
            }
        }
        if (!GlobalVariablesController.reversedMode)
        {
            KpTitle.text = "Kp+:";
            KiTitle.text = "Ki+:";
            KdTitle.text = "Kd+:";
        }
        else
        {
            KpTitle.text = "Kp-:";
            KiTitle.text = "Ki-:";
            KdTitle.text = "Kd-:";
        }
        var kpModeFlag = !GlobalVariablesController.sineMode
            ? GlobalVariablesController.stepKpScaleType
            : GlobalVariablesController.sineKpScaleType;
        switch (kpModeFlag)
        {
            case 1:
                kpScaleButtonI.SetActive(true);
                kpScaleButtonII.SetActive(false);
                kpScaleButtonIII.SetActive(false);
                kpScaleButtonIV.SetActive(false);
                kpMinScale.text = "-" + GlobalVariablesController.scaleI.ToString(CultureInfo.InvariantCulture);
                kpMaxScale.text = "+" + GlobalVariablesController.scaleI.ToString(CultureInfo.InvariantCulture);
                break;
            case 2:
                kpScaleButtonI.SetActive(false);
                kpScaleButtonII.SetActive(true);
                kpScaleButtonIII.SetActive(false);
                kpScaleButtonIV.SetActive(false);
                kpMinScale.text = "-" + GlobalVariablesController.scaleII.ToString(CultureInfo.InvariantCulture);
                kpMaxScale.text = "+" + GlobalVariablesController.scaleII.ToString(CultureInfo.InvariantCulture);
                break;
            case 3:
                kpScaleButtonI.SetActive(false);
                kpScaleButtonII.SetActive(false);
                kpScaleButtonIII.SetActive(true);
                kpScaleButtonIV.SetActive(false);
                kpMinScale.text = "-" + GlobalVariablesController.scaleIII.ToString(CultureInfo.InvariantCulture);
                kpMaxScale.text = "+" + GlobalVariablesController.scaleIII.ToString(CultureInfo.InvariantCulture);
                break;
            case 4:
                kpScaleButtonI.SetActive(false);
                kpScaleButtonII.SetActive(false);
                kpScaleButtonIII.SetActive(false);
                kpScaleButtonIV.SetActive(true);
                kpMinScale.text = "-" + GlobalVariablesController.scaleIV.ToString(CultureInfo.InvariantCulture);
                kpMaxScale.text = "+" + GlobalVariablesController.scaleIV.ToString(CultureInfo.InvariantCulture);
                break;
        }
        var kiModeFlag = !GlobalVariablesController.sineMode
            ? GlobalVariablesController.stepKiScaleType
            : GlobalVariablesController.sineKiScaleType;
        switch (kiModeFlag)
        {
            case 1:
                kiScaleButtonI.SetActive(true);
                kiScaleButtonII.SetActive(false);
                kiScaleButtonIII.SetActive(false);
                kiScaleButtonIV.SetActive(false);
                kiMinScale.text = "-" + GlobalVariablesController.scaleI.ToString(CultureInfo.InvariantCulture);
                kiMaxScale.text = "+" + GlobalVariablesController.scaleI.ToString(CultureInfo.InvariantCulture);
                break;
            case 2:
                kiScaleButtonI.SetActive(false);
                kiScaleButtonII.SetActive(true);
                kiScaleButtonIII.SetActive(false);
                kiScaleButtonIV.SetActive(false);
                kiMinScale.text = "-" + GlobalVariablesController.scaleII.ToString(CultureInfo.InvariantCulture);
                kiMaxScale.text = "+" + GlobalVariablesController.scaleII.ToString(CultureInfo.InvariantCulture);
                break;
            case 3:
                kiScaleButtonI.SetActive(false);
                kiScaleButtonII.SetActive(false);
                kiScaleButtonIII.SetActive(true);
                kiScaleButtonIV.SetActive(false);
                kiMinScale.text = "-" + GlobalVariablesController.scaleIII.ToString(CultureInfo.InvariantCulture);
                kiMaxScale.text = "+" + GlobalVariablesController.scaleIII.ToString(CultureInfo.InvariantCulture);
                break;
            case 4:
                kiScaleButtonI.SetActive(false);
                kiScaleButtonII.SetActive(false);
                kiScaleButtonIII.SetActive(false);
                kiScaleButtonIV.SetActive(true);
                kiMinScale.text = "-" + GlobalVariablesController.scaleIV.ToString(CultureInfo.InvariantCulture);
                kiMaxScale.text = "+" + GlobalVariablesController.scaleIV.ToString(CultureInfo.InvariantCulture);
                break;
        }
        var kdModeFlag = !GlobalVariablesController.sineMode
            ? GlobalVariablesController.stepKdScaleType
            : GlobalVariablesController.sineKdScaleType;
        switch (kdModeFlag)
        {
            case 1:
                kdScaleButtonI.SetActive(true);
                kdScaleButtonII.SetActive(false);
                kdScaleButtonIII.SetActive(false);
                kdScaleButtonIV.SetActive(false);
                kdMinScale.text = "-" + GlobalVariablesController.scaleI.ToString(CultureInfo.InvariantCulture);
                kdMaxScale.text = "+" + GlobalVariablesController.scaleI.ToString(CultureInfo.InvariantCulture);
                break;
            case 2:
                kdScaleButtonI.SetActive(false);
                kdScaleButtonII.SetActive(true);
                kdScaleButtonIII.SetActive(false);
                kdScaleButtonIV.SetActive(false);
                kdMinScale.text = "-" + GlobalVariablesController.scaleII.ToString(CultureInfo.InvariantCulture);
                kdMaxScale.text = "+" + GlobalVariablesController.scaleII.ToString(CultureInfo.InvariantCulture);
                break;
            case 3:
                kdScaleButtonI.SetActive(false);
                kdScaleButtonII.SetActive(false);
                kdScaleButtonIII.SetActive(true);
                kdScaleButtonIV.SetActive(false);
                kdMinScale.text = "-" + GlobalVariablesController.scaleIII.ToString(CultureInfo.InvariantCulture);
                kdMaxScale.text = "+" + GlobalVariablesController.scaleIII.ToString(CultureInfo.InvariantCulture);
                break;
            case 4:
                kdScaleButtonI.SetActive(false);
                kdScaleButtonII.SetActive(false);
                kdScaleButtonIII.SetActive(false);
                kdScaleButtonIV.SetActive(true);
                kdMinScale.text = "-" + GlobalVariablesController.scaleIV.ToString(CultureInfo.InvariantCulture);
                kdMaxScale.text = "+" + GlobalVariablesController.scaleIV.ToString(CultureInfo.InvariantCulture);
                break;
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnMouseUp(); 
        }

        if (kpScrollBarOnDrag)
        {
            if (Camera.main is { })
            {
                var dist = Camera.main.ScreenToWorldPoint(Input.mousePosition).x  + initScrollXOffset;
                if (dist >= 1.7)
                {
                    dist = (float) 1.7;
                }
                else if (dist <= -1.7)
                {
                    dist = (float) -1.7;
                }
                kpScrollBar.transform.localPosition =
                    new Vector3(dist,
                        kpScrollBar.transform.localPosition.y,
                        kpScrollBar.transform.localPosition.z);
                if (!GlobalVariablesController.sineMode)
                {
                    var delta = (dist - scrollInitX) * FindScale(GlobalVariablesController.stepKpScaleType);
                    if (!GlobalVariablesController.reversedMode)
                    {
                        GlobalVariablesController.stepKp = scrollStepTempKp + delta;
                    }
                    else
                    {
                        GlobalVariablesController.stepKpReverse = scrollStepTempKp + delta;
                    }
                    
                }
                else
                {
                    var delta = (dist - scrollInitX) * FindScale(GlobalVariablesController.sineKpScaleType);
                    if (!GlobalVariablesController.reversedMode)
                    {
                        GlobalVariablesController.sineKp = scrollSineTempKp + delta;
                    }
                    else
                    {
                        GlobalVariablesController.sineKpReverse = scrollSineTempKp + delta;
                    }
                }
            }

            if (!GlobalVariablesController.reversedMode)
            {
                kpValue.text = !GlobalVariablesController.sineMode
                    ? (GlobalVariablesController.stepKp).ToString(CultureInfo.InvariantCulture)
                    : (GlobalVariablesController.sineKp).ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                kpValue.text = !GlobalVariablesController.sineMode
                    ? (GlobalVariablesController.stepKpReverse).ToString(CultureInfo.InvariantCulture)
                    : (GlobalVariablesController.sineKpReverse).ToString(CultureInfo.InvariantCulture);
            }
        }
        if (kiScrollBarOnDrag)
        {
            if (Camera.main is { })
            {
                var dist = Camera.main.ScreenToWorldPoint(Input.mousePosition).x + initScrollXOffset;
                if (dist >= 1.7)
                {
                    dist = (float) 1.7;
                }
                else if (dist <= -1.7)
                {
                    dist = (float) -1.7;
                }
                kiScrollBar.transform.localPosition =
                    new Vector3(dist,
                        kiScrollBar.transform.localPosition.y,
                        kiScrollBar.transform.localPosition.z);
                if (!GlobalVariablesController.sineMode)
                {
                    var delta = (dist - scrollInitX) * FindScale(GlobalVariablesController.stepKiScaleType);
                    if (!GlobalVariablesController.reversedMode)
                    {
                        GlobalVariablesController.stepKi = scrollStepTempKi + delta;
                    }
                    else
                    {
                        GlobalVariablesController.stepKiReverse = scrollStepTempKi + delta;
                    }
                    
                }
                else
                {
                    var delta = (dist - scrollInitX) * FindScale(GlobalVariablesController.sineKiScaleType);
                    if (!GlobalVariablesController.reversedMode)
                    {
                        GlobalVariablesController.sineKi = scrollSineTempKi + delta;
                    }
                    else
                    {
                        GlobalVariablesController.sineKiReverse = scrollSineTempKi + delta;
                    }
                    
                }
            }

            if (!GlobalVariablesController.reversedMode)
            {
                kiValue.text = !GlobalVariablesController.sineMode
                                ? (GlobalVariablesController.stepKi).ToString(CultureInfo.InvariantCulture)
                                : (GlobalVariablesController.sineKi).ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                kiValue.text = !GlobalVariablesController.sineMode
                    ? (GlobalVariablesController.stepKiReverse).ToString(CultureInfo.InvariantCulture)
                    : (GlobalVariablesController.sineKiReverse).ToString(CultureInfo.InvariantCulture);
            }
        }
        if (kdScrollBarOnDrag)
        {
            if (Camera.main is { })
            {
                var dist = Camera.main.ScreenToWorldPoint(Input.mousePosition).x + initScrollXOffset;
                if (dist >= 1.7)
                {
                    dist = (float) 1.7;
                }
                else if (dist <= -1.7)
                {
                    dist = (float) -1.7;
                }
                kdScrollBar.transform.localPosition =
                    new Vector3(dist,
                        kdScrollBar.transform.localPosition.y,
                        kdScrollBar.transform.localPosition.z);
                if (!GlobalVariablesController.sineMode)
                {
                    var delta = (dist - scrollInitX) * FindScale(GlobalVariablesController.stepKdScaleType);
                    if (!GlobalVariablesController.reversedMode)
                    {
                        GlobalVariablesController.stepKd = scrollStepTempKd + delta;
                    }
                    else
                    {
                        GlobalVariablesController.stepKdReverse = scrollStepTempKd + delta;
                    }
                }
                else
                {
                    var delta = (dist - scrollInitX) * FindScale(GlobalVariablesController.sineKdScaleType);
                    if (!GlobalVariablesController.reversedMode)
                    {
                        GlobalVariablesController.sineKd = scrollSineTempKd + delta;
                    }
                    else
                    { 
                        GlobalVariablesController.sineKdReverse = scrollSineTempKd + delta;
                    }
                    
                }
            }

            if (!GlobalVariablesController.reversedMode)
            {
                kdValue.text = !GlobalVariablesController.sineMode
                                ? (GlobalVariablesController.stepKd).ToString(CultureInfo.InvariantCulture)
                                : (GlobalVariablesController.sineKd).ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                kdValue.text = !GlobalVariablesController.sineMode
                    ? (GlobalVariablesController.stepKdReverse).ToString(CultureInfo.InvariantCulture)
                    : (GlobalVariablesController.sineKdReverse).ToString(CultureInfo.InvariantCulture);
            }
        }

        if (!onEdit)
        {
            if (!GlobalVariablesController.reversedMode)
            {
                kpValue.text = !GlobalVariablesController.sineMode
                    ? (GlobalVariablesController.stepKp).ToString(CultureInfo.InvariantCulture)
                    : (GlobalVariablesController.sineKp).ToString(CultureInfo.InvariantCulture);
                kiValue.text = !GlobalVariablesController.sineMode
                    ? (GlobalVariablesController.stepKi).ToString(CultureInfo.InvariantCulture)
                    : (GlobalVariablesController.sineKi).ToString(CultureInfo.InvariantCulture);
                kdValue.text = !GlobalVariablesController.sineMode
                    ? (GlobalVariablesController.stepKd).ToString(CultureInfo.InvariantCulture)
                    : (GlobalVariablesController.sineKd).ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                kpValue.text = !GlobalVariablesController.sineMode
                    ? (GlobalVariablesController.stepKpReverse).ToString(CultureInfo.InvariantCulture)
                    : (GlobalVariablesController.sineKpReverse).ToString(CultureInfo.InvariantCulture);
                kiValue.text = !GlobalVariablesController.sineMode
                    ? (GlobalVariablesController.stepKiReverse).ToString(CultureInfo.InvariantCulture)
                    : (GlobalVariablesController.sineKiReverse).ToString(CultureInfo.InvariantCulture);
                kdValue.text = !GlobalVariablesController.sineMode
                    ? (GlobalVariablesController.stepKdReverse).ToString(CultureInfo.InvariantCulture)
                    : (GlobalVariablesController.sineKdReverse).ToString(CultureInfo.InvariantCulture);
            }
        }
        if (!onDrag) return;
        Cursor.visible = false;
        mouseArrow.transform.position = new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, 0);
        mouseArrow.SetActive(true);
        
        deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - initMousePositionX;
        switch (dragType)
        {
            case 1:
                if (!GlobalVariablesController.sineMode)
                {   
                    stepTempKp = deltaX * FindScale(GlobalVariablesController.stepKpScaleType);
                }
                else
                {
                    sineTempKp = deltaX * FindScale(GlobalVariablesController.sineKpScaleType);
                }
                break; 
            case 2:
                if (!GlobalVariablesController.sineMode)
                {
                    stepTempKi = deltaX * FindScale(GlobalVariablesController.stepKiScaleType);
                }
                else
                {
                    sineTempKi = deltaX * FindScale(GlobalVariablesController.sineKiScaleType);
                }
                break;  
            case 3:
                if (!GlobalVariablesController.sineMode)
                {
                    stepTempKd = deltaX * FindScale(GlobalVariablesController.stepKdScaleType);
                }
                else
                {
                    sineTempKd = deltaX * FindScale(GlobalVariablesController.sineKdScaleType);
                }
                break;  
        }

        if (!GlobalVariablesController.reversedMode)
        {
            kpValue.text = !GlobalVariablesController.sineMode
                ? (GlobalVariablesController.stepKp + stepTempKp).ToString(CultureInfo.InvariantCulture)
                : (GlobalVariablesController.sineKp + sineTempKp).ToString(CultureInfo.InvariantCulture);
            kiValue.text = !GlobalVariablesController.sineMode
                ? (GlobalVariablesController.stepKi + stepTempKi).ToString(CultureInfo.InvariantCulture)
                : (GlobalVariablesController.sineKi + sineTempKi).ToString(CultureInfo.InvariantCulture);
            kdValue.text = !GlobalVariablesController.sineMode
                ? (GlobalVariablesController.stepKd + stepTempKd).ToString(CultureInfo.InvariantCulture)
                : (GlobalVariablesController.sineKd + sineTempKd).ToString(CultureInfo.InvariantCulture);
        }
        else
        {
            kpValue.text = !GlobalVariablesController.sineMode
                ? (GlobalVariablesController.stepKpReverse + stepTempKp).ToString(CultureInfo.InvariantCulture)
                : (GlobalVariablesController.sineKpReverse + sineTempKp).ToString(CultureInfo.InvariantCulture);
            kiValue.text = !GlobalVariablesController.sineMode
                ? (GlobalVariablesController.stepKiReverse + stepTempKi).ToString(CultureInfo.InvariantCulture)
                : (GlobalVariablesController.sineKiReverse + sineTempKi).ToString(CultureInfo.InvariantCulture);
            kdValue.text = !GlobalVariablesController.sineMode
                ? (GlobalVariablesController.stepKdReverse + stepTempKd).ToString(CultureInfo.InvariantCulture)
                : (GlobalVariablesController.sineKdReverse + sineTempKd).ToString(CultureInfo.InvariantCulture);
        }
    }

    public void KpScaleButtonIClicked()
    {
        if (!GlobalVariablesController.sineMode)
        {
            if (GlobalVariablesController.stepKpScaleType <= 3)
                GlobalVariablesController.stepKpScaleType += 1;
            else
            {
                GlobalVariablesController.stepKpScaleType = 1;
            }
        }
        else
        {
            if (GlobalVariablesController.sineKpScaleType <= 3)
                GlobalVariablesController.sineKpScaleType += 1;
            else
            {
                GlobalVariablesController.sineKpScaleType = 1;
            }
        }
    }
    
    public void KiScaleButtonIClicked()
    {
        if (!GlobalVariablesController.sineMode)
        {
            if (GlobalVariablesController.stepKiScaleType <= 3)
                GlobalVariablesController.stepKiScaleType += 1;
            else
            {
                GlobalVariablesController.stepKiScaleType = 1;
            }
        }
        else
        {
            if (GlobalVariablesController.sineKiScaleType <= 3)
                GlobalVariablesController.sineKiScaleType += 1;
            else
            {
                GlobalVariablesController.sineKiScaleType = 1;
            }
        }
    }
    
    public void KdScaleButtonIClicked()
    {
        if (!GlobalVariablesController.sineMode)
        {
            if (GlobalVariablesController.stepKdScaleType <= 3)
                GlobalVariablesController.stepKdScaleType += 1;
            else
            {
                GlobalVariablesController.stepKdScaleType = 1;
            }
        }
        else
        {
            if (GlobalVariablesController.sineKdScaleType <= 3)
                GlobalVariablesController.sineKdScaleType += 1;
            else
            {
                GlobalVariablesController.sineKdScaleType = 1;
            }
        }
    }
    

    public void KpRecordMousePosition()
    {
        dragType = 1;
        if (Camera.main is null) return;
        onDrag = true;
        var initMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        initMousePositionX = initMousePosition.x;
        initMousePositionY = initMousePosition.y;
    }
    
    
    public void KiRecordMousePosition()
    {
        dragType = 2;
        if (Camera.main is null) return;
        onDrag = true;
        var initMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        initMousePositionX = initMousePosition.x;
        initMousePositionY = initMousePosition.y;
    }
    
    
    public void KdRecordMousePosition()
    {
        dragType = 3;
        if (Camera.main is null) return;
        onDrag = true;
        var initMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        initMousePositionX = initMousePosition.x;
        initMousePositionY = initMousePosition.y;
    }

    public void OnEndEdit()
    {
        onEdit = false;
        if (!GlobalVariablesController.sineMode)
        {
            if (!GlobalVariablesController.reversedMode)
            {
                GlobalVariablesController.stepKp = Convert.ToSingle(kpValue.text);
                GlobalVariablesController.stepKi = Convert.ToSingle(kiValue.text);
                GlobalVariablesController.stepKd = Convert.ToSingle(kdValue.text);
            }
            else
            {
                GlobalVariablesController.stepKpReverse = Convert.ToSingle(kpValue.text);
                GlobalVariablesController.stepKiReverse = Convert.ToSingle(kiValue.text);
                GlobalVariablesController.stepKdReverse = Convert.ToSingle(kdValue.text);
            }
            
        }
        else
        {
            if (!GlobalVariablesController.reversedMode)
            {
                GlobalVariablesController.sineKp = Convert.ToSingle(kpValue.text);
                GlobalVariablesController.sineKi = Convert.ToSingle(kiValue.text);
                GlobalVariablesController.sineKd = Convert.ToSingle(kdValue.text);
            }
            else
            {
                GlobalVariablesController.sineKpReverse = Convert.ToSingle(kpValue.text);
                GlobalVariablesController.sineKiReverse = Convert.ToSingle(kiValue.text);
                GlobalVariablesController.sineKdReverse = Convert.ToSingle(kdValue.text);
            }
        }
    }
    public void OnEdit()
    {
        onEdit = true;
    }

    public void SaveBreakPoint()
    {
        if (!GlobalVariablesController.sineMode)
        {
            GlobalVariablesController.stepKpBreakPoint = GlobalVariablesController.stepKp;
            GlobalVariablesController.stepKiBreakPoint = GlobalVariablesController.stepKi;
            GlobalVariablesController.stepKdBreakPoint = GlobalVariablesController.stepKd;
            GlobalVariablesController.stepKpReverseBreakPoint = GlobalVariablesController.stepKpReverse;
            GlobalVariablesController.stepKiReverseBreakPoint = GlobalVariablesController.stepKiReverse;
            GlobalVariablesController.stepKdReverseBreakPoint = GlobalVariablesController.stepKdReverse;
        }
        else
        {
            GlobalVariablesController.sineKpBreakPoint = GlobalVariablesController.sineKp;
            GlobalVariablesController.sineKiBreakPoint = GlobalVariablesController.sineKi;
            GlobalVariablesController.sineKdBreakPoint = GlobalVariablesController.sineKd;
            GlobalVariablesController.sineKpReverseBreakPoint = GlobalVariablesController.sineKpReverse;
            GlobalVariablesController.sineKiReverseBreakPoint = GlobalVariablesController.sineKiReverse;
            GlobalVariablesController.sineKdReverseBreakPoint = GlobalVariablesController.sineKdReverse;
        }
        GlobalVariablesController.consoleText += "Breakpoint saved successfully.\n";
        onEdit = false;
        
    }
    public void LoadBreakPoint()
    {
        if (!GlobalVariablesController.sineMode)
        {
            GlobalVariablesController.stepKp = GlobalVariablesController.stepKpBreakPoint;
            GlobalVariablesController.stepKi = GlobalVariablesController.stepKiBreakPoint;
            GlobalVariablesController.stepKd = GlobalVariablesController.stepKdBreakPoint;
            GlobalVariablesController.stepKpReverse = GlobalVariablesController.stepKpReverseBreakPoint;
            GlobalVariablesController.stepKiReverse = GlobalVariablesController.stepKiReverseBreakPoint;
            GlobalVariablesController.stepKdReverse = GlobalVariablesController.stepKdReverseBreakPoint;
        }
        else
        {
            GlobalVariablesController.sineKp = GlobalVariablesController.sineKpBreakPoint;
            GlobalVariablesController.sineKi = GlobalVariablesController.sineKiBreakPoint;
            GlobalVariablesController.sineKd = GlobalVariablesController.sineKdBreakPoint;
            GlobalVariablesController.sineKpReverse = GlobalVariablesController.sineKpReverseBreakPoint;
            GlobalVariablesController.sineKiReverse = GlobalVariablesController.sineKiReverseBreakPoint;
            GlobalVariablesController.sineKdReverse = GlobalVariablesController.sineKdReverseBreakPoint;
        }

        GlobalVariablesController.consoleText += "Breakpoint loaded successfully.\n";
        onEdit = false;
    }

    public void KpScrollBarMouseDown()
    {
        kpScrollBarOnDrag = true;
        if (Camera.main is null) return;
        if (!GlobalVariablesController.sineMode)
        {
            scrollStepTempKp = !GlobalVariablesController.reversedMode
                ? GlobalVariablesController.stepKp
                : GlobalVariablesController.stepKpReverse;
        }
        else
        {
            scrollSineTempKp = !GlobalVariablesController.reversedMode
                ? GlobalVariablesController.sineKp
                : GlobalVariablesController.sineKpReverse;
        }
        var position = kpScrollBar.transform.localPosition;
        scrollInitX = position.x;
        initScrollXOffset = position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
    }
    public void KpScrollBarMouseUp()
    {
        kpScrollBarOnDrag = false;
        initScrollXOffset = 0;
        if (!GlobalVariablesController.sineMode)
        {
            if (!GlobalVariablesController.reversedMode)
            {
                GlobalVariablesController.stepKp = scrollStepTempKp;
            }
            else
            {
                GlobalVariablesController.stepKpReverse = scrollStepTempKp;
            }
            scrollStepTempKp = 0;
        }
        else
        {
            if (!GlobalVariablesController.reversedMode)
            {
                GlobalVariablesController.sineKp = scrollSineTempKp;
            }
            else
            {
                GlobalVariablesController.sineKpReverse = scrollSineTempKp;
            }
            scrollSineTempKp = 0;
        }
        if (Camera.main is null) return;
        kpScrollBar.transform.localPosition = new Vector3(scrollInitX, kpScrollBar.transform.localPosition.y,
            kpScrollBar.transform.localPosition.z);
        onEdit = false;
    }
    
    public void KiScrollBarMouseDown()
    {
        kiScrollBarOnDrag = true;
        if (Camera.main is null) return;
        if (!GlobalVariablesController.sineMode)
        {
            scrollStepTempKi = !GlobalVariablesController.reversedMode
                ? GlobalVariablesController.stepKi
                : GlobalVariablesController.stepKiReverse;
        }
        else
        {
            scrollSineTempKi = !GlobalVariablesController.reversedMode
                ? GlobalVariablesController.sineKi
                : GlobalVariablesController.sineKiReverse;
        }
        
        var position = kiScrollBar.transform.localPosition;
        scrollInitX = position.x;
        initScrollXOffset = position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
    }
    public void KiScrollBarMouseUp()
    {
        kiScrollBarOnDrag = false;
        initScrollXOffset = 0;
        if (!GlobalVariablesController.sineMode)
        {
            if (!GlobalVariablesController.reversedMode)
            {
                GlobalVariablesController.stepKi = scrollStepTempKi;
            }
            else
            {
                GlobalVariablesController.stepKiReverse = scrollStepTempKi;
            }
            scrollStepTempKi = 0;
        }
        else
        {
            if (!GlobalVariablesController.reversedMode)
            {
                GlobalVariablesController.sineKi = scrollSineTempKi;
            }
            else
            {
                GlobalVariablesController.sineKiReverse = scrollSineTempKi;
            }
            scrollSineTempKi = 0;
        }
        if (Camera.main is null) return;
        kiScrollBar.transform.localPosition = new Vector3(scrollInitX, kiScrollBar.transform.localPosition.y,
            kiScrollBar.transform.localPosition.z);
        onEdit = false;
    }
    
    public void KdScrollBarMouseDown()
    {
        kdScrollBarOnDrag = true;
        if (Camera.main is null) return;
        if (!GlobalVariablesController.sineMode)
        {
            scrollStepTempKd = !GlobalVariablesController.reversedMode
                ? GlobalVariablesController.stepKd
                : GlobalVariablesController.stepKdReverse;
        }
        else
        {
            scrollSineTempKd = !GlobalVariablesController.reversedMode
                ? GlobalVariablesController.sineKd
                : GlobalVariablesController.sineKdReverse;
        }
        var position = kdScrollBar.transform.localPosition;
        scrollInitX = position.x;
        initScrollXOffset = position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
    }
    public void KdScrollBarMouseUp()
    {
        kdScrollBarOnDrag = false;
        initScrollXOffset = 0;
        if (!GlobalVariablesController.sineMode)
        {
            if (!GlobalVariablesController.reversedMode)
            {
                GlobalVariablesController.stepKd = scrollStepTempKd;
            }
            else
            {
                GlobalVariablesController.stepKdReverse = scrollStepTempKd;
            }
            scrollStepTempKd = 0;
        }
        else
        {
            if (!GlobalVariablesController.reversedMode)
            {
                GlobalVariablesController.sineKd = scrollSineTempKd;
            }
            else
            {
                GlobalVariablesController.sineKdReverse = scrollSineTempKd;
            }
            scrollSineTempKd = 0;
        }
        if (Camera.main is null) return;
        kdScrollBar.transform.localPosition = new Vector3(scrollInitX, kdScrollBar.transform.localPosition.y,
            kdScrollBar.transform.localPosition.z);
        onEdit = false;
    }
    
    private void OnMouseUp()
    {
        mouseArrow.SetActive(false);
        Cursor.visible = true;
        if (!GlobalVariablesController.reversedMode)
        {
            GlobalVariablesController.stepKp += stepTempKp;
            GlobalVariablesController.stepKi += stepTempKi;
            GlobalVariablesController.stepKd += stepTempKd;
            GlobalVariablesController.sineKp += sineTempKp;
            GlobalVariablesController.sineKi += sineTempKi;
            GlobalVariablesController.sineKd += sineTempKd;
        }
        else
        {
            GlobalVariablesController.stepKpReverse += stepTempKp;
            GlobalVariablesController.stepKiReverse += stepTempKi;
            GlobalVariablesController.stepKdReverse += stepTempKd;
            GlobalVariablesController.sineKpReverse += sineTempKp;
            GlobalVariablesController.sineKiReverse += sineTempKi;
            GlobalVariablesController.sineKdReverse += sineTempKd;
        }
        initMousePositionX = 0;
        initMousePositionY = 0;
        stepTempKp = 0;
        stepTempKi = 0;
        stepTempKd = 0;
        sineTempKp = 0;
        sineTempKi = 0;
        sineTempKd = 0;
        scrollStepTempKp = 0;
        scrollStepTempKi = 0;
        scrollStepTempKd = 0;
        scrollSineTempKp = 0;
        scrollSineTempKi = 0;
        scrollSineTempKd = 0;
        onDrag = false;
        onEdit = false;
        deltaX = 0;
    }
    private static float FindScale(int scaleType)
    {
        return scaleType switch
        {
            1 => GlobalVariablesController.scaleI,
            2 => GlobalVariablesController.scaleII,
            3 => GlobalVariablesController.scaleIII,
            4 => GlobalVariablesController.scaleIV,
            _ => 0
        };
    }
}
