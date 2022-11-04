using Fusion;
using UnityEngine;

namespace Network
{
    public struct NetworkInputData : INetworkInput
    {
        private Vector3 _movementInput;
        private Vector2 _rotationInput;
        private NetworkBool _isJumpPressed;
        private NetworkBool _isFirePressed;

        public NetworkBool IsFirePressed
        {
            get => _isFirePressed;
            set => _isFirePressed = value;
        }

        public Vector3 MovementInput
        {
            get => _movementInput;
            set => _movementInput = value;
        }

        public Vector2 RotationInput
        {
            get => _rotationInput;
            set => _rotationInput = value;
        }

        public NetworkBool IsJumpPressed
        {
            get => _isJumpPressed;
            set => _isJumpPressed = value;
        }
    }
}