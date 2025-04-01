using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class CarControls : MonoBehaviour
{
    [SerializeField]
    [Header("Movement")]
    float initialMoveBurst = 40f;

    [SerializeField]
    float moveAccel = 200f;

    [SerializeField]
    float noMoveInputDeccel = 150f;

    [SerializeField]
    float brakeMoveDeccel = 300f;

    [SerializeField]
    float maxMoveAccel = 1000f;

    [SerializeField]
    float maxMoveSpeed = 10f;

    [SerializeField]
    [Header("Turning")]
    float initialTurnBurst = 10f;

    [SerializeField]
    float turnAccel = 10f;

    [SerializeField]
    float noTurnInputDeccel = 61.5f;

    [SerializeField]
    float maxTurnAccel = 6f;

    [SerializeField]
    float maxTurnSpeed = 3f;

    [SerializeField]
    float minTurnForceMod = .3f;

    [SerializeField]
    GameObject Camera;

    [SerializeField]
    Vector3 baseCamOffset = new Vector3(10.44911f, 11.93f, 4.088889f);


    //Script only
    float lastVelocity = 0f;
    PlayerInput input;
    Rigidbody rb;
    InputAction moveAction;
    InputAction brakeAction;
    InteractControls interactControls;
    Vector2 moveVec;
    float speedForce;
    float turningForce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxLinearVelocity = maxMoveSpeed;
        rb.maxAngularVelocity = maxTurnSpeed;
        input = GetComponent<PlayerInput>();
        interactControls = GetComponent<InteractControls>();
        moveAction = input.actions["Move"];
        brakeAction = input.actions["Brake"];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Camera.transform.position = transform.position + baseCamOffset + (transform.forward * -1f * math.remap(-maxMoveSpeed, maxMoveSpeed, -2f, 2f, rb.linearVelocity.magnitude));

        // --- Movement Input --//
        //W moves van forward, S moves backward, is equal to the transform.forward of the van
        moveVec = moveAction.ReadValue<Vector2>();
        Vector3 direction = transform.forward;
        if (moveVec.y > 0 || moveVec.y < 0)
        {
            if (speedForce == 0) speedForce = initialMoveBurst * moveVec.y; //If starting from no speed, to give an impulse
            speedForce = Mathf.Clamp(speedForce + (Time.fixedDeltaTime * moveAccel * moveVec.y), -maxMoveAccel, maxMoveAccel);
            //Debug.Log(speedForce);
        }
        else
        {
            speedForce = Mathf.Clamp(0 - speedForce + (Time.fixedDeltaTime * noMoveInputDeccel), 0, maxMoveAccel);
            //Debug.Log(speedForce + " No Input");
        }

        if (brakeAction.ReadValue<float>() != 0)
        {
            speedForce = Mathf.Clamp(0 - speedForce - (Time.fixedDeltaTime * brakeMoveDeccel * brakeAction.ReadValue<float>()), 0, maxMoveAccel);
        }

        rb.AddForce(direction * speedForce);
        //Debug.Log(brakeAction.ReadValue<float>());
        
        //rb.angularVelocity = new Vector3(moveVec.y, 0f, moveVec.x) * turningForce * Time.fixedDeltaTime;

        if (moveVec.x > 0 || moveVec.x < 0)
        {
            if (turningForce == 0) turningForce = initialTurnBurst * moveVec.x;
            turningForce = Mathf.Clamp(turningForce + turnAccel * moveVec.x, -maxTurnAccel, maxTurnAccel) * (math.remap(2f, maxMoveSpeed, minTurnForceMod, 1f, Mathf.Clamp(rb.linearVelocity.magnitude, 2f, maxMoveSpeed)));
            //Debug.Log(turningForce);
            //Debug.Log("Not zero somehow");
        }
        else
        {
            if (turningForce > 0)
            {
                turningForce = Mathf.Clamp(turningForce - Time.fixedDeltaTime * noTurnInputDeccel, 0f, maxTurnAccel);
            } else
            {
                turningForce = Mathf.Clamp(turningForce + Time.fixedDeltaTime * noTurnInputDeccel, -maxTurnAccel, 0f);
            }
            //Debug.Log(turningForce);
            
        }
        rb.AddTorque(new Vector3(0f, turningForce * 50, 0f));
        //Debug.Log(turningForce);
        //Debug.Log(rb.angularVelocity);

        if(lastVelocity - rb.linearVelocity.magnitude > 2f)
        {
            interactControls.DettachObjectStack(lastVelocity - rb.linearVelocity.magnitude);
            Debug.Log(lastVelocity - rb.linearVelocity.magnitude);
        }
        lastVelocity = rb.linearVelocity.magnitude;
    }

}
