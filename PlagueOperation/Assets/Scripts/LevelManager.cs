using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    private GameObject gameManager;
    private Level currentLevel;
    private GameObject input;
    private inputProcessor inputP;
    public int oldPotionValue;
    public int oldDialValue;
    public int dialGoal; //0 -1023
    public bool wrongTask;
    public TMP_Text dialogue;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        currentLevel = gameManager.GetComponent<GameManager>().getCurrentLevel();
        input = GameObject.Find("Input");
        inputP = input.GetComponent<inputProcessor>();
        oldPotionValue = -1;
        oldDialValue = input.GetComponent<inputProcessor>().dial;
        dialGoal = (input.GetComponent<inputProcessor>().dial + 750) % 1024;
        if (currentLevel.getLevelNumber() == 4 || currentLevel.getLevelNumber() == 3)
            input.GetComponent<inputProcessor>().game_state = 1;
        else
            input.GetComponent<inputProcessor>().game_state = 0;
        input.GetComponent<inputProcessor>().LCD_line1 = "";
        input.GetComponent<inputProcessor>().LCD_line2 = "";
        string tasks = "";
        foreach (var d in currentLevel.tasks)
        {
            if (tasks.Equals(""))
            {
                tasks = d.diseaseName;
            }
            else
            {
                tasks = tasks + ", " + d.diseaseName;
            }
            Debug.Log(tasks);
        }
        dialogue.text = tasks;
    }

    void CompleteTask(string name)
    {
        wrongTask = true;
        //for(int i = 0; i < currentLevel.tasks)
        //Debug.Log(name);
        foreach (var d in currentLevel.tasks)
            {
            if (d.diseaseName == name)
            {
                wrongTask = false;
                if (d.completed == true)
                {
                    input.GetComponent<inputProcessor>().LCD_line2 ="Already Done";
                }
                else
                {
                    d.completed = true;
                    int size = currentLevel.tasks.Count;
                    currentLevel.completedTasks += 1;
                    input.GetComponent<inputProcessor>().LCD_line2 = currentLevel.completedTasks + "/" + size.ToString() + " Completed";
                    print(currentLevel.completedTasks + "   " + currentLevel.tasks);
                    if (currentLevel.completedTasks == size)
                    {
                        this.GetComponent<Clock>().LevelSuccess();
                    }
                }
            }
        }
        if (wrongTask)
        {
            input.GetComponent<inputProcessor>().LCD_line2 = "Wrong task";
            this.GetComponent<Clock>().KilledPatient();
        }
    }
    void Update()
    {
        input = GameObject.Find("Input");
        inputP = input.GetComponent<inputProcessor>();
        Debug.Log(inputP.getPotion());
        if (oldPotionValue != input.GetComponent<inputProcessor>().potion && input.GetComponent<inputProcessor>().potion >= 0)
        {
            Debug.Log(input.GetComponent<inputProcessor>().potion);
            oldPotionValue = input.GetComponent<inputProcessor>().potion;
            if (input.GetComponent<inputProcessor>().potion == 0)
                CompleteTask("Dawn Potion");
            else if (input.GetComponent<inputProcessor>().potion == 1)
                CompleteTask("Day Potion");
            else if (input.GetComponent<inputProcessor>().potion == 2)
                CompleteTask("Night Potion");
        }

        if (oldDialValue != input.GetComponent<inputProcessor>().dial)
        {
            input.GetComponent<inputProcessor>().LCD_line1 = "Get to " + dialGoal / 10;
            input.GetComponent<inputProcessor>().LCD_line2 = "Your at " + input.GetComponent<inputProcessor>().dial / 10;
            if (input.GetComponent<inputProcessor>().dial < dialGoal + 100 && input.GetComponent<inputProcessor>().dial > dialGoal - 100)
            {
                CompleteTask("Temperature Dial");
            }
        }
        if(input.GetComponent<inputProcessor>().simon)
        {
            CompleteTask("Acupuncture");
            input.GetComponent<inputProcessor>().simon = false;
        }

        
    }
}
