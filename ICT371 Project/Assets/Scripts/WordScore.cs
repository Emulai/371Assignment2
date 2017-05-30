using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable()]
public class WordScore : ISerializable {
	private int m_guesses;
	private float m_time;
	private float m_length;
	private string m_word { get; set; }
	private Difficulty m_diff;
	private List<Disguise> m_disguiser;

	public WordScore(string p_word, Difficulty p_diff)
	{
		m_guesses = 0;
		m_time = 0;
		m_length = p_word.Length;
		m_diff = p_diff;
		m_disguiser = new List<Disguise> ();
		for (int i = 0; i < (int)m_diff; i++)
			m_disguiser.Add (new Disguise (p_word [i]));
	}

	public WordScore(SerializationInfo info, StreamingContext ctxt)
	{
		m_guesses = (int)info.GetValue ("NumberOfGuesses", typeof(int));
		m_time = (float)info.GetValue ("TimeToGuess", typeof(float));
		m_word = (string)info.GetValue ("Word", typeof(string));
	}

	public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		m_word = RenderWord ();
		info.AddValue ("NumberOfGuesses", m_guesses);
		info.AddValue ("TimeToGuess", m_time);
		info.AddValue ("Word", m_word);
	}

	public string GetWord()
	{
		return m_word;
	}

	public string GetTheWord()
	{
		string word = RenderWord ();
		return word;
	}

	public int GetGuesses()
	{
		return m_guesses;
	}

	public void SetGuesses(int p_guess)
	{
		m_guesses = p_guess;
	}

	public float GetTime()
	{
		return m_time;
	}

	public void SetTime(float p_time)
	{
		m_time = p_time;
	}

	public void DisguiseWord()
	{
		int lIndex = 0;
		while (lIndex != (int)m_diff - 3) {
			int rand = (int) Random.Range (0, m_length);
			if (!m_disguiser [rand].isDisguised) {
				m_disguiser [rand].isDisguised = true;
				lIndex++;
			}
		}
	}

	public string RenderWord()
	{
		StringBuilder encrypt = new StringBuilder();
		for (int i = 0; i < (int)m_diff; i++) {
			encrypt.Append(m_disguiser [i].GetLetter ());
		}

		string crypt = encrypt.ToString ();
		return crypt;
	}

	public bool CheckLetter(char p_letter)
	{
		bool goodGuess = false;
		foreach (Disguise disguise in m_disguiser) {
			if (disguise.isDisguised) {
				disguise.isDisguised = false;
				if (disguise.GetLetter () != p_letter)
					disguise.isDisguised = true;
				else
					goodGuess = true;
			}
		}
		return goodGuess;
	}

	public bool CheckRevealed()
	{
		bool revealed = true;
		foreach (Disguise disguise in m_disguiser) {
			if (disguise.isDisguised) {
				revealed = false;
			}
		}
		return revealed;
	}

	public void Reveal()
	{
		foreach (Disguise disguise in m_disguiser)
			disguise.isDisguised = false;
	}



}
