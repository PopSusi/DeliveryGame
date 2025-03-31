using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractControls : MonoBehaviour
{
    PlayerInput input;
    List<Interactables> ThingsToCheck = new List<Interactables>();
    Stack<Interactables> HeldObjects = new Stack<Interactables>();

    [SerializeField]
    public GameObject initialPickupSocket;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        input = GetComponent<PlayerInput>();
        input.actions["Interact"].performed += Interact;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ObjectUpdate();
    }

    void Interact(InputAction.CallbackContext context)
    {
        float closestDistance = (ThingsToCheck[0].transform.position - transform.position).magnitude;
        Interactables thingToInteractWith = ThingsToCheck[0];
        Debug.Log(thingToInteractWith.gameObject.name);
        if (ThingsToCheck.Count != 1)
        {
            foreach (Interactables thing in ThingsToCheck)
            {
                float testDistance = (thing.transform.position - transform.position).magnitude;
                if (testDistance < closestDistance) closestDistance = testDistance;
                thingToInteractWith = thing;
            }
        }

        GameObject pickedUp = thingToInteractWith.Interact();
        Debug.Log(pickedUp.layer);
        if (pickedUp.layer == 6)
        {;
            ThingsToCheck.Remove(thingToInteractWith);
            HeldObjects.Push(thingToInteractWith);
            //AttachObject(pickedUp);
            pickedUp.layer = 8;
        } else if (pickedUp.layer == 7)
        {
            Debug.Log("Dropoff");
        }
    }

    void AttachObject(GameObject pickUpObj)
    {
        if (HeldObjects.Count != 0)
        {
            pickUpObj.transform.position = HeldObjects.Peek().pickupSocket.transform.position;
            Debug.Log("attaching to " + HeldObjects.Peek().gameObject.name + " | " + pickUpObj.gameObject.name);
        }
        else
        {
            Debug.Log("attaching to initial " + pickUpObj.gameObject.name);
            pickUpObj.transform.position = initialPickupSocket.transform.position;
        }

        pickUpObj.transform.parent = initialPickupSocket.transform;
        HeldObjects.Push(pickUpObj.GetComponent<Interactables>());
    }

    void ObjectUpdate()
    {
        Interactables[] objects = HeldObjects.ToArray();
        objects[0].transform.position = initialPickupSocket.transform.position;
        objects[0].transform.rotation = initialPickupSocket.transform.rotation;
        for (int i = 1; i < objects.Length; i++)
        {
            objects[i].gameObject.transform.position = objects[i-1].pickupSocket.transform.position;
            objects[i].gameObject.transform.rotation = gameObject.transform.rotation;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        ThingsToCheck.Add(other.gameObject.GetComponent<Interactables>());
        Debug.Log("added " + other.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        ThingsToCheck.Remove(other.gameObject.GetComponent<Interactables>());
    }
}
