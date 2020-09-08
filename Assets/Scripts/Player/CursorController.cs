using InteractiveObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField] private KeyCode interactKey;
        [SerializeField] private float interactDistance = 7f;
        [SerializeField] private float grabbableCheck = 5f;
        [SerializeField] private float throwForce = 10;

        [SerializeField] private Transform cursor;
        [SerializeField] private float resizeCursor;
        [SerializeField] private Color resizeColor;
        [SerializeField] private Color defaultColor;

        [SerializeField] private Transform holdingPlace;
        [SerializeField] private CharacterController character;

        [SerializeField] private Camera cam;

        private bool _grabbedThisFrame = false;
        private bool _isGrabbed = false;
        private Rigidbody _grabbedObj;

        void Start()
        {
            cam = Camera.main;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            _grabbedThisFrame = false;

            //to remove any forces from our object while holding it
            if (_isGrabbed)
            {
                StopItem();
                //grabbedObj.MovePosition(holdingPlace.position);
                //grabbedObj.MoveRotation(holdingPlace.rotation);
                if (Vector3.Distance(_grabbedObj.position, transform.position) > interactDistance)
                {
                    DropItem();
                }

                if (Input.GetMouseButton(0))
                {
                    Debug.Log("throw");
                    DropItem();
                    _grabbedObj.AddForce(transform.forward * throwForce, ForceMode.Impulse);
                }
            }

            CheckIfInteractable();

            if (_isGrabbed && Input.GetKeyDown(interactKey) && !_grabbedThisFrame)
            {
                DropItem();
            }
        }

        private void FixedUpdate()
        {
            if (_isGrabbed)
            {
                _grabbedObj.MovePosition(Vector3.Lerp(_grabbedObj.transform.position, holdingPlace.position, Time.deltaTime * 25));
                //grabbedObj.transform.position = (Vector3.Lerp(grabbedObj.transform.position,holdingPlace.position,Time.deltaTime * 20));
                _grabbedObj.MoveRotation(Quaternion.Lerp(_grabbedObj.rotation,holdingPlace.rotation, Time.deltaTime * 50));
            }
        }

        private void CheckIfInteractable()
        {
            RaycastHit hitInfo;

            //Will check if we looking at some object in interactDistance
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, interactDistance))
            {
                //Every object with we can interact with need to have InteractiveObj component
                InteractiveObj interactive = hitInfo.collider.gameObject.GetComponent<InteractiveObj>();

                if (interactive != null && Vector3.SqrMagnitude(interactive.transform.position - transform.position) <= grabbableCheck)
                {
                    //Show to the player that he is looking at an interactive object
                    if (cursor != null) EnlargeCursor();

                    //Check if we can grab it
                    if (interactive.IsGrabbable)
                    {

                        if (!_isGrabbed && Input.GetKeyDown(interactKey))
                        {
                            PickUpItem(hitInfo.collider.gameObject.GetComponent<Rigidbody>());
                            _grabbedThisFrame = true;
                        }
                    
                    }

                    if (Input.GetKeyDown(interactKey)) interactive.Interact();

                }
                else if (_isGrabbed)
                {
                    //If we can't see the grabbable object(and we see another interactive object) but we still hold it we need to drop it
                    //DropItem();
                }
                else
                {
                    CursorDefault();
                }

            }
            else if (_isGrabbed)
            {
                //If we can't see the grabbable object(and we see the non-interactive object) but we still hold it we need to drop it
                //DropItem();
            }
            else
            {
                //If we don't see any interactive objects 
                if (cursor != null) CursorDefault();
            }

        }

        private void EnlargeCursor()
        {
            cursor.localScale = new Vector3(resizeCursor + 1f, resizeCursor + 1f);
            cursor.GetComponent<Image>().color = resizeColor;
        }
    
        private void CursorDefault()
        {
            cursor.localScale = Vector3.one;
            cursor.GetComponent<Image>().color = defaultColor;
        }
    
        private void PickUpItem(Rigidbody rb)
        {
            //Check if the game object has Rigidbody
            if (rb != null)
            {

                _isGrabbed = true;
                _grabbedObj = rb;
                StopItem();
                _grabbedObj.useGravity = false; // to we can hold it
                //grabbedObj.mass = 100000f; // So it would win in colisions.
                //grabbedObj.transform.SetParent(transform); // to follow out position

            }
        }
        public void DropItem()
        {
            _isGrabbed = false;
            if (_grabbedObj != null)
            {

                StopItem();
                //Set all vars back to normal
                //grabbedObj.mass = 1;
                _grabbedObj.useGravity = true;
                //grabbedObj.transform.parent = null; ;
                //grabbedObj = null;
            }
        }
        private void StopItem()
        {
            if (_grabbedObj != null)
            {
                _grabbedObj.velocity = Vector3.zero;
                _grabbedObj.angularVelocity = Vector3.zero;
            }
        }
    }
}
