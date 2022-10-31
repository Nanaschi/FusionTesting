using Fusion;
using UnityEngine;

namespace Network
{
    public struct NetworkInputData : INetworkInput
    {
        private Vector2 _movementInput;
        private float _rotationInput;
        private NetworkBool _isJumpPressed;

        public Vector2 MovementInput
        {
            get => _movementInput;
            set => _movementInput = value;
        }

        public float RotationInput => _rotationInput;

        public NetworkBool IsJumpPressed => _isJumpPressed;
    }
}