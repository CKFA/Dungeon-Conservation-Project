using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve fadeCurve;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene, bool sync)
    {
        StartCoroutine(FadeOut(scene,sync));
    }
    public void FadeTo(int scene,bool sync)
    {
        StartCoroutine(FadeOut(scene,sync));
    }

    IEnumerator FadeIn()
    {
        float t = 1f;
        while(t > 0f)
        {
            t -= Time.deltaTime;
            float a = fadeCurve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0; // skip to the next frame
        }
    }
    IEnumerator FadeOut(string scene, bool sync)
    {
        IsSceneExist(scene);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = fadeCurve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0; // skip to the next frame
        }

        if(sync)
        {
            var progress = SceneManager.LoadSceneAsync(scene,LoadSceneMode.Additive);
            while(!progress.isDone)
            {
                yield return null;
            }

            Debug.Log("Level loaded");
            yield break;
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
    }
    
    IEnumerator FadeOut(int scene, bool sync)
    {
        IsSceneExist(scene);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = fadeCurve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0; // skip to the next frame
        }

        if (sync)
        {
            var progress = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            while (!progress.isDone)
            {
                yield return null;
            }
           

            Debug.Log("Level loaded");
            yield break;
        }
        else
        SceneManager.LoadScene(scene);
    }
    
    bool IsSceneExist(int scene)
    {
        if (SceneManager.sceneCount > 0)
        {
            for(int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene checkingScene = SceneManager.GetSceneAt(i);
                if(SceneManager.GetSceneByBuildIndex(scene) == checkingScene)
                {
                    return false;
                }
            }
        }
        return true;
    }
    
    bool IsSceneExist(string scene)
    {
        if (SceneManager.sceneCount > 0)
        {
            for(int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene checkingScene = SceneManager.GetSceneAt(i);
                if(SceneManager.GetSceneByName(scene) == checkingScene)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
