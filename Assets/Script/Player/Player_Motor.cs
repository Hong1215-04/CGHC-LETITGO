using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Motor : MonoBehaviour
{
    private PlayerStates[] _playerStates;

    private void Start()
    {
        _playerStates = GetComponents<PlayerStates>();
    }

    private void Update()
    {
        if (_playerStates.Length != 0)
        {
            foreach (PlayerStates state in _playerStates)
            {
                state.LocalInput();
                state.ExecuteState();
            }
        }
    }

}
