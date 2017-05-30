using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disguise {
	private char letter;
	private const char disguise = '?';
	public bool isDisguised { get; set; }

	public Disguise(char letter)
	{
		this.letter = letter;
		isDisguised = false;
	}

	public void SetLetter(char letter)
	{
		this.letter = letter;
	}

	public char GetLetter()
	{
		if (isDisguised)
			return disguise;
		else
			return letter;
	}

}
