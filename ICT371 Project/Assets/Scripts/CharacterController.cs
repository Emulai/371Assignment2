/**
 * Brief - Script containing code for moving the camera
 * Author - Jack Matters
 * Date - 29/05/2017
 * Version 01 - Started and finished (used Youtube guide)
 * Reference - https://www.youtube.com/watch?v=blO039OzUZc
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    // Camera move speed
    private float speed;

	// Use this for initialization
	void Start () 
    {
        // Initialize speed
        speed = 10.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        // Get movement direction
        float moveFB = Input.GetAxis("Vertical") * speed;
        float strafe = Input.GetAxis("Horizontal") * speed;

        // Keep movement smooth
        moveFB *= Time.deltaTime;
        strafe *= Time.deltaTime;

        // Move camera
        transform.Translate(strafe, 0, moveFB);
	}
}
