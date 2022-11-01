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

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData networkInputData))
        {
            networkInputData.MovementInput.Normalize();
            _networkCharacterControllerCustom.Move(5 * networkInputData.MovementInput * Runner.DeltaTime);
            /*Vector3 moveDirection = transform.forward * networkInputData.MovementInput.z +
                                    transform.right * networkInputData.MovementInput.x;
            moveDirection.Normalize();
            _networkCharacterControllerCustom.Move(moveDirection);*/
        }
    }
    
    
}