using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D body;
    private Animator playerAnimator;
    private GameObject textBoxManager;
    TextManager textBox;

    [SerializeField]
    private float movementSpeed;
    public float horizontal;

    // Use this for initialization
    void Start ()
    {
        textBoxManager = GameObject.Find("TextBoxManager");
        textBox = textBoxManager.GetComponent<TextManager>();
        
        body = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        movementSpeed = 10;
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void FixedUpdate()
    {
        // = Input.GetAxis("Horizontal");
        HandleMovement(horizontal);
        

    }

    private void HandleMovement(float horizontal)
    {
        body.velocity = new Vector2(horizontal * movementSpeed, 0);
        playerAnimator.SetFloat("speed", Mathf.Abs(horizontal));
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        textBox.setState("FINISHED");
        textBox.loadLevel();
        Debug.Log("Collision detected");
    }


    public float getSpeed()
    {
        return movementSpeed;
    }

    public void setSpeed(float newSpeed)
    {
        movementSpeed = newSpeed;
    }

    public void setHorizontal(float newHorizontal)
    {
        horizontal = newHorizontal;
    }
}
