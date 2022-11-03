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
    [SerializeField] private NetworkCharacterControllerCustom _networkCharacterControllerCustom;
    [SerializeField] private Camera _localCamera;
    private float _rotY;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        _rotY += Input.GetAxis("Mouse Y") * _networkCharacterControllerCustom.RotationSpeed * Time.deltaTime;
        _rotY = Mathf.Clamp(_rotY, -_networkCharacterControllerCustom.VeiwYClamp, _networkCharacterControllerCustom.VeiwYClamp);
        var localCameraRotation = _localCamera.transform.localRotation;
        localCameraRotation = Quaternion.Euler(-_rotY,   
            localCameraRotation.y,   localCameraRotation.z);
        _localCamera.transform.localRotation = localCameraRotation;
    }



    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData networkInputData))
        {
            //View
            _networkCharacterControllerCustom.Rotate(networkInputData.RotationInput, _localCamera);
            

            //Move

            networkInputData.MovementInput.Normalize();
            _networkCharacterControllerCustom.Move(5 * networkInputData.MovementInput * Runner.DeltaTime);

            if (networkInputData.IsJumpPressed) _networkCharacterControllerCustom.Jump();
        }
    }
}