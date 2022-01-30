using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class Clock : MonoBehaviour
{
    public TMP_Text timer;
    public float startCount;
    public float dangerZone;
    private float countDown;
    private bool levelOver = false;
    public float startWait;
    public TMP_Text startTimer;
    public TMP_Text instructions;
    public TMP_Text Dialouge;
    public bool startTimerFinish = true;
    private GameObject gameManager;
    private Level currentLevel;
    private bool won;
    public GameObject patient;
    public GameObject textBox;
    private GameObject input;
    // Start is called before the first frame update
    void Start()
    {
        countDown = startCount;
        UpdateText();
        gameManager = GameObject.Find("GameManager");
        currentLevel = gameManager.GetComponent<GameManager>().getCurrentLevel();
        instructions.text = currentLevel.getInstructions();
        patient.GetComponent<Image>().sprite = currentLevel.getPatient();
        textBox.GetComponent<Image>().sprite = currentLevel.getTextBox();
        Dialouge.text = currentLevel.getDialogue();
        won = false;
        input = GameObject.Find("Input");
    }

    void UpdateText()
    {
        if (countDown >= 0)
        {
            timer.text = TimeSpan.FromSeconds(countDown).ToString(@"m\:ss\:ff");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startWait <= 0)
        {
            if (!levelOver)
            {
                if (countDown >= 0)
                {
                    if (startTimerFinish)
                    {
                        startTimerFinish = false;
                        startTimer.text = "";
                        instructions.text = "";
                    }
                    countDown -= Time.deltaTime;
                    input.GetComponent<inputProcessor>().timer = (byte)(Math.Ceiling((countDown/startCount * 9)));
                    if (countDown <= dangerZone)
                    {
                        timer.color = new Color(1.0f, 0.0f, 0.0f);
                    }
                }
                else
                {
                    KilledPatient();
                    StartCoroutine(Delay());
                }
                UpdateText();

            }
            else
            {
                StartCoroutine(Delay());
            }
        }
        else
        {
            startWait -= Time.deltaTime;
            startTimer.text = ((int)startWait).ToString();
        }


    }

    public void KilledPatient()
    {

        gameManager.GetComponent<GameManager>().addDeath();
        levelOver = true;
        instructions.text = "You killed the patient";
        currentLevel.LevelEnd(true);
        won = false;
        foreach (var d in currentLevel.tasks)
        {
            d.completed = false;
        }
    }
    public void LevelSuccess()
    {
        levelOver = true;
        instructions.text = "Level Complete ";
        currentLevel.LevelEnd(false);
        won = true;
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        if (gameManager.GetComponent<GameManager>().getDeath() == 3)
        {
            SceneManager.LoadScene("EndLoseScene");
        }
        else if(!won)
        {
            SceneManager.LoadScene("transitionLoseScene");
        }
        else if(currentLevel.getLevelNumber() == 4)
        {
            SceneManager.LoadScene("EndWinScene");
        }
        else
        {
            SceneManager.LoadScene("transitionWinScene");
        }
        
    }
}
