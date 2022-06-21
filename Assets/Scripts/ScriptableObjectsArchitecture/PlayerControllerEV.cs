using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerEV : MonoBehaviour
{
    private float force;
    public IntVariable marioUpSpeed;
    public IntVariable marioMaxSpeed;
    public GameConstants gameConstants;
    public CustomCastEvent castPowerup;

    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    private bool onGroundState = true;
    private Animator marioAnimator;
    private AudioSource marioAudio;
    public ParticleSystem dustCloud;
    private bool isDead = false;
    // private bool isADKeyUp = true;
    private bool countScoreState = false;
    // private bool isADKeyDown = false;
    // private bool isSpacebarDown = false;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();

        marioUpSpeed.SetValue(gameConstants.playerMaxJumpSpeed);
        marioMaxSpeed.SetValue(gameConstants.playerMaxSpeed);
        force = gameConstants.playerDefaultForce;
    }

    // Update is called once per frame
    void Update()
    {
        // isADKeyUp = Input.GetKeyUp("a") || Input.GetKeyUp("d");
        // isADKeyDown = Input.GetKeyDown("a") || Input.GetKeyDown("d");
        // isSpacebarDown = Input.GetKeyDown("space");

        // Debug.Log("Is AD key up: " + isADKeyUp);
        // Debug.Log("Is space key up: " + isSpacebarUp);

        // toggle state
        if (Input.GetKeyDown("a") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;

            // check velocity
            if (Mathf.Abs(marioBody.velocity.x) > 0.05)
            {
                marioAnimator.SetTrigger("onSkid");
            }
        }

        if (Input.GetKeyDown("d") && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;

            // check velocity
            if (Mathf.Abs(marioBody.velocity.x) > 0.05)
            {
                marioAnimator.SetTrigger("onSkid");
            }
        }
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));

        if (Input.GetKeyDown("z"))
        {
            // cast powerup in first slot
            castPowerup.Invoke(KeyCode.Z);
        }

        if (Input.GetKeyDown("x"))
        {
            // cast powerup in second slot
            castPowerup.Invoke(KeyCode.X);
        }
    }

    void FixedUpdate()
    {
        if (!isDead)
        {

            float moveHorizontal = Input.GetAxis("Horizontal");
            if (Mathf.Abs(moveHorizontal) > 0)
            {
                Vector2 movement = new Vector2(moveHorizontal, 0);
                if (marioBody.velocity.magnitude < marioMaxSpeed.Value)
                {
                    marioBody.AddForce(movement * 40);
                }
            }
            if (Input.GetKeyDown("space") && onGroundState)
            {
                marioBody.AddForce(Vector2.up * marioUpSpeed.Value, ForceMode2D.Impulse);
                onGroundState = false;
                countScoreState = true; //check if Gomba is underneath

                marioAnimator.SetBool("onGround", onGroundState);
            }
            if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
            {
                // stop
                marioBody.velocity = Vector2.zero;
            }
        }
    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") && !isDead)
        {
            onGroundState = true; // back on ground
            countScoreState = false; // reset score state
            marioAnimator.SetBool("onGround", onGroundState);
            dustCloud.Play();
        };

        if (col.gameObject.CompareTag("Obstacle") && !onGroundState && Mathf.Abs(marioBody.velocity.y) <= 0.01f)
        {
            onGroundState = true; // back on ground
            countScoreState = false; // reset score state
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    void PlayJumpSound()
    {
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    public void PlayerDiesSequence()
    {
        isDead = true;
        StartCoroutine(flatten());
    }


    IEnumerator flatten()
    {
        int steps = 5;
        float stepper = 1.0f / (float)steps;

        for (int i = 0; i < steps; i++)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x - stepper, this.transform.localScale.y, this.transform.localScale.z);

            // make sure enemy is still above ground
            yield return null;
        }
        this.gameObject.SetActive(false);
        yield break;
    }

}
