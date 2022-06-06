using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;
    // private SpriteRenderer enemySprite;
    private bool gameOver = false;


    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        // enemySprite = GetComponent<SpriteRenderer>();
        // get the starting position
        originalX = transform.position.x;
        ComputeVelocity();
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }
    void MoveGomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver && Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {// move gomba
            MoveGomba();
        }
        else if (!gameOver)
        {
            // change direction
            moveRight *= -1;
            ComputeVelocity();
            MoveGomba();
        }
    }

    public void SetGameOver()
    {
        gameOver = true;
        enemyBody.velocity = Vector2.zero;
    }

    public void RestartGame()
    {
        gameOver = false;
        // enemySprite.transform.position = new Vector2(2.5f, -0.46f);
        transform.position = new Vector2(2.5f, -0.46f);
    }

    private void Testing()
    {

    }
}
