using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BunnyController : MonoBehaviour {

    private Rigidbody2D rbody;
    private Animator anim;
    private Collider2D collid;
    private float startTime;
    private float deathTime = -1;

    public Text scoreText;
    public float Score = 0;
    public float JumpForce = 750f;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collid = GetComponent<Collider2D>();

        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if(deathTime == -1)
        {
            if (Input.GetButtonUp("Jump")){
                rbody.AddForce(transform.up * JumpForce);
            }
            anim.SetFloat("JVelocity", rbody.velocity.y);
            scoreText.text = (Time.time - startTime).ToString("0.0");
        }else
        {

            if(Time.time > (deathTime + 2))
                Application.LoadLevel(Application.loadedLevel);
        }
        
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            foreach(MoveLeft movelefter in FindObjectsOfType<MoveLeft>())
            {
                movelefter.enabled = false;
            }
            foreach (ElementSpawner spawner in FindObjectsOfType<ElementSpawner>())
            {
                spawner.enabled = false;
            }
            deathTime = Time.time;
            anim.SetBool("Dead", true);
            rbody.velocity = Vector2.zero;
            collid.enabled = false;
            rbody.AddForce(transform.up * JumpForce);
            Score = 0;
        }
            
    }
}
