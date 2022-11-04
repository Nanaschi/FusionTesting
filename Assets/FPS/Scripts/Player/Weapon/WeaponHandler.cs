using System.Reflection;
using Fusion;
using Network;
using UnityEngine;

namespace Movement.Weapon
{
    public class WeaponHandler : NetworkBehaviour
    {
        private bool _isFiring;

        [Networked(OnChanged = nameof(OnFireChanged))]
        public bool IsFiring
        {
            get => _isFiring;
            set => _isFiring = value;
        }

        public override void FixedUpdateNetwork()
        {
            if (GetInput(out NetworkInputData networkInputData))
            {
                 IsFiring = networkInputData.IsFirePressed;
            }
        }


        static void OnFireChanged(Changed<WeaponHandler> changed)
        {
            
            Debug.Log(changed.Behaviour.IsFiring);
        }
    }
}