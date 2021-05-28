using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayUI : MonoBehaviour
{
    public GameObject[] page;
    // Start is called before the first frame update

    public void ToPage(int index)
    {
        
        for(int i = 0; i < page.Length; i++)
        {
            if( index == i )
            {
                page[i].SetActive(true);
            }
            else
            {
                page[i].SetActive(false);
            }
        }
    }

    public void ToPrevPage()
    {
        int index = 0;
        for(int i = 0; i < page.Length; i++)
        {
            if(page[i].activeSelf)
            {
                index = i;
                break;
            }
        }
        if(index <= 0)
        {
            Debug.Log("Already the min page");
        }
        else
        {
            page[index].SetActive(false);
            page[index - 1].SetActive(true);
        } 
    }
    
    public void ToNextPage()
    {
        int index = 0;
        for(int i = 0; i < page.Length; i++)
        {
            if(page[i].activeSelf)
            {
                index = i;
                Debug.Log(index + "/" + page.Length);
                break;
            }
        }

        if (index != page.Length - 1)

        {
            page[index].SetActive(false);
            page[index + 1].SetActive(true);
        }
        else
        {
            Debug.Log("already the max page");
        }
    }
    
}
