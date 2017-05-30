using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    private Rigidbody2D body;
    private Animator aiAnimator;
    public float movementSpeed, horizontal;
    string state;

    // Use this for initialization
    void Start()
    {
        state = "IDLE";
        body = GetComponent<Rigidbody2D>();
        aiAnimator = GetComponent<Animator>();
        movementSpeed = Random.Range(1f, 10f);
        horizontal = Random.Range(0.3f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (state == "RUNNING")
        {
            body.velocity = new Vector2(1 * movementSpeed, 0);
            aiAnimator.SetFloat("speed", Mathf.Abs(horizontal));
        }

    }

    public void setState(string newState)
    {
        state = newState;
    }
}
