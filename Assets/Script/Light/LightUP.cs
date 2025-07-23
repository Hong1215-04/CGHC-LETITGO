using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUP : MonoBehaviour
{
    [SerializeField] private Animator LightChange;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Got");
            LightChange.Play("LightsUp");
        }
    }
}
