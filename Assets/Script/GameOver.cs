using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text waveText;
    public string menuSceneName = "MainMenu";
    public SceneFader sceneFader;
    //public int wavesCount;
    private void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    public void Retry()
    {
        FindObjectOfType<PlayerStats>().Initialisation();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name,false);
    }
    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName,false);
    }

    IEnumerator AnimateText()
    {
        waveText.text = "0";
        int number = 0;

        yield return new WaitForSeconds(.7f);
        while (number < PlayerStats.waves) 
        {
            number++;
            waveText.text = number.ToString();
            yield return new WaitForSeconds(.05f);
        }
    }
}
