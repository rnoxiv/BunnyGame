using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BunnyController : MonoBehaviour {

    private Rigidbody2D rbody;
    private Animator anim;
    private Collider2D collid;
    private float startTime;
    private float deathTime = -1;
    private int numJumped = 0;

    public Text scoreText;
    public float JumpForce = 750f;
    public AudioSource deathSFX;
    public AudioSource jumpSFX;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collid = GetComponent<Collider2D>();

        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScene");
        }

        if(deathTime == -1)
        {
            if ((Input.GetButtonUp("Jump") || Input.GetButtonUp("Fire1"))  && numJumped < 2){
                jumpSFX.Play();
                if (rbody.velocity.y < 0)
                    rbody.velocity = Vector2.zero;
                float valJump = JumpForce;
                if (numJumped == 1)
                    valJump = JumpForce * 0.75f;
                rbody.AddForce(transform.up * valJump);
                numJumped++;
            }
            anim.SetFloat("JVelocity", rbody.velocity.y);
            scoreText.text = (Time.time - startTime).ToString("0.0");
        }else
        {

            if(Time.time > (deathTime + 2))
                SceneManager.LoadScene("GameScene");
        }
        
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            GameObject.Find("BGSound").GetComponent<AudioSource>().Stop();
            deathSFX.Play();
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

            float currentBestScore = PlayerPrefs.GetFloat("BestScore", 0);
            float currentScore = Time.time - startTime;
            if (currentBestScore < currentScore)
                PlayerPrefs.SetFloat("BestScore", currentScore);
        }else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground") && numJumped != 0)
        {
            numJumped = 0;
        }
            
    }
}
