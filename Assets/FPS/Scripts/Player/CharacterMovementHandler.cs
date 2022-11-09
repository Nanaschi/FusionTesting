using System;
using FPS.Scripts.Extensions;
using Fusion;
using Movement;
using Movement.Weapon;
using Network;
using UnityEngine;

public class CharacterMovementHandler : NetworkBehaviour
{
    [SerializeField] private NetworkCharacterControllerCustom _networkCharacterControllerCustom;
    [SerializeField] private Camera _localCamera;
    [SerializeField] private Transform _headReplacement;
    [SerializeField] private float _deathYZone;
    [SerializeField] private WeaponHandler _weaponHandler;
    private float _rangeToSpawn;


    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _weaponHandler.PlayerCamera = _localCamera;
    }

    public float RangeToSpawn
    {
        get => _rangeToSpawn;
        set => _rangeToSpawn = value;
    }

    private float _rotY;

    private CharacterMovementHandler _localPlayer;

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            //spawned local
            _localPlayer = this;
        }
        else
        {
            //spawned remote
            _localCamera.gameObject.SetActive(false);
        }

        gameObject.name = $"Player {Object.Id}";
    }

    private void Start()
    {
        LockMouse();
    }

    private static void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!Object.HasInputAuthority) return;
        CameraControl();
    }

    private void CameraControl()
    {
        _rotY += Input.GetAxis("Mouse Y") * _networkCharacterControllerCustom.RotationSpeed * Time.deltaTime;
        _rotY = Mathf.Clamp(_rotY, -_networkCharacterControllerCustom.VeiwYClamp, _networkCharacterControllerCustom.VeiwYClamp);
        var localCameraRotation = _headReplacement.transform.localRotation;
        localCameraRotation = Quaternion.Euler(-_rotY, localCameraRotation.y, localCameraRotation.z);
        _headReplacement.transform.localRotation = localCameraRotation;
    }


    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData networkInputData))
        {
            //View
            _networkCharacterControllerCustom.Rotate(networkInputData.RotationInput, _headReplacement);


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