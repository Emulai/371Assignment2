/**
 * Brief - Script containing code for opening and closing menu on press of Esc key
 * Author - Jack Matters
 * Date - 29/05/2017
 * Version 01 - Started and finished
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenuOnEsc : MonoBehaviour {

    // Variables for menu canvas
    public GameObject canvas;
    private bool pause;

	// Use this for initialization
	void Start () 
    {
        // Make sure menu is hidden and cursor isn't visible
        canvas.gameObject.SetActive(false);
        pause = false;
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Open/close menu on press of Esc key
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            if (pause)
            {
                // Exit menu, enable camera movement
                Time.timeScale = 1.0f;
                canvas.gameObject.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                pause = false;
                GameObject.Find("Player").GetComponent<CharacterController>().enabled = true;
                GameObject.Find("Main Camera").GetComponent<CharacterRotation>().enabled = true;
            }
            else
            {
                // Open menu, stopping camera movement
                Time.timeScale = 0.0f;
                canvas.gameObject.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                pause = true;
                GameObject.Find("Player").GetComponent<CharacterController>().enabled = false;
                GameObject.Find("Main Camera").GetComponent<CharacterRotation>().enabled = false;
            }
        }
	}

    // Resume game on button press
    public void Resume()
    {
        // Hide menu and cursor and enable camera movement
        Time.timeScale = 1.0f;
        canvas.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pause = false;
        GameObject.Find("Player").GetComponent<CharacterController>().enabled = true;
        GameObject.Find("Main Camera").GetComponent<CharacterRotation>().enabled = true;
    }
}
