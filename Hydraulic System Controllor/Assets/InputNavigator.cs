using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InputNavigator : MonoBehaviour
{
    private EventSystem es;
    public InputField[] IfArray;
    public int index = 0;
    public GameObject field1;
    void Start() {
        es = EventSystem.current;
        es.SetSelectedGameObject(IfArray[index].gameObject,new BaseEventData(es));
    }

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Tab)) return;
        switch (index)
        {
            case 0:
                index = field1.activeInHierarchy ? 1 : 0;
                break;
            case 1:
                index = 0;
                break;
            case 2:
            case 3:
                index++;
                break;
            case 4:
                index = 2;
                break;
        }
        es.SetSelectedGameObject(IfArray[index].gameObject, new BaseEventData(es));
    }
}