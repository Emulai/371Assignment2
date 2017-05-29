using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TextManager : MonoBehaviour {

    public GameObject textBox;
    public Text text;

    public enum gameState { IDLE, RUNNING, FINISHED};
    //gameState state;

    string state;

    public TextAsset intro, level2, level3;
    public string[] lines;

    //Current line and letter to be typed
    public string currentLine;
    private char[] currentLineCharArray;
    private char currentLineLetter;
    private int currentLinePos;
    private int currentLineNum;
    public int endAtLine;

    //GUI Styling
    private GUIStyle finishedStyle;
    private GUIStyle introStyle;
    public Texture2D finishedStyleBackground;
    public Texture2D introStyleBackground;

    private GameObject playerObject;
    Player player;

    private GameObject[] aiObject;
    AI ai, ai1, ai2, ai3;

    float speedIncrease = 0;

    private int levelNumber = 0;


    // Use this for initialization
    void Start()
    {
        state = "IDLE";

        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();

        //Set AI Controllers
        aiObject = new GameObject[4];
        aiObject[0] = GameObject.Find("AI");
        ai = aiObject[0].GetComponent<AI>();
        aiObject[1] = GameObject.Find("AI (1)");
        ai1 = aiObject[1].GetComponent<AI>();
        aiObject[2] = GameObject.Find("AI (2)");
        ai2 = aiObject[2].GetComponent<AI>();
        aiObject[3] = GameObject.Find("AI (3)");
        ai3 = aiObject[3].GetComponent<AI>();


        text.supportRichText = true;
        currentLine = "";
        currentLinePos = 0;
        currentLineNum = 0;

        SetStyleGUI();

        loadLevel();

    }

    void Update()
    {
        if (speedIncrease > 0)
        {
            speedIncrease -= 0.005f;
            player.setHorizontal(speedIncrease);
        }
        if (speedIncrease < 0)
        {
            speedIncrease = 0f;
            player.setHorizontal(speedIncrease);
        }
        if(speedIncrease > 1)
        {
            speedIncrease = 1f;
            player.setHorizontal(speedIncrease);
        }


        if (currentLine == "")
        {
            currentLine = lines[currentLineNum].ToString();
            currentLineCharArray = currentLine.ToCharArray();
            currentLineLetter = currentLineCharArray[0];
            currentLinePos = 0;
        }
        if(state == "IDLE")
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                state = "RUNNING";
                ai.setState(state);
                ai1.setState(state);
                ai2.setState(state);
                ai3.setState(state);
            }
        }
        if (state == "RUNNING")
        {
            if (Input.anyKeyDown)
            {
                //Check if the character is uppercase
                if (char.IsUpper(currentLineLetter))
                {
                    //Make character lowercase
                    currentLineLetter = char.ToLower(currentLineLetter);
                    //Ensure user presses shift to denote uppercase
                    if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown((KeyCode)currentLineLetter))
                    {
                        speedIncrease += 0.10f;
                        player.setHorizontal(speedIncrease);
                        // If correct character, move onto next. Skip newline characters
                        if (currentLinePos + 1 < currentLineCharArray.Length - 1)
                        {
                            currentLinePos++;
                            currentLineLetter = currentLineCharArray[currentLinePos];
                        }
                        else
                        {
                            currentLineNum++;
                            //Check to see if this is the last line in the file and set the textBox inactive
                            if (currentLineNum > endAtLine)
                            {
                                textBox.SetActive(false);
                            }
                            else
                            {
                                currentLine = lines[currentLineNum].ToString();
                                currentLineCharArray = currentLine.ToCharArray();
                                currentLineLetter = currentLineCharArray[0];
                                currentLinePos = 0;
                            }
                        }
                    }
                    else
                    {
                        speedIncrease -= 0.05f;
                        player.setHorizontal(speedIncrease);
                    }
                }
                else if (Input.GetKeyDown((KeyCode)currentLineLetter))
                {
                    speedIncrease += 0.10f;
                    player.setHorizontal(speedIncrease);
                    // If correct character, move onto next. Skip newline characters
                    if (currentLinePos + 1 < currentLineCharArray.Length - 1)
                    {
                        currentLinePos++;
                        currentLineLetter = currentLineCharArray[currentLinePos];

                    }
                    else
                    {
                        currentLineNum++;
                        //Check to see if this is the last line in the file and set the textBox inactive
                        if (currentLineNum > endAtLine)
                        {
                            textBox.SetActive(false);

                        }
                        else
                        {
                            currentLine = lines[currentLineNum].ToString();
                            currentLineCharArray = currentLine.ToCharArray();
                            currentLineLetter = currentLineCharArray[0];
                            currentLinePos = 0;
                        }
                    }
                }
                else
                {
                    speedIncrease -= 0.05f;
                    player.setHorizontal(speedIncrease);
                }
            }
        }
    }

    void FixedUpdate()
    {
        

    }

    public void setState(string newState)
    {
        state = newState;
    }

    public string getState()
    {
        return this.state;
    }

    public void loadLevel()
    {
        levelNumber++;
        if(levelNumber == 1)
        {
            loadText(intro);
        }
        else if(levelNumber == 2)
        {
            loadText(level2);
            
        }
        else if(levelNumber == 3)
        {
            loadText(level3);
        }
    }

    public void loadText(TextAsset toLoad)
    {
        if (toLoad != null)
        {
            lines = (toLoad.text.Split('\n'));
        }
        else
            Debug.Log("ERROR: TEXT FILE EMPTY! PLEASE RE-ATTACH TO TEXTBOXMANAGER!");
        if (endAtLine == 0)
        {
            endAtLine = lines.Length - 1;
        }

    }

    // Set the different GUI style properties
    private void SetStyleGUI()
    {
        finishedStyle = new GUIStyle();
        finishedStyle.alignment = TextAnchor.MiddleCenter;
        finishedStyle.wordWrap = true;
        finishedStyle.fontSize = 32;
        finishedStyle.normal.background = finishedStyleBackground;

        introStyle = new GUIStyle();
        introStyle.alignment = TextAnchor.MiddleCenter;
        introStyle.wordWrap = true;
        introStyle.fontSize = 32;
        introStyle.normal.background = introStyleBackground;
    }

    void OnGUI()
    {
        if(state == "IDLE")
        {
            string temp = "Type the correct letters as fast as you can to win the race\n\nPress enter to start..";
            GUI.Box(new Rect(Screen.width / 4, Screen.height * 0.2f, Screen.width / 2, Screen.height * 0.6f), temp, introStyle);
        }
        if (state == "FINISHED")
        {
            string temp = "Congratulations!!\n\nPress ESC to quit\nPress Enter to advance..";
            GUI.Box(new Rect(Screen.width / 4, Screen.height * 0.2f, Screen.width / 2, Screen.height * 0.6f), temp, finishedStyle);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                state = "IDLE";

            }
        }
        string tempString = "";
        // Display word to be typed to the screen, colouring each letter appropriately
        for (int i = 0; i < currentLine.Length; i++)
        {

            // Colour each character Green if typed, White to type and black if untyped
            if (i < currentLinePos)
            {
                string tempChar = "<color=blue>" + currentLineCharArray[i].ToString() + "</color>";
                tempString += tempChar;
                text.text = tempString;
            }
            else if (i == currentLinePos)
            {
                string tempChar = "<color=white>" + currentLineCharArray[i].ToString() + "</color>";
                tempString += tempChar;
                text.text = tempString;
            }
            else
            {
                string tempChar = "<color=black>" + currentLineCharArray[i].ToString() + "</color>";
                tempString += tempChar;
                text.text = tempString;
            }


        }
    }
}
