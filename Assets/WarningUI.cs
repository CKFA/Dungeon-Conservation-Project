using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningUI : MonoBehaviour
{
    public Text warningText;
    public string warningContent;
    public GameManager gameManager;
    public Image backgroundColour;
    public Color warningColour;
    public Color hintColour;
    [HideInInspector]
    public bool isWarning;
    // Start is called before the first frame update

    private void OnEnable()
    {
        if(isWarning)
        {
            backgroundColour.color = warningColour;
        }
        else
        {
            backgroundColour.color = hintColour;
        }
        warningText.text = warningContent;
        StartCoroutine(RunTime());
    }

    IEnumerator RunTime()
    {
        yield return new WaitForSeconds(2f);
        gameManager.HideWarningNotice();
        Debug.Log("Hide notice");
        yield break;
    }
}
