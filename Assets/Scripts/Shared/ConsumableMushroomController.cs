using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableMushroomController : MonoBehaviour
{
    public GameConstants gameConstants;
    private Rigidbody2D mushroomBody;
    private SpriteRenderer mushroomSprite;
    private int moveRight = 1;
    public Vector2 initialForce = new Vector2(0f, 20f);
    public Vector2 fallingForce = new Vector2(-2f, -1f);
    private Vector2 velocity;

    private float speed = 6;
    private bool onGroundState = false;
    private bool hitPlayer = false;
    private bool collected = false;
    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();
        mushroomSprite = GetComponent<SpriteRenderer>();
        mushroomSprite.drawMode = SpriteDrawMode.Sliced;
        if (mushroomBody.position.x < 0)
        {
            moveRight = 1;
        }
        else
        {
            moveRight = -1;
        }
        initialForce.x *= moveRight;
        mushroomBody.AddForce(initialForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (onGroundState && !hitPlayer && !collected)
        {
            ComputeVelocity();
            MoveMushroom();
        }
    }

    void MoveMushroom()
    {
        mushroomBody.MovePosition(mushroomBody.position + velocity * Time.fixedDeltaTime);
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * speed, 0);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacle"))
        {
            onGroundState = true;
        }

        if (col.gameObject.CompareTag("Player"))
        {
            hitPlayer = true;
            collected = true;
            StartCoroutine(scale());
        }

        if (col.gameObject.name == "PipeBody")
        {
            moveRight *= -1;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Obstacle"))
        {
            mushroomBody.AddForce(fallingForce, ForceMode2D.Force);
        }
        if (col.gameObject.CompareTag("Player") && hitPlayer)
        {
            hitPlayer = false;
        }
    }

    void OnBecameInvisible()
    {
        // Destroy(gameObject);
    }

    IEnumerator scale()
    {
        int steps = 5;
        float stepper = (gameConstants.enlargeScale - 1) / (float)steps;

        for (int i = 0; i < steps; i++)
        {
            mushroomSprite.size += new Vector2(stepper, stepper);
            yield return null;
        }
        collected = true;
        this.transform.localScale = new Vector3(0, 0, 0);
        // this.gameObject.SetActive(false);
    }
}