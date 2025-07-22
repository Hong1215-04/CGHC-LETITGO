using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : PlayerStates
{
    [Header("Settings")]
    [SerializeField] private float speed = 10f;

    private float _horizontalMovement;
    private float _movement;

    protected override void InitState()
    {
        base.InitState();
    }

    public override void ExecuteState()
    {
        MovePlayer();
    }

    // Moves our Player    
    private void MovePlayer()
    {
        if (Mathf.Abs(_horizontalMovement) > 0.1f)
        {
            _movement = _horizontalMovement;
        }
        else
        {
            _movement = 0f;
        }

        float moveSpeed = _movement * speed;
        _playerController.SetHorizontalForce(moveSpeed);
    }

    // Initialize our internal movement direction   
    protected override void GetInput()
    {
        _horizontalMovement = _horizontalInput;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Spike"))
        {
            Debug.Log("Player hit spike!");
            GetComponent<PlayerHealth>().Kill();
        }
    }
}
