using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public void fullScreenModeChange()
    {
        SceneManager.LoadScene(GlobalVariablesController.fullScreen ? 0 : 1);
        GlobalVariablesController.fullScreen = !GlobalVariablesController.fullScreen;
    }
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Input.GetKey(KeyCode.Escape)) return;
        if (SceneManager.GetActiveScene().buildIndex == 0) return;
        SceneManager.LoadScene(0);
        GlobalVariablesController.fullScreen = false;
    }
}
