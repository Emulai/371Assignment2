using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable()]
public class Score {
	private int m_tWords;
	private int m_tGuesses;
	private float m_tTime;

	public Score(int p_tWords, int p_tGuesses, float p_tTime)
	{
		m_tWords = p_tWords;
		m_tGuesses = p_tGuesses;
		m_tTime = p_tTime;
	}

	public Score(SerializationInfo info, StreamingContext ctxt)
	{
		m_tWords = (int)info.GetValue ("NumberOfWords", typeof(int));
		m_tGuesses = (int)info.GetValue ("NumberOfGuesses", typeof(int));
		m_tTime = (float)info.GetValue ("TimeTaken", typeof(float));
	}

	public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		info.AddValue ("NumberOfWords", m_tWords);
		info.AddValue ("NumberOfGuesses", m_tGuesses);
		info.AddValue ("TimeTaken", m_tTime);
	}

	public int GetWords()
	{
		return m_tWords;
	}

	public int GetGuesses()
	{
		return m_tGuesses;
	}

	public float GetTime()
	{
		return m_tTime;
	}
}
