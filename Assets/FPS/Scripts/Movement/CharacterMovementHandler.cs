using FPS.Scripts.Extensions;
using Fusion;
using Movement;
using Network;
using UnityEngine;

public class CharacterMovementHandler : NetworkBehaviour
{
    [SerializeField] private NetworkCharacterControllerCustom _networkCharacterControllerCustom;
    [SerializeField] private Camera _localCamera;
    [SerializeField] private float _deathYZone;
    [SerializeField] private float _rangeToSpawn;
    private float _rotY;

    private CharacterMovementHandler _localPlayer;

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            print("spawned local");
            _localPlayer = this;
        }
        else
        {
            print("spawned remote");
            _localCamera.gameObject.SetActive(false);
        }
    }

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
        localCameraRotation = Quaternion.Euler(-_rotY, localCameraRotation.y, localCameraRotation.z);
        _localCamera.transform.localRotation = localCameraRotation;
    }


    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData networkInputData))
        {
            //View
            _networkCharacterControllerCustom.Rotate(networkInputData.RotationInput, _localCamera);


            //Move

            Vector3 moveDirection = transform.forward * networkInputData.MovementInput.z +
                                    transform.right * networkInputData.MovementInput.x;

            var old = 5 * networkInputData.MovementInput * Runner.DeltaTime;
            moveDirection.Normalize();
            _networkCharacterControllerCustom.Move(moveDirection);

            if (networkInputData.IsJumpPressed) _networkCharacterControllerCustom.Jump();
        }

        if (transform.position.y <= _deathYZone) transform.position =
            new Vector3().GetRandomSpawnPosition(_rangeToSpawn, 1.1f);
    }
}