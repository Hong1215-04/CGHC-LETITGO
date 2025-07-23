using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Movement : PlayerStates
{
    [Header("Settings")]
    [SerializeField] private float speed = 10f;

    private float _horizontalMovement;
    private float _movement;
    [SerializeField] private float slow = 0.5f;

    protected override void InitState()
    {
        base.InitState();
    }

    public override void ExecuteState()
    {
        MovePlayer();
        IceMoveAdd();
        CheckStand();
    }

    private void IceMoveAdd()
    {
        if (_movement == 0f && _playerController.Conditions.StopIce == true)
        {
            if (_playerController.Conditions.IceRight == true)
            {
                float IceStay = 1f;
                _playerController.SetHorizontalForce(IceStay);
                _playerController.Conditions.IceRight = false;
            }
            else if (_playerController.Conditions.IceLeft == true)
            {
                float IceStay = 1f;
                _playerController.SetHorizontalForce(-IceStay);
                _playerController.Conditions.IceLeft = false;
            }
        }
    }

    // private void WaterMoveAdd()
    // {
    //     if (_playerController.Conditions.isWater == true)
    //     {
    //         if (_playerController.Conditions.waterright == true)
    //         {
    //              -= 20f;
    //             _playerController.Conditions.waterright = false;
    //             _playerController.Conditions.isWater = false;  
    //         }

    //         if (_playerController.Conditions.waterleft == true)
    //         {
    //             _movement += 20f;
    //             _playerController.Conditions.waterleft = false;
    //             _playerController.Conditions.isWater = false;
    //         }
    //     }
    // }

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

        if (_playerController.Conditions.isWater == true)
        {
            _movement = slow * _horizontalMovement;
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
        // else if (collision.gameObject.layer == LayerMask.NameToLayer("Fire"))
        // {
        //     _playerController.Conditions.TimeStop = true;
        // }
    }

    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.gameObject.layer == LayerMask.NameToLayer("Fire"))
    //     {
    //         _playerController.Conditions.TimeStop = false;
    //     }
    // }

    private void OnTriggerStay2D(Collider2D waterIn)
    {
        if (waterIn.gameObject.layer == LayerMask.NameToLayer("Slow"))
        {
            Debug.Log("Nice");
            _playerController.Conditions.isWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D waterIn)
    {
        if (waterIn.gameObject.layer == LayerMask.NameToLayer("Slow"))
        {
            Debug.Log("End");
            _playerController.Conditions.isWater = false;
        }
    }

    private void CheckStand()
    {
        if (_movement == 0)
        {
            _playerController.Conditions.Stand = true;
        }
        else
        {
            _playerController.Conditions.Stand = false;
        }
    }
}
