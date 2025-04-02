using UnityEngine;

public abstract class Interactables : MonoBehaviour
{
    public delegate void BreakOffEvent(GameObject dropoff);
    public static event BreakOffEvent BreakOff;

    protected static Camera cam;
    protected static GameObject player;
    public GameObject image;
    protected MeshRenderer imageMesh;
    public string publicName;

    [SerializeField]
    protected Component[] components;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Awake()
    { 

        player = FindAnyObjectByType<InteractControls>().gameObject;
        imageMesh = image.GetComponent<MeshRenderer>();
        imageMesh.enabled = false;
        cam = FindAnyObjectByType<Camera>();
        image.transform.LookAt(cam.transform);
        image.transform.Rotate(90, 180, 180);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        image.transform.LookAt(cam.transform);
        image.transform.Rotate(90, 180, 180);
    }

    public abstract GameObject Interact();

    public void Break(GameObject passthrough)
    {
        BreakOff(passthrough);
    }

    //public abstract void Activate();
}
