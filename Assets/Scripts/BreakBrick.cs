using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrick : MonoBehaviour
{
    // Start is called before the first frame update
    private bool broken = false;
    public GameObject prefab;
    public GameObject coinObject;
    public AudioSource shatterAudio;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !broken)
        {
            broken = true;
            // assume we have 5 debris per box
            for (int x = 0; x < 10; x++)
            {
                Instantiate(prefab, transform.position, Quaternion.identity);
            }
            gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<EdgeCollider2D>().enabled = false;
            shatterAudio.PlayOneShot(shatterAudio.clip);
            Vector3 coinPosition = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            Instantiate(coinObject, coinPosition, Quaternion.identity);
        }
    }

}
