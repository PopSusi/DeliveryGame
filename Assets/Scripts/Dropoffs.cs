using UnityEngine;

public class Dropoffs : Interactables
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Awake()
    {
        base.Awake();
        GetComponentInChildren<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override GameObject Interact()
    {
        return gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            imageMesh.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            imageMesh.enabled = false;
        }
    }

    public void Activate()
    {
        GetComponentInChildren<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        //Debug.Log("Activating");
    }
}
