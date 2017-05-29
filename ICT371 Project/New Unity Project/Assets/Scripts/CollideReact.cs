using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideReact : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log ("Collide!!");
		/*Vector3 tree = new Vector3 ();
		tree.x += 3;
		this.transform.position = tree;*/
	}
}
