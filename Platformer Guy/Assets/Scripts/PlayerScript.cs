using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerScript : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip winMusic;
    public AudioClip loseMusic;

    private Rigidbody2D rd2d;
    public float jumpAmount = 6;
    public float gravityScale = 5;
    public float fallingGravityScale = 2;
    public float speed = 0;
    public Text score;
    private int scoreValue = 0;
    public TextMeshProUGUI livesText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    private int lives;

    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        scoreValue = 0;
        lives = 3;

        SetScoreText();
        SetLivesText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }
    void SetScoreText()
    {

        if (scoreValue == 4)
        {
            musicSource.clip = winMusic;
            musicSource.Play();
            winTextObject.SetActive(true);

        }
    }
     void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();

        if (lives == 0)

        {   
            this.gameObject.SetActive(false);     
            loseTextObject.SetActive(true);
            musicSource.clip = loseMusic;
            musicSource.Play();
        }

    }
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            SetScoreText();
        }

        else if (collision.collider.tag == "Enemy")
        {
            lives = lives -1;
            Destroy(collision.collider.gameObject);
            SetLivesText();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
    {
        if (Input.GetKeyDown(KeyCode.W))
            {
                rd2d.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
            }
        if(rd2d.velocity.y >= 0)
            {
                rd2d.gravityScale = gravityScale;
            }
        else if (rd2d.velocity.y < 0)
            {
                rd2d.gravityScale = fallingGravityScale;
            }
    }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}