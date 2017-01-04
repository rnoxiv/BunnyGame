using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyController : MonoBehaviour {

    private Rigidbody2D rbody;
    private Animator anim;

    public float JumpForce = 300f;
    public float Speed = 20f;
    private int numJumped = 0;
	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //rbody.AddForce(transform.right * Speed);
        if (Input.GetButtonUp("Jump") && numJumped != 2){
            rbody.AddForce(transform.up * JumpForce);
            numJumped++;
        }else
        {
            numJumped = 0;
        }
        anim.SetFloat("JVelocity", rbody.velocity.y);
	}
}
