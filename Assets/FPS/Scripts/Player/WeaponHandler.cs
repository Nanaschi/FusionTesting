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

        [SerializeField] private float _shootFrequency;

        [SerializeField] private float _rayLength;
        [SerializeField] private byte _amountOfDamage;

        private Camera _playerCamera;

        public Camera PlayerCamera
        {
            set => _playerCamera = value;
        }


        [SerializeField] private LayerMask _layerMask;

        private LayerMask LayerMask
        {
            get => _layerMask;
            set => _layerMask = value;
        }

        [Networked(OnChanged = nameof(PerformFire))]
        private bool IsFiring { get; set; }

        [Networked] private TickTimer _shootFrequencyTick { get; set; }

        private void InitTickTimer()
        {
            _shootFrequencyTick = TickTimer.CreateFromSeconds(Runner, _shootFrequency);
        }

        public override void FixedUpdateNetwork()
        {
            if (GetInput(out NetworkInputData networkInputData))
            {
                IsFiring = networkInputData.IsFirePressed;
            }
        }


        static void PerformFire(Changed<WeaponHandler> changed)
        {
            if (changed.Behaviour.IsFiring && changed.Behaviour._shootFrequencyTick.ExpiredOrNotRunning(changed.Behaviour.Runner))
            {
                changed.Behaviour.InitTickTimer();
                changed.Behaviour.FireParticleSystem.Play();
                changed.Behaviour.Runner.LagCompensation.Raycast(changed.Behaviour._playerCamera.transform.position,
                    changed.Behaviour._playerCamera.transform.TransformDirection(Vector3.forward), changed.Behaviour._rayLength,
                    changed.Behaviour.Object.InputAuthority, out var hitInfo, changed.Behaviour.LayerMask,
                    HitOptions.IncludePhysX);
                Debug.Log(changed.Behaviour.IsFiring);
                //Unity built in colliders
                if (hitInfo.Collider)
                {
                    Debug.DrawRay(changed.Behaviour._playerCamera.transform.position,
                        changed.Behaviour._playerCamera.transform.TransformDirection(Vector3.forward) *
                        changed.Behaviour._rayLength, Color.green, .5f);
                    print(hitInfo.GameObject.name);
                }

                //Fusion component for raycast detection 
                if (hitInfo.Hitbox && changed.Behaviour.HasInputAuthority)
                {
                    hitInfo.Hitbox.Root.GetComponent<HPHandler>().TakeDamage(changed.Behaviour._amountOfDamage);
                    Debug.DrawRay(changed.Behaviour._playerCamera.transform.position,
                        changed.Behaviour._playerCamera.transform.TransformDirection(Vector3.forward) *
                        changed.Behaviour._rayLength, Color.red, .5f);
                    print(hitInfo.GameObject.name);
                }
            }
        }
    }
}