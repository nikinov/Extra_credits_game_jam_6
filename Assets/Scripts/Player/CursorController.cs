using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    [SerializeField] private KeyCode interactKey;
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private float throwForce = 100;

    [SerializeField] private Transform cursor;
    [SerializeField] private float resizeCursor;
    [SerializeField] private Color resizeColor;
    [SerializeField] private Color defaultColor;

    [SerializeField] private Transform holdingPlace;
    [SerializeField] private CharacterController character;

    private bool isGrabbed = false;
    private Rigidbody grabbedObj;

    void Update()
    {

        //to remove any forces from our object while holding it
        if (isGrabbed)
        {
            StopItem();
            //grabbedObj.MovePosition(holdingPlace.position);
            //grabbedObj.MoveRotation(holdingPlace.rotation);
            if (Vector3.Distance(grabbedObj.position, transform.position) > interactDistance)
            {
                DropItem();
            }

            if (Input.GetMouseButton(0))
            {
                Debug.Log("throw");
                DropItem();
                grabbedObj.AddForce(transform.forward * throwForce);
            }
        }


    }

    private void FixedUpdate()
    {
        CheckIfInteractable();
        if (isGrabbed)
        {
            grabbedObj.MovePosition(Vector3.Lerp(grabbedObj.transform.position, holdingPlace.position, Time.deltaTime * 25));
            //grabbedObj.transform.position = (Vector3.Lerp(grabbedObj.transform.position,holdingPlace.position,Time.deltaTime * 20));
            grabbedObj.MoveRotation(Quaternion.Lerp(grabbedObj.rotation,holdingPlace.rotation, Time.deltaTime * 50));
        }
    }

    private void CheckIfInteractable()
    {

        RaycastHit hitInfo;

        //Will check if we looking at some object in interactDistance
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, interactDistance))
        {
            //Every object with we can interact with need to have InteractiveObj component
            InteractiveObj interactive = hitInfo.collider.gameObject.GetComponent<InteractiveObj>();

            if (interactive != null)
            {
                //Show to the player that he is looking at an interactive object
                if (cursor != null) EnlargeCursor();

                //Check if we can grab it
                if (interactive.isGrabbable)
                {

                    if (!isGrabbed && Input.GetKeyDown(interactKey))
                    {
                        PickUpItem(hitInfo.collider.gameObject.GetComponent<Rigidbody>());
                    }
                    else if (isGrabbed && Input.GetKeyDown(interactKey))
                    {
                        DropItem();
                    }
                }

                if (Input.GetKeyDown(interactKey)) interactive.Interac();

            }
            else if (isGrabbed)
            {
                //If we can't see the grabbable object(and we see another interactive object) but we still hold it we need to drop it
                //DropItem();
            }
            else
            {
                CursorDefault();
            }

        }
        else if (isGrabbed)
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

            isGrabbed = true;
            grabbedObj = rb;
            StopItem();
            grabbedObj.useGravity = false; // to we can hold it
            //grabbedObj.mass = 100000f; // So it would win in colisions.
            //grabbedObj.transform.SetParent(transform); // to follow out position

        }
    }
    public void DropItem()
    {
        isGrabbed = false;
        if (grabbedObj != null)
        {

            StopItem();
            //Set all vars back to normal
            //grabbedObj.mass = 1;
            grabbedObj.useGravity = true;
            //grabbedObj.transform.parent = null; ;
            //grabbedObj = null;
        }
    }
    private void StopItem()
    {
        if (grabbedObj != null)
        {
            grabbedObj.velocity = Vector3.zero;
            grabbedObj.angularVelocity = Vector3.zero;
        }
    }
}
