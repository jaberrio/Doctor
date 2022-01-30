using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    private GameObject gameManager;
    public void switchScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
        if (scene_name == "StartScreen")
        {
            gameManager = GameObject.Find("GameManager");
            Destroy(gameManager);
        }
    }
}
