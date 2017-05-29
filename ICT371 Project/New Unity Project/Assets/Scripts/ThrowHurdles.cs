using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHurdles : MonoBehaviour {
	public GameObject hurdle;
	Transform m_trans;
	private float m_dt;
	// Use this for initialization
	void Start () {
		m_trans = this.transform;
		m_dt = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= m_dt + 5) {
			m_dt = Time.time;
			Instantiate (hurdle, new Vector3 (m_trans.position.x, m_trans.position.y, 0), Quaternion.identity);
		}
	}
}
