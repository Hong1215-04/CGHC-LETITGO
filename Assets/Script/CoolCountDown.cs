using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolCountDown : PlayerStates
{
    private float LifeTimeInIce;
    private bool CountingDownOrNot;

    [SerializeField] float maxTime;


    protected override void InitState()
    {
        base.InitState();
        CountingDownOrNot = true;
        UIManager.Instance.UpdateFuel(LifeTimeInIce, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (CountingDownOrNot == true)
        {
            LifeTimeInIce += Time.deltaTime;
            UIManager.Instance.UpdateFuel(LifeTimeInIce, maxTime);
        }
        if (LifeTimeInIce >= maxTime)
        {
            GetComponent<PlayerHealth>().Kill();
            LifeTimeInIce = 0;
            UIManager.Instance.UpdateFuel(LifeTimeInIce, maxTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Fire"))
        {
            CountingDownOrNot = false;
            UIManager.Instance.UpdateFuel(LifeTimeInIce, maxTime);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Fire"))
        {
            CountingDownOrNot = false;
            if (LifeTimeInIce >= 0)
            {
                LifeTimeInIce -= 1f;
                UIManager.Instance.UpdateFuel(LifeTimeInIce, maxTime);
            }
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
