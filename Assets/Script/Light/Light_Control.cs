using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light_Control : MonoBehaviour
{
    [SerializeField] private Animator LightChange;
    //[SerializeField] private GameObject LightPlayer;

    //Intensity
    void Start()
    {
        
    }


    void Update()
    {
        //GlobalLight.intensity = Mathf.Clamp(light2D.intensity, 0f, 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            LightChange.Play("LightsDown");
        }  
    }
}
