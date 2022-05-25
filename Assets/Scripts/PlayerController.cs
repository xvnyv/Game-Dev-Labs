using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Transform enemyLocation;
    public Text scoreText;
    public float maxSpeed = 10;
    public float upSpeed = 10;
    public float speed;

    private MenuController menuController;
    private EnemyController enemyController;
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private bool gameOver = false;
    private bool faceRightState = true;
    private int score = 0;
    private bool countScoreState = false;
    private bool onGroundState = true;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        menuController = FindObjectOfType<MenuController>();
        enemyController = FindObjectOfType<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        // toggle state
        if (Input.GetKeyDown("a") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
        }

        if (Input.GetKeyDown("d") && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
        }

        // when jumping, and Gomba is near Mario and we haven't registered our score
        if (!onGroundState && countScoreState && !gameOver)
        {
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState = false;
                score++;
                Debug.Log(score);
            }
        }
    }

    void FixedUpdate()
    {
        if (!gameOver)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            if (Mathf.Abs(moveHorizontal) > 0)
            {
                Vector2 movement = new Vector2(moveHorizontal, 0);
                if (marioBody.velocity.magnitude < maxSpeed)
                    marioBody.AddForce(movement * speed);
            }
            if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
            {
                // stop
                marioBody.velocity = Vector2.zero;
            }

            if (Input.GetKeyDown("space") && onGroundState)
            {
                marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
                onGroundState = false;
                countScoreState = true; //check if Gomba is underneath
            }
        }
    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") && !gameOver)
        {
            onGroundState = true; // back on ground
            countScoreState = false; // reset score state
            if (!gameOver)
            {
                scoreText.text = "Score: " + score.ToString();
            }
        };
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && !gameOver)
        {
            gameOver = true;
            marioBody.velocity = Vector2.zero;
            menuController.SetGameOver();
            enemyController.SetGameOver();
        }
    }

    public void RestartGame()
    {
        gameOver = false;
        score = 0;
        countScoreState = false;
        scoreText.text = "Score: 0";
        marioSprite.transform.position = new Vector2(0, 0);
        marioSprite.flipX = false;
        onGroundState = true;
        faceRightState = true;
    }
}
