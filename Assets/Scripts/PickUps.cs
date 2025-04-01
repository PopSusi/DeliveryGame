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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override GameObject Interact()
    {
        pickedup = true;
        if(gameObject.TryGetComponent(out Rigidbody existentRb))   Destroy(existentRb);
        pickupCollider.enabled = false;
        imageMesh.enabled = false;
        return gameObject;
    }

    public void ResetVars()
    {
        pickedup = false;
        pickupCollider.enabled = true;
        gameObject.layer = 6;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (pickedup)
        {
            Break(gameObject);
        }
        else
        {
            if(other.gameObject == player)
            {
                imageMesh.enabled = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            imageMesh.enabled = false;
        }
    }
}
