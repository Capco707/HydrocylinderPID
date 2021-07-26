using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class DrawLine : MonoBehaviour
{
    
    private RectTransform _parentRectTransform;
    
    private double _scroll = 0;
    public List<double> inputArray = new List<double>();
    public List<double> inputArray2 = new List<double>();
    public int totalSegment = 2048;
    public int totalYSegment = 512;
    public GameObject imgLine;
    public GameObject imgLine2;
    public GameObject imgGrid;
    public GameObject imgCursor;
    private List<GameObject>_imgList = new List<GameObject>();
    private List<RectTransform> _rectTransforms = new List<RectTransform>();
    private List<GameObject>_imgList2 = new List<GameObject>();
    private List<RectTransform> _rectTransforms2 = new List<RectTransform>();
    private List<GameObject> _xGrid = new List<GameObject>();
    private List<RectTransform> _xTransforms = new List<RectTransform>();
    private List<GameObject> _yGrid = new List<GameObject>();
    private List<RectTransform> _yTransforms = new List<RectTransform>();
    public GameObject parentCanvas;
    private double _tempXScale = 10000;
    private double _tempYScale = 800;
    private Rect _parentRect;
    private Vector3 _currParentPos;
    private int _activeFlag = 2;
    public int maxVoltageInput = 10000;
    public int minVoltageInput = 0;
    public int gridDensityFactor = 100;
    //The position of the Cursor on the graph
    public int CursorX;
    public double CursorY;
    public bool isCursorActive = false;
    private GameObject _cursorX, _cursorY,_cursorY2;
    private RectTransform _transformX, _transformY, _transformY2;

    public GameObject angleImgX;
    public GameObject angleImgY, angleImgY2;
    private bool mouseInRegion = false;
    public Vector3 mouseDownPos;
    public Vector3 mouseUpPos;
    
    // Start is called before the first frame update
    void Start()
    {
        _parentRectTransform = parentCanvas.GetComponent<RectTransform>();
        _parentRect = parentCanvas.GetComponent<RectTransform>().rect;
        _tempXScale = _parentRect.width;
        _tempYScale = _parentRect.height;
        //TODO: Uncomment this code
        //for (int i = 0; i <= totalSegment; i++)
        //    inputArray.Add(-2);
        //TODO: delete this code
        for (int i = 0; i < totalSegment; i++)
        {
            inputArray.Add(Math.Sin(((float)(i-1))/20));
            inputArray2.Add(Math.Cos(((float)(i-1))/20));
        }

        for (int i = 0; i <= totalSegment; i++)
        {
            GlobalVariablesController.voltageArray.Add(0);
            GlobalVariablesController.voltageArray2.Add(0);
        }
        DrawXGrid();
        DrawYGrid();
        DrawCursor();
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariablesController.startUpdate)
        {
            voltage2input();
        }
        _parentRect = _parentRectTransform.rect;
        _scroll = Input.GetAxis("Mouse ScrollWheel");
        //Set pivot
        //if (_scroll != 0)
        //{
        //    _currParentPos = parentCanvas.transform.position;
        //    float x = Input.mousePosition.x - _currParentPos.x;
        //    _parentRectTransform.pivot = new Vector2((float)(x/_parentRect.width + 0.5), (float)0.5);
        //}
        if (_activeFlag == 0)
            _activeFlag++;
        if (_activeFlag == 1)
        {
            _activeFlag = 2;
            foreach (var img in _imgList)
            {
                img.SetActive(true);
            }
            foreach (var img in _imgList2)
            {
                img.SetActive(true);
            }
        }

        if (_scroll != 0 && mouseInRegion)
        {
            _activeFlag = 0;
            foreach (var img in _imgList)
            {
                img.SetActive(false);
            }

            foreach (var img in _imgList2)
            {
                img.SetActive(false);
            }

            _activeFlag = 0;
            //Scroll Mouse to zoom
            
            
            
        
        //Move to the exact position
        
        
            _currParentPos = parentCanvas.transform.position;
            if (GlobalVariablesController.xScrollable && (1+_scroll)*_tempXScale<=150000 && (1+_scroll)*_tempXScale>1000)
            {
                _tempXScale *= (_scroll+1);
                float x = Input.mousePosition.x - _currParentPos.x;
                _currParentPos.x -= (float)_scroll*x;
            }

            if (GlobalVariablesController.yScrollable && (1+_scroll)*_tempYScale<=50000 && (1+_scroll)*_tempYScale>270)
            {
                _tempYScale *= (_scroll+1); 
                var y = Input.mousePosition.y - _currParentPos.y;
                _currParentPos.y -= (float)_scroll*y;
            }
            _parentRectTransform.sizeDelta = new Vector2((float)_tempXScale, (float)_tempYScale);
            parentCanvas.transform.position = _currParentPos;
        }
        if (_imgList.Count >= 500)
            UpdateLine();
        UpdateGrid(_activeFlag==2);
        UpdateCursor();
        if (!mouseInRegion) return;
        if (Input.GetMouseButtonDown(0))
        {
            ScreenOnMouseDown();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            ScreenOnMouseUp();
        }
    }

    //If voltage is 0, then return -2
    //Else return corresponding -1 to 1
    private void voltage2input()
    {
        if (GlobalVariablesController.paused)
        {
            for (var i = 0; i < totalSegment; i++)
            {
                if (GlobalVariablesController.savedVoltageArray[i] != 0)
                    inputArray[i] = (double) (GlobalVariablesController.savedVoltageArray[i] - minVoltageInput) /
                                    (maxVoltageInput - minVoltageInput) * 2 -
                                    1;
                else
                    inputArray[i] = -2;
                if (GlobalVariablesController.savedVoltageArray2[i] != 0)
                    inputArray2[i] = (double) (GlobalVariablesController.savedVoltageArray2[i] - minVoltageInput) /
                                     (maxVoltageInput - minVoltageInput) * 2 -
                                     1;
                else
                    inputArray2[i] = -2;
            }
        }
        else
        {
            for (var i = 0; i < totalSegment; i++)
            {
                if (GlobalVariablesController.voltageArray[i] != 0)
                    inputArray[i] = (double) (GlobalVariablesController.voltageArray[i] - minVoltageInput) /
                                    (maxVoltageInput - minVoltageInput) * 2 -
                                    1;
                else
                    inputArray[i] = -2;
                if (GlobalVariablesController.voltageArray2[i] != 0)
                    inputArray2[i] = (double) (GlobalVariablesController.voltageArray2[i] - minVoltageInput) /
                                     (maxVoltageInput - minVoltageInput) * 2 -
                                     1;
                else
                    inputArray2[i] = -2;
            }
        }
        
    }

    private void disableAll()
    {
        for (var i = 0; i < inputArray.Count; i++)
        {
            inputArray[i] = -2;
        }
    }

    private GameObject DrawSingleLine(bool is1, double x1, double y1, double x2, double y2)
    {
        var w = x2 - x1;
        var h = y2 - y1;
        var tempImg = Instantiate(is1 ? imgLine:imgLine2);
        tempImg.transform.localScale = parentCanvas.transform.lossyScale;
        //tempImg.transform.localScale = lossyScale;
        RectTransform rectTransform = tempImg.GetComponent<RectTransform>();
        tempImg.GetComponent<Transform>().SetParent(parentCanvas.GetComponent<Transform>(), true);
        rectTransform.sizeDelta = new Vector2((float)Math.Sqrt(w*w + h*h), 0);
        rectTransform.Rotate((float)0.0, (float)0.0, (float)(Math.Atan(h / w) / Math.PI * 180));
        rectTransform.anchoredPosition = new Vector2((float)(x1+x2)/2, (float)(y1+y2)/2);
        return tempImg;
    }
    
    public void Draw()
    {
        GlobalVariablesController.consoleText += "Auto Setting...\n";
        var totalWidth = _parentRect.width;
        for (var i = 0; i < totalSegment; i++)
        {
            _imgList.Add(DrawSingleLine(true, totalWidth / totalSegment * (i - 1) - totalWidth / 2, 0,
                totalWidth / totalSegment * (i) - totalWidth / 2, 0));
            _rectTransforms.Add(_imgList[_imgList.Count - 1].GetComponent<RectTransform>());
            _imgList2.Add(DrawSingleLine(false, totalWidth / totalSegment * (i - 1) - totalWidth / 2, 0,
                totalWidth / totalSegment * (i) - totalWidth / 2, 0));
            _rectTransforms2.Add(_imgList2[_imgList2.Count - 1].GetComponent<RectTransform>());
        }
    }

    private void UpdateSingleLine(RectTransform lineTrans, double x1, double y1, double x2, double y2)
    {
        var w = x2 - x1;
        var h = y2 - y1;
        lineTrans.sizeDelta = new Vector2((float) Math.Sqrt(w * w + h * h), 2);
        lineTrans.Rotate((float) 0.0, (float) 0.0, (float) (Math.Atan(h / w) / Math.PI * 180)-lineTrans.rotation.eulerAngles.z);
        lineTrans.anchoredPosition = new Vector2((float) (x1 + x2) / 2, (float) (y1 + y2) / 2);
    }
    public void UpdateLine()
    {
        
        float totalWidth = _parentRect.width;
        
        for (int i = 1; i < totalSegment; i++)
        {
            if (inputArray[i] == -2)
                _imgList[i-1].SetActive(false);
            else
            {
                if (_activeFlag == 2)
                    _imgList[i-1].SetActive(true);
                UpdateSingleLine(_rectTransforms[i - 1], totalWidth / totalSegment * (i - 1) - totalWidth / 2,
                    inputArray[i - 1] / 2 * _parentRect.height, totalWidth / totalSegment * (i) - totalWidth / 2,
                    inputArray[i] / 2 * _parentRect.height);
            }
            
            if (inputArray2[i] == -2 || !GlobalVariablesController.referenceOn)
                _imgList2[i-1].SetActive(false);
            else
            {
                if (_activeFlag == 2)
                    _imgList2[i-1].SetActive(true);
                UpdateSingleLine(_rectTransforms2[i - 1], totalWidth / totalSegment * (i - 1) - totalWidth / 2,
                    inputArray2[i - 1] / 2 * _parentRect.height, totalWidth / totalSegment * (i) - totalWidth / 2,
                    inputArray2[i] / 2 * _parentRect.height);
            }
        }
    }
    private GameObject DrawSingleGrid(double x, bool isX)
    {
        var tempGrid = Instantiate(imgGrid);
        tempGrid.transform.localScale = parentCanvas.transform.lossyScale;
        //tempGrid.transform.localScale = lossyScale;
        RectTransform rectTransform = tempGrid.GetComponent<RectTransform>();
        tempGrid.GetComponent<Transform>().SetParent(parentCanvas.GetComponent<Transform>(), true);
        rectTransform.sizeDelta = isX ? new Vector2(1, _parentRect.height) : new Vector2(_parentRect.width, 1);
        rectTransform.anchoredPosition = isX ? new Vector2((float)x, 0) : new Vector2(0, (float)x);
        return tempGrid;
    }
    
    private void DrawXGrid()
    {
        for (int i = 0; i <= totalSegment; i++)
        {
            _xGrid.Add(DrawSingleGrid(_parentRect.width/totalSegment*i - _parentRect.width/2, true));
            _xTransforms.Add(_xGrid[_xGrid.Count - 1].GetComponent<RectTransform>());
        }
    }
    
    private void DrawYGrid()
    {
        for (int i = 0; i <= totalYSegment; i++)
        {
            _yGrid.Add(DrawSingleGrid(_parentRect.height/totalYSegment*i - _parentRect.height/2, false));
            _yTransforms.Add(_yGrid[_yGrid.Count - 1].GetComponent<RectTransform>());
        }
    }
    
    private void UpdateSingleGrid(GameObject line, RectTransform lineTrans, double x, bool isX)
    {
        lineTrans.sizeDelta = isX ? new Vector2(1, _parentRect.height) : new Vector2(_parentRect.width, 1);
        lineTrans.anchoredPosition = isX ? new Vector2((float)x, 0) : new Vector2(0, (float)x);
    }
    
    private void UpdateGrid(bool canBeActive)
    {
        int scaleFactorX = (int) Math.Pow(2,
            (int) Math.Log(gridDensityFactor / (_parentRectTransform.rect.width / totalSegment), 2));
        int scaleFactorY = (int) Math.Pow(2,
            (int) Math.Log(gridDensityFactor / (_parentRectTransform.rect.height / totalYSegment), 2));
        for (int i = 0; i <= totalSegment; i++)
        {
            if (i % scaleFactorX == 0)
            {
                UpdateSingleGrid(_xGrid[i], _xTransforms[i],
                    _parentRect.width / totalSegment * i - _parentRect.width / 2, true);
                if (canBeActive)
                    _xGrid[i].SetActive(true);
                else
                    _xGrid[i].SetActive(false);
            }
            else
                _xGrid[i].SetActive(false);
        }
        
        for (int i = 0; i <= totalYSegment; i++)
        {
            if (i % scaleFactorY == 0)
            {
                UpdateSingleGrid(_yGrid[i], _yTransforms[i],
                    _parentRect.height / totalYSegment * i - _parentRect.height / 2, false);
                if (canBeActive)
                    _yGrid[i].SetActive(true);
                else
                    _yGrid[i].SetActive(false);
            }
            else
                _yGrid[i].SetActive(false);
        }
    }

    public void GetRelatedPos()
    {
        GlobalVariablesController.measuring = true;
        _currParentPos = parentCanvas.transform.position;
        var x = Input.mousePosition.x - _currParentPos.x;
        var i = (int) Math.Round(
            x / parentCanvas.transform.lossyScale.x / _parentRect.width * totalSegment + (float) totalSegment / 2, 0);
        CursorX = i;
        GlobalVariablesController.measuredTime = CursorX;
        GlobalVariablesController.referenceTime = CursorX;
        //TODO: Uncomment this line
        
        CursorY = GlobalVariablesController.voltageArray[i];
        isCursorActive = true;
    }

    private void DrawCursor()
    {
        _cursorX = Instantiate(imgCursor);
        _cursorX.transform.localScale = parentCanvas.transform.lossyScale;
        _transformX = _cursorX.GetComponent<RectTransform>();
        _cursorX.GetComponent<Transform>().SetParent(parentCanvas.GetComponent<Transform>(), true);
        _transformX.sizeDelta = new Vector2(2, _parentRect.height);
        _transformX.anchoredPosition = new Vector2(0, 0);
        
        _cursorY = Instantiate(imgCursor);
        _cursorY.transform.localScale = parentCanvas.transform.lossyScale;
        _transformY = _cursorY.GetComponent<RectTransform>();
        _cursorY.GetComponent<Transform>().SetParent(parentCanvas.GetComponent<Transform>(), true);
        _transformY.sizeDelta = new Vector2(_parentRect.width, 2);
        _transformY.anchoredPosition = new Vector2(0, 0);
        
        _cursorY2 = Instantiate(imgCursor);
        _cursorY2.transform.localScale = parentCanvas.transform.lossyScale;
        _transformY2 = _cursorY2.GetComponent<RectTransform>();
        _cursorY2.GetComponent<Transform>().SetParent(parentCanvas.GetComponent<Transform>(), true);
        _transformY2.sizeDelta = new Vector2(_parentRect.width, 2);
        _transformY2.anchoredPosition = new Vector2(0, 0);
    }

    private void UpdateCursor()
    {
        _cursorX.SetActive(isCursorActive && GlobalVariablesController.measuring);
        _transformX.sizeDelta = new Vector2(2, _parentRect.height);
        _transformX.anchoredPosition = new Vector2(_parentRect.width/totalSegment*CursorX - _parentRect.width/2, 0);
        var position = angleImgX.transform.position;
        position = new Vector3(_cursorX.transform.position.x, position.y,
            position.z);
        angleImgX.transform.position = position;

        _cursorY.SetActive(isCursorActive && GlobalVariablesController.measuring);
        _transformY.sizeDelta = new Vector2(_parentRect.width, 2);
        _transformY.anchoredPosition = new Vector2(0, (float)inputArray[CursorX]/2*_parentRect.height);
        var positionY = angleImgY.transform.position;
        positionY = new Vector3(positionY.x, _cursorY.transform.position.y,
            positionY.z);
        angleImgY.transform.position = positionY;

        _cursorY2.SetActive(isCursorActive && GlobalVariablesController.measuring &&
                            GlobalVariablesController.referenceOn);
        _transformY2.sizeDelta = new Vector2(_parentRect.width, 2);
        _transformY2.anchoredPosition = new Vector2(0, (float)inputArray2[CursorX]/2*_parentRect.height);
        
        var positionY2 = angleImgY2.transform.position;
        positionY2 = new Vector3(positionY2.x, _cursorY2.transform.position.y,
            positionY2.z);
        angleImgY2.transform.position = positionY2;
        
        GlobalVariablesController.measuredAmp = GlobalVariablesController.paused
            ? GlobalVariablesController.savedVoltageArray[CursorX]
            : GlobalVariablesController.voltageArray[CursorX];
        GlobalVariablesController.referenceAmp = GlobalVariablesController.paused
            ? GlobalVariablesController.savedVoltageArray2[CursorX]
            : GlobalVariablesController.voltageArray2[CursorX];
    }

    public void ScreenOnMouseEnter()
    {
        mouseInRegion = true;
    }
    public void ScreenOnMouseExit()
    {
        mouseInRegion = false;
    }

    private void ScreenOnMouseDown()
    {
        
        mouseDownPos = Input.mousePosition;
    }
    private void ScreenOnMouseUp()
    {
        mouseUpPos = Input.mousePosition;
        if (mouseDownPos == mouseUpPos)
        {
            GetRelatedPos();
        }
    }

}
