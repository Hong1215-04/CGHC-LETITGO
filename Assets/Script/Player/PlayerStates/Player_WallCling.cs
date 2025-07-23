using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WallCling : PlayerStates
{
    [Header("Settings")] 
    [SerializeField] private float fallFactor = 0.5f;
    
    protected override void GetInput()
    {
        if (_horizontalInput <= -0.1f || _horizontalInput >= 0.1f)
        {
            WallCling();
        }
    }

    public override void ExecuteState()
    {
        ExitWallCling();
    }

    private void WallCling()
    {
        if (_playerController.Conditions.IsCollidingBelow || _playerController.Force.y >= 0) //on the FLOOR or in the AIR
        {
            _playerController.Conditions.IsWallClinging = false;
            _playerController.SetWallClingMultiplier(0f);  
        }

        if (_playerController.Conditions.IsCollidingLeft && _horizontalInput <= -0.1f)
        {
            _playerController.SetWallClingMultiplier(fallFactor);
            _playerController.Conditions.IsWallClinging = true;
            _playerController.Conditions.IsCollidingLeft = false;
        }
        if (_playerController.Conditions.IsColRight && _horizontalInput >= 0.1f)
        {
            Debug.Log("WallRighttrue");
            _playerController.SetWallClingMultiplier(fallFactor);
            _playerController.Conditions.IsWallClinging = true;
            _playerController.Conditions.IsColRight = false;
        }
    }

    private void ExitWallCling()
    {
        if (_playerController.Conditions.IsWallClinging)
        {
            if (_playerController.Conditions.IsCollidingBelow || _playerController.Force.y >= 0)
            {
                _playerController.SetWallClingMultiplier(0f);
                _playerController.Conditions.IsWallClinging = false;
            }
            //Improve for clinging without wall bugs
            if (_playerController.Conditions.IsCollidingAbove)
            {
                _playerController.SetWallClingMultiplier(0f);
                _playerController.Conditions.IsWallClinging = false;
            }

            if (_playerController.FacingRight)
            {
                if (_horizontalInput <= -0.0001f || _horizontalInput < 0.0001f)
                {
                    _playerController.SetWallClingMultiplier(0f);
                    _playerController.Conditions.IsWallClinging = false;
                }
            }
            else
            {
                if (_horizontalInput >= 0.0001f || _horizontalInput > -0.0001f)
                {
                    _playerController.SetWallClingMultiplier(0f);
                    _playerController.Conditions.IsWallClinging = false;
                }
            }
        }
    }
}
