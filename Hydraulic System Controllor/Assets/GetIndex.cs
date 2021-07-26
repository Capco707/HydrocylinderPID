using UnityEngine;
using UnityEngine.EventSystems;
public class GetIndex : MonoBehaviour,ISelectHandler
{
    public GameObject parentHelper;
    public int index;

    public void OnSelect(BaseEventData eventData)
    {
        parentHelper.GetComponent<InputNavigator>().index = index;
    }
}