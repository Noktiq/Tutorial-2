using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;
    
    public AudioSource playSound;

    private int scoreValue = 0;

    public GameObject winTextObject;

    public GameObject losetextObject;

    private int livesvalue;

    public Text lives;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        winTextObject.SetActive(false);
        losetextObject.SetActive(false);
        livesvalue = 3;
        Setlivestext();

    }

    // Update is called once per frame
    void Update()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (Input.GetKey ("escape")) 
        {
            Application.Quit();
            Debug.Log("escaped program");
        }
    }

    void Setlivestext()
    {
        lives.text = "Lives: " + livesvalue.ToString();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Enemy")
        {
            livesvalue -=1;
            lives.text = "Lives: " + livesvalue.ToString();
            Destroy(collision.collider.gameObject);
        }
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if (scoreValue >= 4)
        {
            winTextObject.SetActive(true);
            Destroy(this.gameObject);
            
        }

        if (livesvalue <= 0)
        {
            losetextObject.SetActive(true);
            Destroy(this.gameObject);
        }

        

    }

   
    

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }

        
    }
}