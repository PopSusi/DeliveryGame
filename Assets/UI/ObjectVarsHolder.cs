using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ObjectVarsHolder : MonoBehaviour
{

    public UnityEngine.UI.Image image;
    public RectTransform rect;
    public TextMeshProUGUI item;
    public TextMeshProUGUI location;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Awake()
    {
        Wipe();
    }
    public void InitialLoad(string item_, string dropoff)
    {
        //Debug.Log(placeInStack);
        rect = GetComponent<RectTransform>();
        //float Y = -35f * placeInStack;
        //rect.anchoredPosition = new Vector3(-96f, Y, 0f);
        //Debug.Log(new Vector3(-96f, (-35f * placeInStack) + 1f, 0f));
        if (!image.isActiveAndEnabled)
        {
            image.enabled = true;
        }
        item.text = item_;
        location.text = dropoff;
    }

    public void Wipe(){
        item.text = "";
        location.text = "";
        image.enabled = false;
    }
    // Update is called once per frame
    /*
    public void stackUpdate(int newPlaceInStack)
    {
        float Y = -35f * newPlaceInStack;
        //Debug.Log(newPlaceInStack);
        if (newPlaceInStack == -1)
        {
            Destroy(gameObject);
        } else
        {
            rect.position = new Vector3(-96f, Y, 0f);
            Debug.Log(new Vector3(-96f, Y, 0f));
        }
    }
    */
}
