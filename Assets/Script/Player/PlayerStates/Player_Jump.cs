using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class Player_Jump : PlayerStates
{
    [Header("Settings")]
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float DashDistance = 2f;
    [SerializeField] private int maxDash = 2;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private int dashingCount = 40;
    [SerializeField] private TrailRenderer tr;

    // Return how many jumps we have left
    public int JumpsLeft { get; set; }
    public int DashLeft { get; set; }

    protected override void InitState()
    {
        base.InitState();
        JumpsLeft = maxJumps;
        DashLeft = maxDash;
    }

    public override void ExecuteState()
    {
        if (_playerController.Conditions.IsCollidingBelow && _playerController.Force.y == 0f)
        {
            JumpsLeft = maxJumps;
            DashLeft = maxDash;
            _playerController.Conditions.IsJumping = false;
            //_playerController.SetHorizontalForce(0);
        }
    }


    protected override void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            DashCon();
        }
    }

    private void Jump()
    {
        if (!CanJump())
        {
            return;
        }

        if (JumpsLeft == 0)
        {
            return;
        }

        JumpsLeft -= 1;

        _playerController.Conditions.IsJumping = true;
        float jumpForce = Mathf.Sqrt(jumpHeight * 2f * Mathf.Abs(_playerController.Gravity));
        _playerController.SetVerticalForce(jumpForce);
    }

    private bool CanJump()
    {
        if (!_playerController.Conditions.IsCollidingBelow && JumpsLeft <= 0)
        {
            return false;
        }

        if (_playerController.Conditions.IsCollidingBelow && JumpsLeft <= 0)
        {
            return false;
        }

        return true;
    }

    private void DashCon()
    {
        if (!CanDash())
        {
            return;
        }

        if (DashLeft == 0)
        {
            return;
        }

        DashLeft -= 1;

        _playerController.Conditions.IsJumping = false;

        if (_playerController.FacingRight == true)
        {
            StartCoroutine(DashRight());
        }
        else if (_playerController.FacingRight == false)
        {
            StartCoroutine(DashLeftSide());
        }
    }

    private bool CanDash()
    {
        if (!_playerController.Conditions.IsCollidingBelow && DashLeft <= 0)
        {
            return false;
        }

        if (_playerController.Conditions.IsCollidingBelow && DashLeft <= 0)
        {
            return false;
        }

        return true;
    }

    // private IEnumerator Dash()
    // {
    //     _playerController.SetVerticalForce(zerogravity);
    //     rb.isKinematic = false;
    //     float originalGravity = rb.gravityScale;
    //     rb.gravityScale = 0f;
    //     rb.velocity = new Vector2(transform.localScale.x * DashDistance, 0f);
    //     tr.emitting = true;
    //     yield return new WaitForSeconds(dashingTime);
    //     tr.emitting = false;
    //     rb.gravityScale = originalGravity;
    //     rb.isKinematic = true;
    //     _playerController.SetVerticalForce(normalgravity);
    // }

    private IEnumerator DashRight()
    {
        _playerController.Conditions.IsDashing = true;
        tr.emitting = true;
        float dashForce = DashDistance * 20f;
        for (int i = 0; i < dashingCount; i++)
        {
            if (_playerController.Conditions.IsJumping == false)
            {
                _playerController.SetHorizontalForce(dashForce);
                yield return new WaitForSeconds(dashingTime);
            }
        }
        _playerController.Conditions.IsDashing = false;
        _playerController.SetHorizontalForce(0);
        tr.emitting = false;
    }
    
    private IEnumerator DashLeftSide()
    {
        _playerController.Conditions.IsDashing = true;
        tr.emitting = true;
        float dashForce = -DashDistance * 20f;
        for (int i = 0; i < dashingCount; i++)
        {
            if (_playerController.Conditions.IsJumping == false)
            {
                _playerController.SetHorizontalForce(dashForce);
                yield return new WaitForSeconds(dashingTime);
            }
        }
        _playerController.Conditions.IsDashing = false;
        _playerController.SetHorizontalForce(0);
        tr.emitting = false;
    }
}

//added force must be minus back