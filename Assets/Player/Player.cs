using System.Collections;
using UnityEngine;

namespace MemezawyDev.Player
{
    [RequireComponent(typeof(Shooting.PlayerShootingManager))]
    [RequireComponent(typeof(Input.PlayerInputManager))]
    [RequireComponent(typeof(Movement.PlayerMotionManager))]
    [RequireComponent(typeof(PhysicsController))]
    public class Player : MonoBehaviour
    {
        public Input.PlayerInputManager Input { get; private set; }
        public Movement.PlayerMotionManager Movement { get; private set; }
        public Shooting.PlayerShootingManager Shooting { get; private set; }
        public PhysicsController PhysicsController { get; private set; }
        public static Player Instance { get; private set; }


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

            Input = GetComponent<Input.PlayerInputManager>();
            Movement = GetComponent<Movement.PlayerMotionManager>();
            Shooting = GetComponent<Shooting.PlayerShootingManager>();
            PhysicsController = GetComponent<PhysicsController>();
        }
    }
}