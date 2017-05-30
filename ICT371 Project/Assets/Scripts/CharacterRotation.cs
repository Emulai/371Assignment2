/**
 * Brief - Script containing code for rotating the camera
 * Author - Jack Matters
 * Date - 29/05/2017
 * Version 01 - Started and finished (used Youtube guide)
 * Reference - https://www.youtube.com/watch?v=blO039OzUZc
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotation : MonoBehaviour {

    // Camera rotation variables
    Vector2 mouseLook;
    Vector2 smoothV;
    private float sensitivity;
    private float smoothing;

    // Player object
    GameObject player;

	// Initialize variables
	void Start () 
    {
        // Camera rotation initialization
        sensitivity = 5.0f;
        smoothing = 2.0f;

        // Get player object
        player = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () 
    {
		// Mouse rotation values
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        // Multiply rotation by sensitivity and smoothing
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        
        // Smooth camera rotation
        smoothV.x = Mathf.Lerp(smoothV.x, mouseDelta.x, 1.0f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, mouseDelta.y, 1.0f / smoothing);

        // Add to total rotation
        mouseLook += smoothV;

        // Clamp mouse x-rotation to directly up and directly down
        mouseLook.y = Mathf.Clamp(mouseLook.y, -90.0f, 90.0f);

        // Rotate the camera
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, player.transform.up);
	}
}
