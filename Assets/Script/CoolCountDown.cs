using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolCountDown : MonoBehaviour
{
    float elapsedTime;
    private bool CountingDownOrNot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        Debug.Log(elapsedTime);
    }
}
