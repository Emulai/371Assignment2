/**
 * Brief - Script containing code for swithing of scenes when camera walks through banner
 * Author - Jack Matters
 * Date - 30/05/2017
 * Version 01 - Started and finished
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCollideSwitch : MonoBehaviour {

    // Switch scene depending on which banner walked through
    void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.name == "JavelinBanner")
            SceneManager.LoadScene(2);
        //if (this.gameObject.name == "100MBanner")
        //    SceneManager.LoadScene(3);
        //if (this.gameObject.name == "110MBanner")
        //    SceneManager.LoadScene(4);
    }
}
