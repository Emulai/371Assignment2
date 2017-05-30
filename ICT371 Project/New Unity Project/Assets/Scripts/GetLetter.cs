using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class GetLetter : MonoBehaviour {
	private InputField input;
	public GameObject m_manager;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		input = gameObject.GetComponent<InputField> ();
		input.ActivateInputField ();
		input.Select ();
		InputField.SubmitEvent se = new InputField.SubmitEvent ();
		se.AddListener (Guess);
		input.onEndEdit = se;
		input.onValueChanged.AddListener ( delegate { OnValueChanged (); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnValueChanged()
	{
		string text = input.text;
		text = Regex.Replace (text, @"[^a-zA-Z]", "");
		if (text != input.text.ToUpper ())
			input.text = text.ToUpper();

	}

	private void Guess(string guess)
	{
		if (guess != "") {
			m_manager.GetComponent<DictionaryToScreen> ().CheckLetter (guess [0]);
		}
		input.text = "";
		input.ActivateInputField ();
		input.Select ();
	}
}
