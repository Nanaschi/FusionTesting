using System;
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



        [SerializeField] private float _rayLength;

        public float RayLength
        {
            get => _rayLength;
            set => _rayLength = value;
        }

        [SerializeField] private LayerMask _layerMask;

        public LayerMask LayerMask
        {
            get => _layerMask;
            set => _layerMask = value;
        }

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
                changed.Behaviour.Runner.LagCompensation.Raycast
                    (changed.Behaviour.transform.position,
                        Vector3.forward,
                        changed.Behaviour.RayLength,
                        changed.Behaviour.Object.InputAuthority, 
                        out var hitInfo, 
                        changed.Behaviour.LayerMask, HitOptions.IncludePhysX);
                Debug.Log(changed.Behaviour.IsFiring);
                if (hitInfo.Collider != null) print(hitInfo.GameObject.name);
                Debug.DrawRay(changed.Behaviour.transform.position,
                    changed.Behaviour.transform.TransformDirection(Vector3.forward) 
                    * changed.Behaviour.RayLength, Color.red, 6);
            }
            
        }
    }
}