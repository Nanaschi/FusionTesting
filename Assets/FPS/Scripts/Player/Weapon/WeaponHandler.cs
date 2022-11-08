using System.Reflection;
using Fusion;
using Network;
using UnityEngine;

namespace Movement.Weapon
{
    public class WeaponHandler : NetworkBehaviour
    {
        [SerializeField] private ParticleSystem _fireParticleSystem;

        private ParticleSystem FireParticleSystem => _fireParticleSystem;

        [Networked(OnChanged = nameof(PerformFire))]
        private bool IsFiring { get; set; }

        public override void FixedUpdateNetwork()
        {
            if (GetInput(out NetworkInputData networkInputData))
            {
                 IsFiring = networkInputData.IsFirePressed;
            }
        }


        static void PerformFire(Changed<WeaponHandler> changed)
        {
            if (changed.Behaviour.IsFiring)
            {
                changed.Behaviour.FireParticleSystem.Play();
                Debug.Log(changed.Behaviour.IsFiring);
            }
            
        }
    }
}