using System;
using System.Collections;
using System.Collections.Generic;
using Network;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    private Vector2 _moveInputVector;
    private Vector2 _viewInputVector;

    private void Update()
    {
        //View Input
        _viewInputVector.x = Input.GetAxis("Mouse X");
        _viewInputVector.y = Input.GetAxis("Mouse Y");
        //Move Input 
        _moveInputVector.x = Input.GetAxis("Horizontal");
        _moveInputVector.y = Input.GetAxis("Vertical");
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData();
        networkInputData.MovementInput = _moveInputVector;
        return networkInputData;
    }
}
