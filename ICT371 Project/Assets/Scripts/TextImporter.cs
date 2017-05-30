using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextImporter : MonoBehaviour {

    public TextAsset textfile;
    public string[] lines;

	// Use this for initialization
	void Start ()
    {
		if(textfile != null)
        {
            lines = (textfile.text.Split('\n'));

        }
	}
	

}
