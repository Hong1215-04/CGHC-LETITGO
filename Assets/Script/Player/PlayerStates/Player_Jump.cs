using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Callbacks;
using UnityEngine;

public class Player_Jump : PlayerStates
{
    [Header("Settings")]
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private int maxJumps = 1;
    [SerializeField] private float DashDistance = 2f;
    [SerializeField] private int maxDash = 1;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private Rigidbody2D rb;

    // Return how many jumps we have left
    public int JumpsLeft { get; set; }
    public int DashLeft { get; set; }

    float zerogravity = 0f;
    float dashtest = 5;

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

        StartCoroutine(Dash());
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

    private IEnumerator Dash()
    {
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        GetComponent<Rigidbody2D>().AddForce(transform.right * DashDistance);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        GetComponent<Rigidbody2D>().AddForce(-transform.right * DashDistance);
    }
}

//added force must be minus back