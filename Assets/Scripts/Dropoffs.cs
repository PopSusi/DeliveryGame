using UnityEngine;

public class Dropoffs : Interactables
{
    [SerializeField]
    public Streets street;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // So we can see in editor, we just hide it once the game starts
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

    //When used by the MissionManager we turn on visibility
    public void Activate()
    {
        GetComponentInChildren<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        //Debug.Log("Activating");
    }
}
