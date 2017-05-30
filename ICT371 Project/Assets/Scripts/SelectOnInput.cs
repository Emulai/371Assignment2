/**
 * Brief - Script containing code for selecting buttons via keyboard/gamepad
 * Author - Jack Matters
 * Date - 29/05/2017
 * Version 01 - Started and finished (followed a Unity guide)
 * Reference - https://unity3d.com/learn/tutorials/topics/user-interface-ui/creating-main-menu
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject selectedButton;

    private bool buttonSelected;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Allow switching of buttons via gamepad/keyboard
        if (Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedButton);
            buttonSelected = true;
        }
	}

    // Reset buttonSelectdd to false (no button selected)
    private void OnDisable()
    {
        buttonSelected = false;
    }
}
