using Fusion;
using UnityEngine;

namespace VanillaTutorial
{
    public struct NetworkInputData: INetworkInput
    {
        public const byte MOUSEBUTTON1 = 0x01;

        public byte buttons;
        public Vector3 direction;
    }
}