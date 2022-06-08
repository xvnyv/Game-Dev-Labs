using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 scaler;
    private Rigidbody2D rigidBody;
    void Start()
    {
        scaler = transform.localScale / (float)30;
        rigidBody = GetComponent<Rigidbody2D>();
        StartCoroutine("ScaleOut");
    }

    IEnumerator ScaleOut()
    {

        Vector2 direction = new Vector2(Random.Range(-1.0f, 1.0f), 1);
        rigidBody.AddForce(direction.normalized * 10, ForceMode2D.Impulse);
        rigidBody.AddTorque(10, ForceMode2D.Impulse);
        // wait for next frame
        yield return null;

        // render for 0.5 second
        for (int step = 0; step < 30; step++)
        {
            this.transform.localScale = this.transform.localScale - scaler;
            // wait for next frame
            yield return null;
        }

        Destroy(gameObject);

    }
    // Update is called once per frame
    void Update()
    {

    }
}
