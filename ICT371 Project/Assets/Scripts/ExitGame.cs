/**
 * Brief - Script containing code for exiting of game
 * Author - Jack Matters
 * Date - 29/05/2017
 * Version 01 - Started and finished (followed a Unity guide)
 * Reference - https://unity3d.com/learn/tutorials/topics/user-interface-ui/creating-main-menu
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour {

	// Exit the game
    public void Quit()
    { 
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
