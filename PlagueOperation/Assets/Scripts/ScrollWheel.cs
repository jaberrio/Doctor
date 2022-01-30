using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrollWheel : MonoBehaviour
{

    private GameObject input;
    // Start is called before the first frame update
    void Start()
    {
        input = GameObject.Find("Input");
    }

    // Update is called once per frame
    void Update()
    {
        if (input.GetComponent<inputProcessor>().dial_press == true)
        {
            if(SceneManager.GetActiveScene().name == "StartScreen")
                SceneManager.LoadScene("IntroDialogueScreen1");
            else if(SceneManager.GetActiveScene().name == "IntroDialogueScreen1")
                SceneManager.LoadScene("IntroDialogueScreen2");
            else if (SceneManager.GetActiveScene().name == "IntroDialogueScreen2")
                SceneManager.LoadScene("GameScene");
            else if (SceneManager.GetActiveScene().name == "IntroDialogueScreen2")
                SceneManager.LoadScene("GameScene");
            else if (SceneManager.GetActiveScene().name == "TransitionLoseScene")
                SceneManager.LoadScene("GameScene");
            else if (SceneManager.GetActiveScene().name == "TransitionWinScene")
                SceneManager.LoadScene("GameScene");
            else if (SceneManager.GetActiveScene().name == "EndLoseScene")
                SceneManager.LoadScene("StartScreen");
            else if (SceneManager.GetActiveScene().name == "EndWinScene")
                SceneManager.LoadScene("FinalScene");
            input.GetComponent<inputProcessor>().dial_press = false;
        }
    }
}
