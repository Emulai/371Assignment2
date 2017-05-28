using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DictionaryToScreen : MonoBehaviour {

	public enum Difficulty {Easy = 4, Medium = 5, Hard = 6};
	private Text m_word;
	private List<string> m_dictionary = new List<string> ();
	private List<string> m_filter = new List<string> ();
	private List<Disguise> m_disguiser = new List<Disguise> ();
	public Difficulty m_difficulty;

	// Use this for initialization
	void Start () {
		m_word = GetComponent<Text> ();
		Load ();
		FilterWords ();
		int rand = Random.Range (0, m_filter.Count);
		EncryptWord (m_filter [rand].ToUpper());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private bool Load()
	{
		string filePath = @"Assets\Dictionary\Dictionary.txt";
		try
		{
			string line;
			StreamReader theReader = new StreamReader(filePath, Encoding.Default);
			using (theReader)
			{
				do
				{
					line = theReader.ReadLine();

					if (line != null)
					{
						m_dictionary.Add(line);
					}
				}
				while (line != null);
				theReader.Close();
				return true;
			}
		}
		catch (Exception e) {
			Debug.Log (e.Message);
			return false;
		}
	}

	private void FilterWords()
	{
		foreach (string word in m_dictionary)
		{
			if (word.Length == (int)m_difficulty) {
				m_filter.Add (word);
			}
		}
	}

	private void EncryptWord(string p_crypt)
	{
		for (int i = 0; i < (int)m_difficulty; i++) {
			m_disguiser.Add (new Disguise (p_crypt [i]));
		}

		int lIndex = 0;
		while (lIndex != (int)m_difficulty - 3) {
			int rand = Random.Range (0, p_crypt.Length - 1);
			if (!m_disguiser [rand].isDisguised) {
				m_disguiser [rand].isDisguised = true;
				lIndex++;
			}
		}

		RenderWord ();
	}

	private void RenderWord()
	{
		StringBuilder encrypt = new StringBuilder();
		for (int i = 0; i < (int)m_difficulty; i++) {
			encrypt.Append(m_disguiser [i].GetLetter ());
		}

		string crypt = encrypt.ToString();
		m_word.text = crypt;

		CheckVictory ();
	}

	public void CheckLetter(char p_letter)
	{
		foreach (Disguise disguise in m_disguiser) {
			if (disguise.isDisguised) {
				disguise.isDisguised = false;
				if (disguise.GetLetter () != p_letter)
					disguise.isDisguised = true;
			}
		}

		RenderWord ();
	}

	private void CheckVictory()
	{
		bool victory = true;
		foreach (Disguise disguise in m_disguiser) {
			if (disguise.isDisguised) {
				victory = false;
			}
		}
		if (victory) {
			Debug.Log ("Victory!!!");
		}
	}
}
   