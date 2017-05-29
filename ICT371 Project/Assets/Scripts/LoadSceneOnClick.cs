/**
 * Brief - Script containing code for switching between scenes
 * Author - Jack Matters
 * Date - 29/05/2017
 * Version 01 - Started and finished (followed a Unity guide)
 * Reference - https://unity3d.com/learn/tutorials/topics/user-interface-ui/creating-main-menu
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

    // Load different scene
    public void LoadByIndex(int index)
    {
        Debug.Log("Changing scenes");
        SceneManager.LoadScene(index);
    }
}
