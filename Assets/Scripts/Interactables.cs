using UnityEngine;

public class Interactables : MonoBehaviour
{
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

    public virtual GameObject Interact()
    {
        pickedup = true;
        pickupCollider.enabled = false;
        physicalCollider.enabled = true;
        return gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (pickedup)
        {
            Debug.Log("BREAK!");
        }
    }
}
