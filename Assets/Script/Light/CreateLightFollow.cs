using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLightFollow : MonoBehaviour
{
    [SerializeField] private Animator LightChange;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("DarkIn"))
        {
            Debug.Log("Ok");
            LightChange.Play("PointLightUp");
        }  
        if (collision.gameObject.layer == LayerMask.NameToLayer("DarkOut"))
        {
            LightChange.Play("PointLightDown");
        }  
    }
}
