using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disease
{
    public string diseaseName;
    public bool completed;
    public disease(string DN)
    {
        diseaseName = DN;
        completed = false;
    }
}
public class Level
{
    private int LevelNumber;
    private bool completed;
    private bool lost;
    private double timeCompleted;
    private string dialogue;
    private string instructions;
    private Sprite patient;
    private Sprite textBox;
    public int completedTasks;
    public List<disease> tasks = new List<disease>();
    public Level(int lN, string i, string c)
    {
        patient = null;
        LevelNumber = lN;
        completed = false;
        lost = false;
        timeCompleted = 0;
        instructions = i;
        dialogue = c;
        completedTasks = 0;
    }
    public bool isCompleted()
    {
        return completed;
    }
    public void setPatient(Sprite P)
    {
        patient = P;
    }
    public Sprite getPatient()
    {
        return patient;
    }
    public void setTextBox(Sprite TB)
    {
        textBox = TB;
    }
    public Sprite getTextBox()
    {
        return textBox;
    }
    public bool isLost()
    {
        return lost;
    }
    public void LevelEnd(bool l)
    {
        if (l)
            completed = false;
        else
            completed = true;
        lost = l;
    }
    public string getInstructions()
    {
        return instructions;
    }
    public string getDialogue()
    {
        return dialogue;
    }
    public int getLevelNumber()
    {
        return LevelNumber;
    }
    public void addDisease(string name)
    {
        tasks.Add(new disease(name));
    }
}
public class GameManager : MonoBehaviour
{
    public GameObject GameManagerGO;
    public GameObject Input;

    static string l1Instruction = "Instruction1";
    static string l2Instruction = "Instruction2";
    static string l3Instruction = "Instruction3";
    static string l4Instruction = "Instruction4";
    static string l1Dialogue = "Dialogue1";
    static string l2Dialogue = "Dialogue2";
    static string l3Dialogue = "Dialogue3";
    static string l4Dialogue = "Dialogue4";
    public Sprite patient1;
    public Sprite patient2;
    public Sprite patient3;
    public Sprite patient4;
    public Sprite textBox1;
    public Sprite textBox2;
    public Sprite textBox3;
    public Sprite textBox4;



    // Start is called before the first frame update
    public int deaths;
    public float cash;
    public Level[] levels = new Level[]
    {
        new Level(1, l1Instruction, l1Dialogue),
        new Level(2, l2Instruction, l2Dialogue),
        new Level(3, l3Instruction, l3Dialogue),
        new Level(4, l4Instruction, l4Dialogue)
    };
    void Start()
    {
        levels[0].addDisease("Day Potion");
        levels[1].addDisease("Temperature Dial");
        levels[1].addDisease("Dawn Potion");
        levels[2].addDisease("Acupuncture");
        levels[2].addDisease("Night Potion");
        levels[3].addDisease("Acupuncture");
        levels[3].addDisease("Dawn Potion");
        levels[3].addDisease("Temperature Dial");
        levels[0].setPatient(patient1);
        levels[1].setPatient(patient2);
        levels[2].setPatient(patient3);
        levels[3].setPatient(patient4);
        levels[0].setTextBox(textBox1);
        levels[1].setTextBox(textBox2);
        levels[2].setTextBox(textBox3);
        levels[3].setTextBox(textBox4);
        //levels[2].addDisease("dial");
        DontDestroyOnLoad(Input);
        DontDestroyOnLoad(GameManagerGO);
        
        deaths = 0;
        cash = 0;
    }

    public Level getCurrentLevel()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (!levels[i].isCompleted())
                return levels[i];
        }
        return null;
    }
    public void addDeath()
    {
        deaths++;
        /*if(deaths == 3)
        {
            deaths = 0;
            cash = 0;
            return 3;
        }
        return deaths;*/
    }
    public int getDeath()
    {
        return deaths;
    }
}
