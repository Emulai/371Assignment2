using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable()]
public class Score {
	List<WordScore> m_wScore = new List<WordScore>();

	public Score(List<WordScore> p_wScore)
	{
		m_wScore = p_wScore;
	}

	public Score(SerializationInfo info, StreamingContext ctxt)
	{
		m_wScore = (List<WordScore>)info.GetValue ("PlayerScore", typeof(List<WordScore>));
	}

	public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
	{
		info.AddValue ("PlayerScore", m_wScore);
	}

	public List<WordScore> GetWordScores()
	{
		return m_wScore;
	}
		
}
