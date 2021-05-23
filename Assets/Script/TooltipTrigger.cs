using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static float delay = 2f;
    public string header;
    [TextArea(15, 20)]
    public string content;

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(DelayCheck());
        TooltipSystem.Show(content,header);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }

    public IEnumerator DelayCheck()
    {
        yield return new WaitForSeconds(2f);
    }
}
