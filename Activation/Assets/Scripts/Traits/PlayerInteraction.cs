using ProjectReversing.Handlers;
using ProjectReversing.Interfaces;
using ProjectReversing.Objects;
using UnityEngine;
namespace ProjectReversing.Traits
{
    public class PlayerInteraction : MonoBehaviour
    {
        #region Singleton
        public static PlayerInteraction singleton;
        private void Awake()
        {
            if (singleton == null)
            {
                singleton = this;
            }
        }
        #endregion
        [Header("Pickup")]
        [SerializeField] private Transform pickupParent;
        public GameObject currentlyPickedUpObject;
        private Rigidbody pickupRB;

        [Header("ObjectFollow")]
        [SerializeField] private float minSpeed = 0;
        [SerializeField] private float maxSpeed = 300f;
        [SerializeField] private float maxDistance = 10f;
        private float currentSpeed = 0f;
        private float currentDist = 0f;

        [Header("InteractableInfo")]
        public float sphereCastRadius = 0.5f;
        private Vector3 raycastPos;
        public GameObject lookObject;
        private IHoldable physicsObject;
        private Camera mainCamera;

        [Header("Rotation")]
        public float rotationSpeed = 100f;
        Quaternion lookRot;
        Vector3 oldEulerAngles;

        public float range = 10f;
        RaycastHit _hitInfo;
        RaycastHit _Holdablehit;
        private void Update()
        {
            if (Input.GetKeyDown(KeyHandler.Interact))
            {
                if (Physics.Raycast(transform.position, transform.forward, out _hitInfo, range))
                {
                    IInteractable interactable = _hitInfo.transform.GetComponent<IInteractable>();
                    if (interactable != null)
                    {
                        interactable.Interact();
                    }
                }
            }
            if (Physics.Raycast(transform.position, mainCamera.transform.forward, out _Holdablehit, maxDistance))
            {
                IHoldable holdable = _Holdablehit.collider.GetComponent<IHoldable>();
                if (holdable != null)
                {
                    lookObject = _Holdablehit.collider.gameObject;
                }
            }
            else
            {
                lookObject = null;
            }
            //if we press the button of choice
            if (Input.GetKeyDown(KeyHandler.PickUp))
            {
                //and we're not holding anything
                if (currentlyPickedUpObject == null)
                {
                    //and we are looking an interactable object
                    if (lookObject != null)
                    {
                        PickUpObject();
                    }
                }
                //if we press the pickup button and have something, we drop it
                else
                {
                    BreakConnection();
                }
            }
        }
        private void Start()
        {
            mainCamera = Camera.main;
        }

        //A simple visualization of the point we're following in the scene view
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            //Gizmos.DrawSphere(pickupParent.position, 0.5f);
        }

        //Velocity movement toward pickup parent and rotation
        private void FixedUpdate()
        {
            if (currentlyPickedUpObject != null && pickupRB != null)
            {
                currentDist = Vector3.Distance(pickupParent.position, pickupRB.position);
                currentSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, currentDist / maxDistance);
                currentSpeed *= Time.fixedDeltaTime;
                Vector3 direction = pickupParent.position - pickupRB.position;
                pickupRB.velocity = direction.normalized * currentSpeed;
                //Rotation
                lookRot = Quaternion.LookRotation(mainCamera.transform.position - pickupRB.position);
                lookRot = Quaternion.Slerp(mainCamera.transform.rotation, lookRot, rotationSpeed * Time.fixedUnscaledDeltaTime);
                pickupRB.MoveRotation(lookRot);
                if (lookObject == currentlyPickedUpObject)
                {
                    pickupRB.constraints = RigidbodyConstraints.FreezeAll;
                } else
                {
                    pickupRB.constraints = RigidbodyConstraints.None;
                }
            }
            if (currentlyPickedUpObject != null)
            {
                if (currentlyPickedUpObject.GetComponent<ActivationCube>() == null)
                {
                    currentlyPickedUpObject = null;
                }
            }
        }
        //Release the object
        public void BreakConnection()
        {
            if (pickupRB != null)
            {
                pickupRB.constraints = RigidbodyConstraints.None;
            }
            currentlyPickedUpObject = null;
            physicsObject.pickedUp = false;
            currentDist = 0;
        }

        public void PickUpObject()
        {
            AudioHandler.PlaySoundEffect("Picked Up");
            physicsObject = lookObject.GetComponent<IHoldable>();
            currentlyPickedUpObject = lookObject;
            pickupRB = currentlyPickedUpObject.GetComponent<Rigidbody>();
            physicsObject.playerInteractions = this;
            StartCoroutine(physicsObject.Hold());
        }
    }
}