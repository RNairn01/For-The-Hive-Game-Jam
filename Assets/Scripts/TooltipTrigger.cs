using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private static LTDescr delay;
    
    public string header;
    [Multiline()]
    public string content;

    public void OnPointerEnter(PointerEventData eventData)
    {
        /*try
        {
            header = GetComponentInParent<EventDisplay>().hiveEvent.eventName;
            content = GetComponentInParent<EventDisplay>().hiveEvent.eventMouseoverText;
        } catch { }*/

        
        try
        {
            if (string.IsNullOrEmpty(GetComponentInParent<EventDisplay>().hiveEvent.eventName) == false) header = GetComponentInParent<EventDisplay>().hiveEvent.eventName;

            if (string.IsNullOrEmpty(GetComponentInParent<EventDisplay>().hiveEvent.eventMouseoverText) == false) content = GetComponentInParent<EventDisplay>().hiveEvent.eventMouseoverText;
        } catch { }
        
        
        /*
        try
        {
            if (string.IsNullOrEmpty(GetComponent<EventDisplay>().hiveEvent.eventName) == false) header = GetComponent<EventDisplay>().hiveEvent.eventName;

            if (string.IsNullOrEmpty(GetComponent<EventDisplay>().hiveEvent.eventMouseoverText) == false) content = GetComponent<EventDisplay>().hiveEvent.eventMouseoverText;
        }
        catch { }
        */
        delay = LeanTween.delayedCall(0.2f, () =>
        {
            TooltipSystem.Show(content, header);
        });
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(delay.uniqueId);
        TooltipSystem.Hide();
    }
}
