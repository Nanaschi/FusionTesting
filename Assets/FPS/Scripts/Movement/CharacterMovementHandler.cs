using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Fusion;
using Movement;
using Network;
using UnityEngine;

public class CharacterMovementHandler : NetworkBehaviour
{
    private Vector2 _viewInput;
    [SerializeField] private NetworkCharacterControllerCustom _networkCharacterControllerCustom;
    private float _cameraRotationY = 0;
    [SerializeField] private Camera _localCamera;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void Update()
    {
        _cameraRotationY = _viewInput.y * Time.deltaTime;
        _cameraRotationY = Mathf.Clamp(_cameraRotationY, -90, 90);
        
        _localCamera.transform.rotation = Quaternion.Euler(_cameraRotationY,0,0);
    }

    
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData networkInputData))
        {
            //View
            _networkCharacterControllerCustom.Rotate(networkInputData.RotationInput);
            
            
            //Move
            
            networkInputData.MovementInput.Normalize();
            _networkCharacterControllerCustom.Move(5 * networkInputData.MovementInput * Runner.DeltaTime);
            
            if(networkInputData.IsJumpPressed) _networkCharacterControllerCustom.Jump();
        }
    }
}