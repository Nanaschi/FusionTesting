using System.Reflection;
using Fusion;
using Network;
using UnityEngine;

namespace Movement.Weapon
{
    public class WeaponHandler : NetworkBehaviour
    {
        private bool _isFiring;
        [SerializeField] private ParticleSystem _fireParticleSystem;

        private ParticleSystem FireParticleSystem => _fireParticleSystem;

        [Networked(OnChanged = nameof(OnFireChanged))]
        private bool IsFiring
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
            if (changed.Behaviour.IsFiring)
            {
                changed.Behaviour.FireParticleSystem.Play();
                Debug.Log(changed.Behaviour.IsFiring);
            }
            
        }
    }
}