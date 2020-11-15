using ProjectReversing.Handlers;
using ProjectReversing.Traits;
using System;
using System.Collections;
using UnityEngine;
namespace ProjectReversing.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        //Singleton
        public static PlayerMovement singleton;

        //Assingables
        [SerializeField] private Transform playerCam;
        [SerializeField] private Transform orientation;
        [SerializeField] private AudioSource footstepAudioSource;
        [SerializeField] private AudioSource fallingAudioSource;

        //Other
        private Rigidbody rb;

        //Rotation and look
        private float xRotation;
        private float sensitivity = 50f;

        //Movement
        [SerializeField] private float moveSpeed = 4500;
        [SerializeField] private float maxSpeed = 20;
        [SerializeField] private bool grounded;
        [SerializeField] private LayerMask GroundLayer;

        [SerializeField] private float counterMovement = 0.175f;
        private float threshold = 0.01f;
        [SerializeField] private float maxSlopeAngle = 35f;

        //Crouch & Slide
        private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
        private Vector3 playerScale;
        [SerializeField] private float slideForce = 400;
        [SerializeField] private float slideCounterMovement = 0.2f;

        //Jumping
        private bool readyToJump = true;
        private float jumpCooldown = 0.25f;
        [SerializeField] private float jumpForce = 550f;

        //Input
        private float x, y;
        private bool jumping;

        //Sliding
        private Vector3 normalVector = Vector3.up;
        private Vector3 wallNormalVector;

        void Awake()
        {
            if (singleton == null)
            {
                singleton = this;
            }
            rb = GetComponent<Rigidbody>();
        }

        void Start()
        {
            playerScale = transform.localScale;

            if (PlayerUI.singleton == null)
            {
                GameHandler.LockCursor();
            }
        }
        private void FixedUpdate()
        {
            if (PlayerUI.singleton != null)
            {
                if (PlayerUI.singleton.isPaused)
                {
                    return;
                }
            }
            Movement();
        }
        private void Update()
        {
            if (PlayerUI.singleton != null)
            {
                if (PlayerUI.singleton.isPaused)
                {
                    return;
                }
            }
            GetInputs();
            CameraLook();
            SoundEffects();
        }

        /// <summary>
        /// Find user input. Should put this in its own class but im lazy
        /// </summary>
        private void GetInputs()
        {
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");
            jumping = Input.GetKey(KeyHandler.Jump);

            /*
            crouching = Input.GetKey(KeyCode.LeftControl);

            //Crouching
            if (Input.GetKeyDown(KeyCode.LeftControl))
                StartCrouch();
            if (Input.GetKeyUp(KeyCode.LeftControl))
                StopCrouch();
            */
        }
        private void Movement()
        {
            //Extra gravity
            rb.AddForce(Vector3.down * Time.fixedDeltaTime * 10);

            //Find actual velocity relative to where player is looking
            Vector2 mag = FindVelRelativeToLook();
            float xMag = mag.x, yMag = mag.y;

            //Counteract sliding and sloppy movement
            CounterMovement(x, y, mag);

            //If holding jump && ready to jump, then jump
            if (readyToJump && jumping) Jump();

            //Set max speed
            float maxSpeed = this.maxSpeed;

            //If speed is larger than maxspeed, cancel out the input so you don't go over max speed
            if (x > 0 && xMag > maxSpeed) x = 0;
            if (x < 0 && xMag < -maxSpeed) x = 0;
            if (y > 0 && yMag > maxSpeed) y = 0;
            if (y < 0 && yMag < -maxSpeed) y = 0;

            //Some multipliers
            float multiplier = 1f, multiplierV = 1f;

            // Movement in air
            if (!grounded)
            {
                multiplier = 0.5f;
                multiplierV = 0.5f;
            }

            //Apply forces to move player
            rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.fixedDeltaTime * multiplier * multiplierV);
            rb.AddForce(orientation.transform.right * x * moveSpeed * Time.fixedDeltaTime * multiplier);
        }

        private void Jump()
        {
            if (grounded && readyToJump)
            {
                readyToJump = false;

                //Add jump forces
                rb.AddForce(Vector2.up * jumpForce * 1.5f);
                rb.AddForce(normalVector * jumpForce * 0.5f);

                //If jumping while falling, reset y velocity.
                Vector3 vel = rb.velocity;
                if (rb.velocity.y < 0.5f)
                    rb.velocity = new Vector3(vel.x, 0, vel.z);
                else if (rb.velocity.y > 0)
                    rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);

                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }

        private void ResetJump()
        {
            readyToJump = true;
        }

        private float desiredX;
        private void CameraLook()
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * GameHandler.sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * GameHandler.sensitivity;

            //Find current look rotation
            Vector3 rot = playerCam.transform.localRotation.eulerAngles;
            desiredX = rot.y + mouseX;

            //Rotate, and also make sure we dont over- or under-rotate.
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            //Perform the rotations
            playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
            orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
        }

        private void SoundEffects()
        {
            if (rb.velocity.magnitude >= 2f && grounded)
            {
                footstepAudioSource.volume = Mathf.Lerp(footstepAudioSource.volume, 0.03f * GameHandler.volume, Time.fixedDeltaTime * 5f);
            }
            else if (rb.velocity.magnitude < 2f || !grounded)
            {
                footstepAudioSource.volume = Mathf.Lerp(footstepAudioSource.volume, 0f, Time.fixedDeltaTime * 5f);
            }
            if (!grounded)
            {
                fallingAudioSource.volume = Mathf.Lerp(fallingAudioSource.volume, 0.08f * GameHandler.volume, Time.fixedDeltaTime * 5f);
            } else
            {
                fallingAudioSource.volume = Mathf.Lerp(fallingAudioSource.volume, 0f * GameHandler.volume, Time.fixedDeltaTime * 5f);
            }
        }

        private void CounterMovement(float x, float y, Vector2 mag)
        {
            if (!grounded || jumping) return;

            //Counter movement
            if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
            {
                rb.AddForce(moveSpeed * orientation.transform.right * Time.fixedDeltaTime * -mag.x * counterMovement);
            }
            if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
            {
                rb.AddForce(moveSpeed * orientation.transform.forward * Time.fixedDeltaTime * -mag.y * counterMovement);
            }

            //Limit diagonal running. This will also cause a full stop if sliding fast and un-crouching, so not optimal.
            if (Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) > maxSpeed)
            {
                float fallspeed = rb.velocity.y;
                Vector3 n = rb.velocity.normalized * maxSpeed;
                rb.velocity = new Vector3(n.x, fallspeed, n.z);
            }
        }

        /// <summary>
        /// Find the velocity relative to where the player is looking
        /// Useful for vectors calculations regarding movement and limiting movement
        /// </summary>
        /// <returns></returns>
        public Vector2 FindVelRelativeToLook()
        {
            float lookAngle = orientation.transform.eulerAngles.y;
            float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

            float u = Mathf.DeltaAngle(lookAngle, moveAngle);
            float v = 90 - u;

            float magnitue = rb.velocity.magnitude;
            float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
            float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

            return new Vector2(xMag, yMag);
        }

        private bool IsFloor(Vector3 v)
        {
            float angle = Vector3.Angle(Vector3.up, v);
            return angle < maxSlopeAngle;
        }

        private bool cancellingGrounded;

        /// <summary>
        /// Handle ground detection
        /// </summary>
        private void OnCollisionStay(Collision other)
        {
            //Make sure we are only checking for walkable layers
            int layer = other.gameObject.layer;
            if (GroundLayer != (GroundLayer | (1 << layer))) return;

            //Iterate through every collision in a physics update
            for (int i = 0; i < other.contactCount; i++)
            {
                Vector3 normal = other.contacts[i].normal;
                //FLOOR
                if (IsFloor(normal))
                {
                    grounded = true;
                    cancellingGrounded = false;
                    normalVector = normal;
                    CancelInvoke(nameof(StopGrounded));
                }
            }

            //Invoke ground/wall cancel, since we can't check normals with CollisionExit
            float delay = 3f;
            if (!cancellingGrounded)
            {
                cancellingGrounded = true;
                Invoke(nameof(StopGrounded), Time.fixedDeltaTime * delay);
            }
        }

        private void StopGrounded()
        {
            grounded = false;
        }
    }
}