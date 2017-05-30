using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHurdle : MonoBehaviour {
	private Transform m_trans;
	private Vector3 m_pos;
	private float m_dt;
	public float m_speed;
	// Use this for initialization
	void Start () {
		m_trans = this.transform;
		m_pos = m_trans.position;
		m_dt = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		m_pos.x -= m_speed;
		this.transform.position = m_pos;

		if (Time.time > m_dt + 15) {
			Destroy (gameObject);
			m_dt = Time.time;
		}
		
	}
		
}
