using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
public class ParametersSettingHelper : MonoBehaviour
{
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
    }
    // Update is called once per frame
    private void Update()
    {
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
                var dist = Camera.main.ScreenToWorldPoint(Input.mousePosition).x + initScrollXOffset;
                if (dist >= 7.55)
                {
                    dist = (float) 7.55;
                }
                else if (dist <= 4.17)
                {
                    dist = (float) 4.17;
                }
                kpScrollBar.transform.position =
                    new Vector3(dist,
                        kpScrollBar.transform.position.y,
                        kpScrollBar.transform.position.z);
                if (!GlobalVariablesController.sineMode)
                {
                    var delta = (dist - scrollInitX) * FindScale(GlobalVariablesController.stepKpScaleType);
                    GlobalVariablesController.stepKp = scrollStepTempKp + delta;
                }
                else
                {
                    var delta = (dist - scrollInitX) * FindScale(GlobalVariablesController.sineKpScaleType);
                    GlobalVariablesController.sineKp = scrollSineTempKp + delta;
                }
            }
            kpValue.text = !GlobalVariablesController.sineMode
                ? (GlobalVariablesController.stepKp).ToString(CultureInfo.InvariantCulture)
                : (GlobalVariablesController.sineKp).ToString(CultureInfo.InvariantCulture);
        }
        if (kiScrollBarOnDrag)
        {
            if (Camera.main is { })
            {
                var dist = Camera.main.ScreenToWorldPoint(Input.mousePosition).x + initScrollXOffset;
                if (dist >= 7.55)
                {
                    dist = (float) 7.55;
                }
                else if (dist <= 4.17)
                {
                    dist = (float) 4.17;
                }
                kiScrollBar.transform.position =
                    new Vector3(dist,
                        kiScrollBar.transform.position.y,
                        kiScrollBar.transform.position.z);
                if (!GlobalVariablesController.sineMode)
                {
                    var delta = (dist - scrollInitX) * FindScale(GlobalVariablesController.stepKiScaleType);
                    GlobalVariablesController.stepKi = scrollStepTempKi + delta;
                }
                else
                {
                    var delta = (dist - scrollInitX) * FindScale(GlobalVariablesController.sineKiScaleType);
                    GlobalVariablesController.sineKi = scrollSineTempKi + delta;
                }
            }
            kiValue.text = !GlobalVariablesController.sineMode
                ? (GlobalVariablesController.stepKi).ToString(CultureInfo.InvariantCulture)
                : (GlobalVariablesController.sineKi).ToString(CultureInfo.InvariantCulture);
        }
        if (kdScrollBarOnDrag)
        {
            if (Camera.main is { })
            {
                var dist = Camera.main.ScreenToWorldPoint(Input.mousePosition).x + initScrollXOffset;
                if (dist >= 7.55)
                {
                    dist = (float) 7.55;
                }
                else if (dist <= 4.17)
                {
                    dist = (float) 4.17;
                }
                kdScrollBar.transform.position =
                    new Vector3(dist,
                        kdScrollBar.transform.position.y,
                        kdScrollBar.transform.position.z);
                if (!GlobalVariablesController.sineMode)
                {
                    var delta = (dist - scrollInitX) * FindScale(GlobalVariablesController.stepKdScaleType);
                    GlobalVariablesController.stepKd = scrollStepTempKd + delta;
                }
                else
                {
                    var delta = (dist - scrollInitX) * FindScale(GlobalVariablesController.sineKdScaleType);
                    GlobalVariablesController.sineKd = scrollSineTempKd + delta;
                }
            }
            kdValue.text = !GlobalVariablesController.sineMode
                ? (GlobalVariablesController.stepKd).ToString(CultureInfo.InvariantCulture)
                : (GlobalVariablesController.sineKd).ToString(CultureInfo.InvariantCulture);
        }

        if (!onEdit)
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
        if (!onDrag) return;
        Cursor.visible = false;
        mouseArrow.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
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
            GlobalVariablesController.stepKp = Convert.ToSingle(kpValue.text);
            GlobalVariablesController.stepKi = Convert.ToSingle(kiValue.text);
            GlobalVariablesController.stepKd = Convert.ToSingle(kdValue.text);
        }
        else
        {
            GlobalVariablesController.sineKp = Convert.ToSingle(kpValue.text);
            GlobalVariablesController.sineKi = Convert.ToSingle(kiValue.text);
            GlobalVariablesController.sineKd = Convert.ToSingle(kdValue.text);
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
        }
        else
        {
            GlobalVariablesController.sineKpBreakPoint = GlobalVariablesController.sineKp;
            GlobalVariablesController.sineKiBreakPoint = GlobalVariablesController.sineKi;
            GlobalVariablesController.sineKdBreakPoint = GlobalVariablesController.sineKd;
        }
        onEdit = false;
    }
    public void LoadBreakPoint()
    {
        if (!GlobalVariablesController.sineMode)
        {
            GlobalVariablesController.stepKp = GlobalVariablesController.stepKpBreakPoint;
            GlobalVariablesController.stepKi = GlobalVariablesController.stepKiBreakPoint;
            GlobalVariablesController.stepKd = GlobalVariablesController.stepKdBreakPoint;
        }
        else
        {
            GlobalVariablesController.sineKp = GlobalVariablesController.sineKpBreakPoint;
            GlobalVariablesController.sineKi = GlobalVariablesController.sineKiBreakPoint;
            GlobalVariablesController.sineKd = GlobalVariablesController.sineKdBreakPoint;
        }
        onEdit = false;
    }

    public void KpScrollBarMouseDown()
    {
        kpScrollBarOnDrag = true;
        if (Camera.main is null) return;
        if (!GlobalVariablesController.sineMode)
        {
            scrollStepTempKp = GlobalVariablesController.stepKp;
        }
        else
        {
            scrollSineTempKp = GlobalVariablesController.sineKp;
        }
        var position = kpScrollBar.transform.position;
        scrollInitX = position.x;
        initScrollXOffset = position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
    }
    public void KpScrollBarMouseUp()
    {
        kpScrollBarOnDrag = false;
        initScrollXOffset = 0;
        if (!GlobalVariablesController.sineMode)
        {
            GlobalVariablesController.stepKp = scrollStepTempKp;
            scrollStepTempKp = 0;
        }
        else
        {
            GlobalVariablesController.sineKp = scrollSineTempKp;
            scrollSineTempKp = 0;
        }
        if (Camera.main is null) return;
        kpScrollBar.transform.position = new Vector3(scrollInitX, kpScrollBar.transform.position.y,
            kpScrollBar.transform.position.z);
        onEdit = false;
    }
    
    public void KiScrollBarMouseDown()
    {
        kiScrollBarOnDrag = true;
        if (Camera.main is null) return;
        if (!GlobalVariablesController.sineMode)
        {
            scrollStepTempKi = GlobalVariablesController.stepKi;
        }
        else
        {
            scrollSineTempKi = GlobalVariablesController.sineKi;
        }
        var position = kiScrollBar.transform.position;
        scrollInitX = position.x;
        initScrollXOffset = position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
    }
    public void KiScrollBarMouseUp()
    {
        kiScrollBarOnDrag = false;
        initScrollXOffset = 0;
        if (!GlobalVariablesController.sineMode)
        {
            GlobalVariablesController.stepKi = scrollStepTempKi;
            scrollStepTempKi = 0;
        }
        else
        {
            GlobalVariablesController.sineKi = scrollSineTempKi;
            scrollSineTempKi = 0;
        }
        if (Camera.main is null) return;
        kiScrollBar.transform.position = new Vector3(scrollInitX, kiScrollBar.transform.position.y,
            kiScrollBar.transform.position.z);
        onEdit = false;
    }
    
    public void KdScrollBarMouseDown()
    {
        kdScrollBarOnDrag = true;
        if (Camera.main is null) return;
        if (!GlobalVariablesController.sineMode)
        {
            scrollStepTempKd = GlobalVariablesController.stepKd;
        }
        else
        {
            scrollSineTempKd = GlobalVariablesController.sineKd;
        }
        var position = kdScrollBar.transform.position;
        scrollInitX = position.x;
        initScrollXOffset = position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
    }
    public void KdScrollBarMouseUp()
    {
        kdScrollBarOnDrag = false;
        initScrollXOffset = 0;
        if (!GlobalVariablesController.sineMode)
        {
            GlobalVariablesController.stepKd = scrollStepTempKd;
            scrollStepTempKd = 0;
        }
        else
        {
            GlobalVariablesController.sineKd = scrollSineTempKd;
            scrollSineTempKd = 0;
        }
        if (Camera.main is null) return;
        kdScrollBar.transform.position = new Vector3(scrollInitX, kdScrollBar.transform.position.y,
            kdScrollBar.transform.position.z);
        onEdit = false;
    }
    
    private void OnMouseUp()
    {
        mouseArrow.SetActive(false);
        Cursor.visible = true;
        GlobalVariablesController.stepKp += stepTempKp;
        GlobalVariablesController.stepKi += stepTempKi;
        GlobalVariablesController.stepKd += stepTempKd;
        GlobalVariablesController.sineKp += sineTempKp;
        GlobalVariablesController.sineKi += sineTempKi;
        GlobalVariablesController.sineKd += sineTempKd;
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
