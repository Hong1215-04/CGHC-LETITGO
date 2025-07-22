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
        IceMoveAdd();
    }

    private void IceMoveAdd()
    {
        if (_movement == 0f)
        {
            if (_playerController.Conditions.IceRight == true)
            {
                float IceStay = 0.1f * 20f;
                _playerController.SetHorizontalForce(IceStay);
                _playerController.Conditions.IceRight = false;
            }
            else if (_playerController.Conditions.IceLeft == true)
            {
                float IceStay = 0.1f * 20f;
                _playerController.SetHorizontalForce(-IceStay);
                _playerController.Conditions.IceLeft = false;
            }  
        }
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
        moveSpeed = EvaluateFriction(moveSpeed);

        _playerController.SetHorizontalForce(moveSpeed);
    }

    // Initialize our internal movement direction   
    protected override void GetInput()
    {
        _horizontalMovement = _horizontalInput;
    }

    private float EvaluateFriction(float moveSpeed)
    {
        if (_playerController.Friction > 0)
        {
            moveSpeed = Mathf.Lerp(_playerController.Force.x, moveSpeed, Time.deltaTime * 10f * _playerController.Friction);
        }

        return moveSpeed;
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
