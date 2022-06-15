using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableCoin : MonoBehaviour
{
    public GameConstants gameConstants;
    private bool collected = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !collected)
        {
            collected = true;
            CentralManager.centralManagerInstance.increaseScore();
            StartCoroutine(scale());
        }
    }

    IEnumerator scale()
    {
        int steps = 5;
        float stepper = (gameConstants.enlargeScale - 1) / (float)steps;

        for (int i = 0; i < steps; i++)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x + stepper, this.transform.localScale.y + stepper, this.transform.localScale.z + stepper);
            yield return null;
        }
        this.gameObject.SetActive(false);
    }
}
