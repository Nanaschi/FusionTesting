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
        private float RayLength
        {
            get => _rayLength;
            set => _rayLength = value;
        }

        public Camera PlayerCamera { get; set; }



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
                    (changed.Behaviour.PlayerCamera.transform.position,
                        changed.Behaviour.PlayerCamera.transform.TransformDirection(Vector3.forward) ,
                        changed.Behaviour.RayLength,
                        changed.Behaviour.Object.InputAuthority, 
                        out var hitInfo, 
                        changed.Behaviour.LayerMask, HitOptions.IncludePhysX);
                Debug.Log(changed.Behaviour.IsFiring);
                //Unity built in colliders
                if (hitInfo.Collider) print(hitInfo.GameObject.name);
                //Fusion component for raycast detection 
                if (hitInfo.Hitbox) print(hitInfo.GameObject.name);
                Debug.DrawRay(changed.Behaviour.PlayerCamera.transform.position,
                    changed.Behaviour.PlayerCamera.transform.TransformDirection(Vector3.forward) 
                    * changed.Behaviour.RayLength, Color.red, .5f);
            }
            
        }
    }
}