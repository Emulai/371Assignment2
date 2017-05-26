/**
 * Brief - Script containing all code used in the Javelin Throw event
 * Author - Jack Matters
 * Date - 20/05/2017
 * Version 01 - Started, got words read in from files and randomly choosing which word to display
 * 
 * Date - 21/05/2017
 * Version 02 - Added GUIStyle and GUIBox code for displaying current word to screen and changing of font colours
 * 
 * Date 24/05/2017
 * Version 03 - Added pause screen between different event states
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JavelinThrowScript : MonoBehaviour {

    // Private word array lists (runSpeedArray contains 4/5 letter words, throwPowerArray contains 6/7 letter words)
    private ArrayList runSpeedArray;
    private ArrayList throwPowerArray;

    // Current word player must type out data variables
    private string currentWord;
    private char[] currentWordCharArray;
    private char currentWordLetter;
    private int currentWordPos;

    // Current event state
    private string eventState;

    // GUI styles
    private GUIStyle style;
    private GUIStyle currentLetterStyle;
    private GUIStyle completedLetterStyle;
    private GUIStyle timeStyle;
    private GUIStyle infoStyle;

    // Time variables
    private int timeStart;
    private int timeNow;

    // Variable for pausing of game between event states
    private bool pause;

	// Initialize Javelin Throw event
	void Start () 
    {
		// Initial word data types
        runSpeedArray = new ArrayList();
        throwPowerArray = new ArrayList();
        currentWord = "";
        currentWordPos = 0;

        // Set data file names
        string fourFiveLetterWords = "Assets/Resources/DataFiles/FourFiveLetterWords.txt";
        string sixSevenLetterWords = "Assets/Resources/DataFiles/SixSevenLetterWords.txt";

        // Read in word data files
        ReadFile(fourFiveLetterWords, runSpeedArray);
        ReadFile(sixSevenLetterWords, throwPowerArray);

        // Initialize current event state (running state, throwing state)
        eventState = "running";

        // Initialize all style properties
        SetStyleGUI();

        // Initialize time variables
        timeStart = (int)Time.time;
        timeNow = (int)Time.time;

        // Initialize pause to true
        pause = true;
	}
	

	// Update the Javelin Throw event
	void Update () 
    {
        // Check to see if game is paused
        if (!pause)
        {
            // Update time
            timeNow = (int)Time.time;
            if (timeNow - timeStart >= 15)
            {
                // Change event state every 15 seconds (15 seconds for running/throwing, then ending screen), displaying a pause screen between states
                if (!pause)
                {
                    pause = true;
                    //else 
                    //timeStart = (int)Time.time;
                }

                if (eventState == "running")
                    eventState = "throwing";
                else if (eventState == "throwing")
                    eventState = "finished";
            }

            // Running state of the event (4 and 5 letter words)
            if (eventState == "running")
            {
                // This runs once at start of Javelin Throw event to initialize first word
                if (currentWord == "")
                {
                    // Set first word to be typed by randomly selecting from list
                    int startNum = Random.Range(0, runSpeedArray.Count);
                    currentWord = runSpeedArray[startNum].ToString();
                    currentWordCharArray = currentWord.ToCharArray();
                    currentWordLetter = currentWordCharArray[0];
                    currentWordPos = 0;
                    return;
                }
                else
                {
                    // Get user input
                    if (Input.anyKeyDown)
                    {
                        if (Input.GetKeyDown((KeyCode)currentWordLetter))
                        {
                            // If user input correct character, move on to next character else get next word if full word has been typed
                            if (currentWordPos + 1 < currentWordCharArray.Length)
                            {
                                currentWordPos++;
                                currentWordLetter = currentWordCharArray[currentWordPos];
                            }
                            else
                            {
                                currentWordPos = 0;
                                int wordNum = Random.Range(0, runSpeedArray.Count);
                                currentWord = runSpeedArray[wordNum].ToString();
                                currentWordCharArray = currentWord.ToCharArray();
                                currentWordLetter = currentWordCharArray[0];

                                // Increase in value for whatever for completing word
                            }
                        }
                        else
                        {

                            // Decrease in value for whatever for incorrect key input
                        }
                    }
                }
            }
            // Throwing state of event (6 and 7 letter words)
            else if (eventState == "throwing")
            {
                // This runs once at start of Javelin Throw event to initialize first word
                if (currentWord.Length < 6)
                {
                    // Set first word to be typed by randomly selecting from list
                    int startNum = Random.Range(0, throwPowerArray.Count);
                    currentWord = throwPowerArray[startNum].ToString();
                    currentWordCharArray = currentWord.ToCharArray();
                    currentWordLetter = currentWordCharArray[0];
                    currentWordPos = 0;
                    return;
                }
                else
                {
                    // Get user input
                    if (Input.anyKeyDown)
                    {
                        if (Input.GetKeyDown((KeyCode)currentWordLetter))
                        {
                            // If user input correct character, move on to next character else get next word if full word has been typed
                            if (currentWordPos + 1 < currentWordCharArray.Length)
                            {
                                currentWordPos++;
                                currentWordLetter = currentWordCharArray[currentWordPos];
                            }
                            else
                            {
                                currentWordPos = 0;
                                int wordNum = Random.Range(0, throwPowerArray.Count);
                                currentWord = throwPowerArray[wordNum].ToString();
                                currentWordCharArray = currentWord.ToCharArray();
                                currentWordLetter = currentWordCharArray[0];

                                // Increase in value for whatever for completing word
                            }
                        }
                        else
                        {
                            Debug.Log("Incorrect Key Pressed");

                            // Decrease in value for whatever for incorrect key input
                        }
                    }
                }
            }
            // Ending screen
            else
            {
                // Ending screen animations/information/whatnot
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            // Unpause when space bar is pressed and reset time
            pause = false;
            timeStart = (int)Time.time;
            timeNow = (int)Time.time;
        }
	}


    // Set the different GUI style properties
    private void SetStyleGUI()
    {
        style = new GUIStyle();
        currentLetterStyle = new GUIStyle();
        completedLetterStyle = new GUIStyle();
        timeStyle = new GUIStyle();
        infoStyle = new GUIStyle();

        // Position text to centre of box
        style.alignment = TextAnchor.MiddleCenter;
        currentLetterStyle.alignment = TextAnchor.MiddleCenter;
        completedLetterStyle.alignment = TextAnchor.MiddleCenter;
        infoStyle.alignment = TextAnchor.MiddleCenter;
        infoStyle.wordWrap = true;

        // Set different style colours
        currentLetterStyle.normal.textColor = Color.green;
        completedLetterStyle.normal.textColor = Color.blue;

        // Set different style text sizes
        style.fontSize = 64;
        currentLetterStyle.fontSize = 64;
        completedLetterStyle.fontSize = 64;
        timeStyle.fontSize = 24;
        infoStyle.fontSize = 32;
    }


    // Display GUI
    void OnGUI()
    {
        // Holds the added total of all characters read
        float tempSize = 0;
        
        // Check to see if screen paused or not
        if (!pause)
        {
            // Display word to be typed to the screen, colouring each letter appropriately
            for (int i = 0; i < currentWord.Length; i++)
            {
                // Get length of current character
                string tempChar = currentWordCharArray[i].ToString();
                GUIContent content = new GUIContent(tempChar);
                Vector2 contentSize = style.CalcSize(content);

                // Add to temp size
                tempSize += contentSize.x;

                // Colour each character (red if already typed, green if current one to type, black if not reached yet)
                if (i < currentWordPos)
                    GUI.Box(new Rect(0, 0, Screen.width + (Screen.width / 2) + (tempSize), Screen.height), currentWordCharArray[i].ToString(), completedLetterStyle);
                else if (i == currentWordPos)
                    GUI.Box(new Rect(0, 0, Screen.width + (Screen.width / 2) + (tempSize), Screen.height), currentWordCharArray[i].ToString(), currentLetterStyle);
                else
                    GUI.Box(new Rect(0, 0, Screen.width + (Screen.width / 2) + (tempSize), Screen.height), currentWordCharArray[i].ToString(), style);

                // Add to temp size again
                tempSize += contentSize.x;
            }
        }
        else
        { 
            // Display pause screen info
            if (eventState == "running")
            {
                string temp = "testing testing testing testing testing testing testing testing testing testing testing testing testing testing testing testing";
                GUI.Box(new Rect(Screen.width/4, 0, Screen.width/2, Screen.height), temp, infoStyle);
            }
            else if (eventState == "throwing")
            {
                string temp = "";
            }
            else if (eventState == "finished")
            {
                string temp = "";
            }
        }

        // Display time in top left corner
        GUI.Label(new Rect(10, 10, 0, 0), "Time: " + (timeNow - timeStart), timeStyle);
    }


    // Read from file and add words to array list structure
    private void ReadFile(string file, ArrayList wordArray)
    {
        // Open file stream
        var fileStream = new FileStream(@file, FileMode.Open, FileAccess.Read);

        // Read each line from file, adding words to array list
        using (var streamReader = new StreamReader(fileStream))
        {
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                line.ToLower();
                wordArray.Add(line);
            }
        }
    }
}
