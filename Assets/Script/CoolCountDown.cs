using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolCountDown : MonoBehaviour
{
    float LifeTimeInIce;
    private bool CountingDownOrNot;

    void Start()
    {
        CountingDownOrNot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (CountingDownOrNot == true)
        {
            LifeTimeInIce += Time.deltaTime;
            Debug.Log(LifeTimeInIce);
        }
        if (LifeTimeInIce >= 7)
        {
            GetComponent<PlayerHealth>().Kill();
            LifeTimeInIce = 0;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Fire"))
        {
            CountingDownOrNot = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Fire"))
        {
            CountingDownOrNot = false;
            if (LifeTimeInIce >= 0)
            {
                LifeTimeInIce -= 0.1f;
            }
            Debug.Log(LifeTimeInIce);
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Fire"))
        {
            CountingDownOrNot = true;
        }
    }
}
