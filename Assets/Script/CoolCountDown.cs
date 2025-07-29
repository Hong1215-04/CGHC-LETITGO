using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolCountDown : PlayerStates
{
    public float LifeTimeInIce;
    private bool CountingDownOrNot;
    [SerializeField] float maxTime;

    protected override void InitState()
    {
        base.InitState();
        LifeTimeInIce = 0;
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
            //Debug.Log(LifeTimeInIce);
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
            
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("ChangeLevel"))
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
                LifeTimeInIce -= 1f;
                UIManager.Instance.UpdateFuel(LifeTimeInIce, maxTime);
            }
            //Debug.Log(LifeTimeInIce);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("ChangeLevel"))
        {
            CountingDownOrNot = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Fire"))
        {
            CountingDownOrNot = true;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("ChangeLevel"))
        {
            CountingDownOrNot = false;

        }
    }
}
