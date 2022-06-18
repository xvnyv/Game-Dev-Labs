using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyControllerEV : MonoBehaviour
{
    public GameConstants gameConstants;
    public UnityEvent onPlayerDeath;
    public UnityEvent onEnemyDeath;
    public float originalX;
    private int moveRight;
    private Vector2 velocity;
    private Rigidbody2D enemyBody;
    private SpriteRenderer enemySprite;
    private bool flattened = false;
    private bool rotating = false;


    // Start is called before the first frame update
    void Start()
    {
        // GameManager.OnPlayerDeath += EnemyRejoice;

        enemyBody = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();

        // get the starting position
        // originalX = transform.position.x;

        // randomise initial direction
        moveRight = Random.Range(0, 2) == 0 ? -1 : 1;
        flipSprite(moveRight);

        // compute initial velocity
        ComputeVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Enemy body position: " + enemyBody.position.x);
        // Debug.Log("Original position: " + originalX.Value);

        if (Mathf.Abs(enemyBody.position.x - originalX) < gameConstants.maxOffset)
        {// move goomba
            MoveEnemy();
        }
        else
        {
            // change direction
            // Debug.Log("Change direction");
            moveRight *= -1;
            flipSprite(moveRight);
            ComputeVelocity();
            MoveEnemy();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // check if it collides with Mario
        if (other.gameObject.tag == "Player")
        {
            // check if collides on top
            float yoffset = (other.transform.position.y - this.transform.position.y);
            if (yoffset > 0.75f)
            {
                KillSelf();
                onEnemyDeath.Invoke();
            }
            else
            {
                // hurt player
                onPlayerDeath.Invoke();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // check if it collides with Mario
        if (other.gameObject.tag == "Ground-Obstacle")
        {
            moveRight *= -1;
            flipSprite(moveRight);
        }
        // Debug.Log("it worked");
    }

    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * gameConstants.maxOffset / gameConstants.enemyPatroltime, 0);
    }

    void MoveEnemy()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void KillSelf()
    {
        // enemy dies
        if (!flattened)
        {
            flattened = true;
            // CentralManager.centralManagerInstance.increaseScore();
            StartCoroutine(flatten());
            // Debug.Log("Kill sequence ends");
        }
    }

    private IEnumerator Celebrate()
    {
        rotating = true;
        float duration = 0.5f;
        float celebrationDuration = 5f;
        Quaternion startRotation = this.gameObject.transform.rotation;
        Quaternion endRotation1 = Quaternion.Euler(new Vector3(0, 0, 90)) * startRotation;
        Quaternion endRotation2 = Quaternion.Euler(new Vector3(0, 0, -90)) * startRotation;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            this.gameObject.transform.rotation = Quaternion.Lerp(startRotation, endRotation1, t / duration);
            yield return null;
        }

        float overallT = duration;
        while (overallT < celebrationDuration)
        {
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                this.gameObject.transform.rotation = Quaternion.Lerp(endRotation1, endRotation2, t / duration);
                yield return null;
            }
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                this.gameObject.transform.rotation = Quaternion.Lerp(endRotation2, endRotation1, t / duration);
                yield return null;
            }
            overallT += 2 * duration;
        }
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            this.gameObject.transform.rotation = Quaternion.Lerp(endRotation1, startRotation, t / duration);
            yield return null;
        }
        this.gameObject.transform.rotation = startRotation;
        rotating = false;
    }

    IEnumerator flatten()
    {
        // Debug.Log("Flatten starts");
        int steps = 5;
        float stepper = 1.0f / (float)steps;

        for (int i = 0; i < steps; i++)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y - stepper, this.transform.localScale.z);

            // make sure enemy is still above ground
            this.transform.position = new Vector3(this.transform.position.x, gameConstants.groundSurface + GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
            yield return null;
        }
        // Debug.Log("Flatten ends");
        // reset position and scale
        this.transform.localScale = new Vector3(1, 1, 1);
        this.transform.position = new Vector3(0, 0, 0);
        this.gameObject.SetActive(false);
        flattened = false;
        // Debug.Log("Enemy returned to pool");
        yield break;
    }

    void flipSprite(int facingRight)
    {
        if (facingRight == 1)
        {
            enemySprite.flipX = true;
        }
        else
        {
            enemySprite.flipX = false;
        }
    }
    // callbacks must be PUBLIC
    public void PlayerDeathResponse()
    {
        // Debug.Log("Enemy killed Mario");
        if (!rotating)
        {
            StartCoroutine(Celebrate());
        }
    }
}
