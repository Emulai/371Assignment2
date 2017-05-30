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
	public Canvas m_game;
	public Canvas m_end;
	public GameObject m_gameObjects;
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
	private bool end = false;

	// Use this for initialization
	void Start () {
		//m_difficulty == Difficulty.Easy;
		m_gameObjects.SetActive(true);
		m_game.enabled = true;
		m_end.enabled = false;
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
		if (!end)
			ShowWord ();	
	}

	public bool IsEnd()
	{
		return end;
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
		Stream stream = File.Open ("HangScores.has", FileMode.Append);
		BinaryFormatter bformatter = new BinaryFormatter ();
		Debug.Log ("Writing Scores");

		int guesses = 0;
		float time = 0.0f;

		foreach (WordScore word in m_wordList) {
			guesses += word.GetGuesses ();
			time += word.GetTime ();
		}

		List<Score> scores = new List<Score> ();
		scores.Add(new Score(m_wordList.Count, guesses, time));

		bformatter.Serialize (stream, scores);
		stream.Close ();
	}

	private void Read()
	{
		List<Score> scores = new List<Score> ();
		Stream stream = File.Open ("HangScores.has", FileMode.Open);

		BinaryFormatter bformatter = new BinaryFormatter ();
		Debug.Log ("Reading Scores");
		scores = (List<Score>)bformatter.Deserialize (stream);
		stream.Close ();
		foreach (Score score in scores) {
			Debug.Log (score.GetWords());
			Debug.Log (score.GetGuesses ());
			Debug.Log (score.GetTime ());
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
				//Save();
				//m_difficulty = Difficulty.Easy;
				//FilterWords();
				m_game.enabled = false;
				m_end.enabled = true;
				m_gameObjects.SetActive (false);
				end = true;
				foreach (WordScore words in m_wordList)
					m_endWords.text += words.GetWord () + " | Guesses: " + words.GetGuesses() + " | Time: " + words.GetTime () + "\n";
			}
		}
	}
}
   