using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    private int scoreValue = 0;


    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    

    private int count;
    public int lives;
    public Text winText;
    public Text livesText;
    private bool facingRight = true;


    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        lives = 3;
        SetLivesText();
        winText.text = "";
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;

        anim = GetComponent <Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        score.text = "Coins : " + scoreValue.ToString();
        
        if (facingRight == false && hozMovement > 0)
            {
                Flip();
            }
            else if (facingRight == true && hozMovement < 0)
            {
                Flip();
            }

     isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
            {
                anim.SetInteger("State", 1);
            }
        else if (Input.GetKeyUp(KeyCode.D))
            {
                anim.SetInteger("State", 0);
            }
        if(Input.GetKeyDown(KeyCode.A))
            {
                anim.SetInteger("State", 1);
            }
        else if (Input.GetKeyUp(KeyCode.A))
            {
                anim.SetInteger("State", 0);
            }
        if(isOnGround == true)
            {
                anim.SetBool("JumpBool", false);
            }
        if(isOnGround == false)
            {
                anim.SetBool("JumpBool", true);
            }
    }
void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

     private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            lives = lives - 1;
            SetLivesText();
        }
        if(scoreValue == 4)
        {
            transform.position = new Vector2(56,0);
            lives = 3;
            SetLivesText();
        }
        if(scoreValue == 8)
        {
            winText.text = "You Win! Game Created by Leonard Garcia";

            musicSource.clip = musicClipTwo;
            musicSource.Play();
            Destroy(this);
        }
      
    }

 private void OnCollisionStay2D(Collision2D collision)
 {
    if (collision.collider.tag == "Ground" && isOnGround)
        {
        if (Input.GetKey(KeyCode.W))
            {
            rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
 }
    void SetLivesText()
    {
        livesText.text= "Lives: " + lives.ToString();
        if(lives == 0)
        {
            winText.text= "You Lose! Game Created By Leonard";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            Destroy(this);
        }
    }
}