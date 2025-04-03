using UnityEngine;

public class PickUps : Interactables
{
    [SerializeField]
    bool pickedup;
    [SerializeField]
    public GameObject pickupSocket;
    [SerializeField]
    public Collider pickupCollider;
    [SerializeField]
    public Collider physicalCollider;
    [SerializeField]
    int pickupIdentity;

    public MissionInfoTotal missionData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // -- When grabbed, we want the rigidbody to be off so we can carry it (This also means collision is handled in OnTriggerEnter) and we dont want to see the UI Prompt
    public override GameObject Interact()
    {
        pickedup = true;
        if(gameObject.TryGetComponent(out Rigidbody existentRb))   Destroy(existentRb);
        pickupCollider.enabled = false;
        imageMesh.enabled = false;
        return gameObject;
    }

    // -- Called when it's dropped onto the ground. We want it to interact with the floor and be returned to the pickup layer so we can re-pick it up
    public void ResetVars()
    {
        if (gameObject.GetComponent<Rigidbody>() == null) gameObject.AddComponent<Rigidbody>();
        pickedup = false;
        pickupCollider.enabled = true;
        gameObject.layer = 6;
    }

    // -- If its on the truck (pickedup) and we hit something, we wanna drop it. This calls the Break event which drops itself and items above it in the stack (this function is in the InteractControls script)
    private void OnTriggerEnter(Collider other)
    {
        if (pickedup)
        {
            Break(gameObject);
        }
        else
        {
            //Turn on pickup prompt if it's near
            if(other.gameObject == player)
            {
                imageMesh.enabled = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Turn off pickup prompt if it's away
        if (other.gameObject == player)
        {
            imageMesh.enabled = false;
        }
    }
}
