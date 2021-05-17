using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameIsOver;
    public GameObject gameOverUI;
    public BuildManager buildManager;
    // Start is called before the first frame update
    void Start()
    {
        gameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsOver)
            return;
        if (PlayerStats.hp <= 0)
        {
            EndGame();
        }

        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            EndGame();
        }

        if(Input.GetMouseButtonDown(1))
        {
            buildManager.DeselectNode();
        }
    }

    void EndGame()
    {
        gameIsOver = true;
        gameOverUI.SetActive(true);
    }
}
