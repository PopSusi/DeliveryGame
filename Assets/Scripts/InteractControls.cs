using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.InputSystem;
using System.Linq;

public class InteractControls : MonoBehaviour
{
    PlayerInput input;
    List<Interactables> ThingsToCheck = new List<Interactables>();
    List<PickUps> HeldObjects = new List<PickUps>();

    [SerializeField]
    public GameObject initialPickupSocket;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        input = GetComponent<PlayerInput>();
        input.actions["Interact"].performed += Interact;
        Interactables.BreakOff += DettachObjectStack;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (HeldObjects.Count > 0)
        {
            ObjectUpdate();
        }

    }

    void Interact(InputAction.CallbackContext context)
    {
        float closestDistance = 10f;
        Interactables thingToInteractWith = null;
        //Debug.Log(thingToInteractWith.gameObject.name);
        if (ThingsToCheck.Count != 1)
        {
            foreach (Interactables thing in ThingsToCheck)
            {
                    Debug.Log(thing.gameObject.name);
                if (!HeldObjects.Contains(thing))
                {
                    float testDistance = (thing.transform.position - transform.position).magnitude;
                    if (testDistance < closestDistance)
                    {
                        closestDistance = testDistance;
                    }
                    thingToInteractWith = thing;
                }
            }
        } else
        {
            thingToInteractWith = ThingsToCheck[0];
        }

        if (thingToInteractWith != null)
        {
            GameObject interactItem = thingToInteractWith.Interact();
            //Debug.Log(interactItem.layer);
            if (interactItem.layer == 6)
            {
                ;
                ThingsToCheck.Remove((PickUps)thingToInteractWith);
                HeldObjects.Add((PickUps)thingToInteractWith);
                //AttachObject(pickedUp);
                interactItem.layer = 8;
            }
            else if (interactItem.layer == 7)
            {
                Debug.Log("Dropoff");
                //Destroy(interactItem);
                DettachTopObject(interactItem);
            }
        }
    }

    void AttachObject(GameObject pickUpObj)
    {
        if (HeldObjects.Count != 0)
        {
            pickUpObj.transform.position = HeldObjects[HeldObjects.Count - 1].pickupSocket.transform.position;
            //Debug.Log("attaching to " + HeldObjects[HeldObjects.Count - 1].gameObject.name + " | " + pickUpObj.gameObject.name);
        }
        else
        {
            //Debug.Log("attaching to initial " + pickUpObj.gameObject.name);
            pickUpObj.transform.position = initialPickupSocket.transform.position;
        }

        pickUpObj.transform.parent = initialPickupSocket.transform;
        HeldObjects.Add(pickUpObj.GetComponent<PickUps>());
    }

    void DettachObjectStack(GameObject dropoff)
    {
        if (HeldObjects.Count > 0)
        {
            //Debug.Log(dropoff.gameObject.name);
            for (int i = HeldObjects.IndexOf(dropoff.GetComponent<PickUps>()); i < HeldObjects.Count; i++)
            {
                GameObject drop = HeldObjects[i].gameObject;
                drop.GetComponent<PickUps>().ResetVars();
                HeldObjects.Remove(drop.GetComponent<PickUps>());
                drop.AddComponent<Rigidbody>();
            }
        }
    }
    public void DettachObjectStack()
    {
        if (HeldObjects.Count > 0)
        {
            for (int i = 0; i < HeldObjects.Count; i++)
            {
                //Debug.Log(HeldObjects[i].gameObject.name);
                GameObject drop = HeldObjects[i].gameObject;
                drop.GetComponent<PickUps>().ResetVars();
                HeldObjects.Remove(drop.GetComponent<PickUps>());
                drop.AddComponent<Rigidbody>();
            }
        }
    }

    public void DettachObjectStack(float force)
    {
        if (HeldObjects.Count > 0)
        {
            for (int i = (int)math.floor(math.remap(8f, 2f, HeldObjects.Count - 1, 0, force)); i < HeldObjects.Count; i++)
            {
                //Debug.Log(HeldObjects[i].gameObject.name);
                GameObject drop = HeldObjects[i].gameObject;
                drop.GetComponent<PickUps>().ResetVars();
                HeldObjects.Remove(drop.GetComponent<PickUps>());
                drop.AddComponent<Rigidbody>();
            }
        }
        Debug.Log(HeldObjects.Count - (int)math.floor(math.remap(2f, 8f, 0, HeldObjects.Count - 1, force)));
    }

    void DettachTopObject(GameObject dropoffPoint)
    {
        if (HeldObjects.Count > 0)
        {
            GameObject drop = HeldObjects[HeldObjects.Count - 1].gameObject;
            HeldObjects.Remove(drop.GetComponent<PickUps>());
            drop.transform.position = dropoffPoint.transform.position;
            drop.transform.rotation = dropoffPoint.transform.rotation;
        }
    }

    void ObjectUpdate()
    {
        PickUps[] objects = HeldObjects.ToArray();
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
