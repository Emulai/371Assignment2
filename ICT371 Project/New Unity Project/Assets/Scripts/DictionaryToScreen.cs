using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum Difficulty {Easy = 4, Medium = 5, Hard = 6};

public class DictionaryToScreen : MonoBehaviour {

	public Text m_word;
	public Text m_uIGuess;
	public Text m_life;
	public Text m_endWords;
	private int m_guesses;
	private float m_guessTime;
	private int m_wordsPassed;
	private int m_wordIndex;
	private int m_lives;
	private List<string> m_dictionary = new List<string> ();
	private List<string> m_filter = new List<string> ();
	private List<WordScore> m_wordList = new List<WordScore> ();
	public Difficulty m_difficulty;
	private bool show = false;
	private float showTime = 0.0f;

	// Use this for initialization
	void Start () {
		//m_difficulty == Difficulty.Easy;
		m_wordsPassed = 0;
		m_wordIndex = -1;
		m_lives = 5;

		Load ();
		FilterWords ();
		EncryptWord ();
		//Read();
	}
	
	// Update is called once per frame
	void Update () {
		ShowWord ();	
	}

	private void ShowWord()
	{
		m_life.text = m_lives.ToString();
		if (show) {

			if (Time.time > (showTime + 1.0)) {
				show = false;
				EncryptWord ();
			}
		}
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

	private void Save()
	{
		Stream stream = File.Open ("HangScores.has", FileMode.Create);
		BinaryFormatter bformatter = new BinaryFormatter ();
		Debug.Log ("Writing Scores");
		bformatter.Serialize (stream, m_wordList);
		stream.Close ();
	}

	private void Read()
	{
		m_wordList = null;
		Stream stream = File.Open ("HangScores.has", FileMode.Open);
		BinaryFormatter bformatter = new BinaryFormatter ();
		Debug.Log ("Reading Scores");
		m_wordList = (List<WordScore>)bformatter.Deserialize (stream);
		stream.Close ();
		foreach (WordScore words in m_wordList) {
			Debug.Log (words.GetWord());
			Debug.Log (words.GetGuesses());
			Debug.Log (words.GetTime());
			m_endWords.text += words.GetWord () + " | Guesses: " + words.GetGuesses() + " | Time: " + words.GetTime () + "\n";
		}
	}

	private void FilterWords()
	{
		m_filter.Clear ();
		foreach (string word in m_dictionary)
		{
			if (word.Length == (int)m_difficulty) {
				m_filter.Add (word);
			}
		}
	}

	private void EncryptWord()
	{
		m_wordIndex++;
		int rand = Random.Range (0, m_filter.Count);

		m_wordList.Add( new WordScore (m_filter [rand].ToUpper(), m_difficulty));
		m_wordList [m_wordIndex].DisguiseWord ();

		m_guesses = 5;
		m_guessTime = Time.time;

		RenderWord ();
	}

	private void RenderWord()
	{
		m_uIGuess.text = m_guesses.ToString();

		m_word.text = m_wordList [m_wordIndex].RenderWord ();

		CheckVictory ();
	}

	public void CheckLetter(char p_letter)
	{
		if (!m_wordList[m_wordIndex].CheckLetter(p_letter))
			m_guesses--;

		RenderWord ();
	}

	private void CheckVictory()
	{
		Debug.Log (m_wordsPassed);

		if (m_wordList[m_wordIndex].CheckRevealed()) {
			//Save numGuesses
			m_wordList[m_wordIndex].SetGuesses(5 - m_guesses);
			//Save time
			m_wordList[m_wordIndex].SetTime((Time.time - m_guessTime));
			//Increment difficulty
			if ((m_wordsPassed > 9) && ((int)m_difficulty == 4)) {
				m_difficulty = Difficulty.Medium;
				FilterWords ();
			} else if ((m_wordsPassed > 19) && ((int)m_difficulty == 5)) {
				m_difficulty = Difficulty.Hard;
				FilterWords ();
			}
			//load new word
			show = true;
			showTime = Time.time;
			//Increment word count
			m_wordsPassed++;
		} else if (m_guesses == 0) {
			//Loss
			m_wordList[m_wordIndex].SetGuesses(5 - m_guesses);
			m_wordList[m_wordIndex].SetTime((Time.time - m_guessTime));
			show = true;
			showTime = Time.time;
			m_lives--;
			m_wordList [m_wordIndex].Reveal ();
			m_word.text = m_wordList [m_wordIndex].RenderWord ();
			if (m_lives == 0) {
				//Save stats and return to main world
				Save();
				//m_difficulty = Difficulty.Easy;
				//FilterWords();
			}
		}
	}
}
   