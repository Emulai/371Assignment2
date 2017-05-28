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
using UnityEngine.SceneManagement;

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
    private GUIStyle barBackStyle;
    private GUIStyle barBackStyleTwo;
    private GUIStyle barFrontStyle;
    public GameObject javelin;

    // GUI style backgrounds
    public Texture2D infoStyleBackground;
    public Texture2D barBackgroundOne;
    public Texture2D barBackgroundTwo;
    public Texture2D barBackgroundThree;

    // Time variables
    private int timeStart;
    private int timeNow;

    // Variable for pausing of game between event states
    private bool pause;

    // Variables for collected data
    private int runSpeed;
    private int throwPower;
    private int runSpeedErrors;
    private int runSpeedComplete;
    private int throwPowerErrors;
    private int throwPowerComplete;
    public float distanceTotal;

	// Initialize Javelin Throw event
	void Start () 
    {
		// Initial word data types
        runSpeedArray = new ArrayList();
        throwPowerArray = new ArrayList();
        currentWord = "";
        currentWordPos = 0;

        // Initialise values
        runSpeed = 0;
        throwPower = 0;
        runSpeedErrors = 0;
        runSpeedComplete = 0;
        throwPowerErrors = 0;
        throwPowerComplete = 0;
        javelin = GameObject.FindWithTag("javelin");
        //javelin.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

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
        pause = true; ;
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
                    pause = true;

                if (eventState == "running")
                    eventState = "throwing";
                else if (eventState == "throwing")
                    eventState = "finishedOne";
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

                                // Increase run speed by 10, and increment words completed
                                runSpeed += 10;
                                runSpeedComplete++;

                                // Ensure run speed doesn't exceed 100
                                if (runSpeed > 100)
                                    runSpeed = 100;
                            }
                        }
                        else
                        {
                            // Decrease run speed by 1 and increment errors
                            runSpeed--;
                            runSpeedErrors++;

                            // Ensure run speed doesn't fall below 0
                            if (runSpeed < 0)
                                runSpeed = 0;
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

                                // Increase throw power by 10, and increment words completed
                                throwPower += 10;
                                throwPowerComplete++;

                                // Ensure run speed doesn't exceed 100
                                if (throwPower > 100)
                                    throwPower = 100;
                            }
                        }
                        else
                        {
                            // Decrease throw power by 1 and increment errors
                            throwPower--;
                            throwPowerErrors++;

                            // Ensure throw power doesn't fall below 0
                            if (throwPower < 0)
                                throwPower = 0;
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
            // Change ending screen on each press of space bar
            if (eventState == "finishedOne")
                eventState = "finishedTwo";
            else if (eventState == "finishedTwo")
                eventState = "finishedThree";
            else if (eventState == "finishedThree")
                EndEvent();
            else
            {
                // Unpause and reset time
                pause = false;
                timeStart = (int)Time.time;
                timeNow = (int)Time.time;
            }
        }
	}
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("test");

        if (collision.gameObject.tag == "javelin")
            Debug.Log("fjdnf");
    }

    // Set the different GUI style properties
    private void SetStyleGUI()
    {
        style = new GUIStyle();
        currentLetterStyle = new GUIStyle();
        completedLetterStyle = new GUIStyle();
        timeStyle = new GUIStyle();
        infoStyle = new GUIStyle();
        barBackStyle = new GUIStyle();
        barBackStyleTwo = new GUIStyle();
        barFrontStyle = new GUIStyle();

        // Position text to centre of box
        style.alignment = TextAnchor.MiddleCenter;
        currentLetterStyle.alignment = TextAnchor.MiddleCenter;
        completedLetterStyle.alignment = TextAnchor.MiddleCenter;
        infoStyle.alignment = TextAnchor.MiddleCenter;
        infoStyle.wordWrap = true;
        barBackStyle.alignment = TextAnchor.MiddleCenter;
        barFrontStyle.alignment = TextAnchor.MiddleCenter;

        // Set different style colours
        currentLetterStyle.normal.textColor = Color.green;
        completedLetterStyle.normal.textColor = Color.blue;

        // Set different style text sizes
        style.fontSize = 72;
        currentLetterStyle.fontSize = 72;
        completedLetterStyle.fontSize = 72;
        timeStyle.fontSize = 24;
        infoStyle.fontSize = 32;
        barFrontStyle.fontSize = 24;

        // Set different style text styles
        style.fontStyle = FontStyle.Bold;
        currentLetterStyle.fontStyle = FontStyle.Bold;
        completedLetterStyle.fontStyle = FontStyle.Bold;
        timeStyle.fontStyle = FontStyle.Bold;

        // Set different style backgrounds
        infoStyle.normal.background = infoStyleBackground;
        barBackStyle.normal.background = barBackgroundOne;
        barBackStyleTwo.normal.background = barBackgroundTwo;
        barFrontStyle.normal.background = barBackgroundThree;
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
            // Display pause screen info, depending on the current screen
            if (eventState == "running")
            {
                string temp = "You have 15 seconds to type out as many 4-5 letter words as you can to build up your running speed. Incorrect characters will decrease speed. \n\n Press Space to begin";
                GUI.Box(new Rect(Screen.width/4, Screen.height * 0.2f, Screen.width/2, Screen.height * 0.6f), temp, infoStyle);
            }
            else if (eventState == "throwing")
            {
                string temp = "You have 15 seconds to type out as many 6-7 letter words as you can to build up throw power. Incorrect characters will decrease power. \n\n Press Space to begin";
                GUI.Box(new Rect(Screen.width / 4, Screen.height * 0.2f, Screen.width / 2, Screen.height * 0.6f), temp, infoStyle);
            }
            else if (eventState == "finishedOne")
            {
                string temp = "Run Phase \n Run Speed: " + runSpeed + "\n Words Completed: " + runSpeedComplete + "\n Errors Made: " + runSpeedErrors + "\n\n Space to continue";
                GUI.Box(new Rect(Screen.width / 4, Screen.height * 0.2f, Screen.width / 2, Screen.height * 0.6f), temp, infoStyle);
            }
            else if (eventState == "finishedTwo")
            {
                string temp = "Throw Phase \n Throw Power: " + throwPower + "\n Words Completed: " + throwPowerComplete + "\n Errors Made: " + throwPowerErrors + "\n\n Space to continue";
                GUI.Box(new Rect(Screen.width / 4, Screen.height * 0.2f, Screen.width / 2, Screen.height * 0.6f), temp, infoStyle);
            }
            else if (eventState == "finishedThree")
            {
                string temp = "Distance Thrown: " + distanceTotal + "\n\n Space to end event";
                GUI.Box(new Rect(Screen.width / 4, Screen.height * 0.2f, Screen.width / 2, Screen.height * 0.6f), temp, infoStyle);
            }
        }

        // Display time in top left corner
        GUI.Label(new Rect(10, 10, 0, 0), "Time: " + (timeNow - timeStart), timeStyle);

        // Display run speed
        GUI.Box(new Rect(Screen.width / 4, 10, 150, 30), "", barBackStyleTwo);
        GUI.Box(new Rect(Screen.width / 4, 10, 150f * ((float)runSpeed / 100f), 30), "", barBackStyle);
        GUI.Box(new Rect(Screen.width / 4, 10, 150, 30), "Run Speed", barFrontStyle);

        // Display throw power
        GUI.Box(new Rect((Screen.width / 4) * 3 - 150, 10, 150, 30), "", barBackStyleTwo);
        GUI.Box(new Rect((Screen.width / 4) * 3 - 150, 10, 150f * ((float)throwPower / 100f), 30), "", barBackStyle);
        GUI.Box(new Rect((Screen.width / 4) * 3 - 150, 10, 150, 30), "Throw Power", barFrontStyle);
    }

    // End the event
    private void EndEvent()
    {
        // Set all stats to whatever


        // Load 3D level again (over world)
        SceneManager.LoadScene(1);
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
