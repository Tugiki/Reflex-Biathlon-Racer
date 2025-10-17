using UnityEngine;

public class CarController : MonoBehaviour
{

    public Rigidbody rb;

    public float forwardAccel = 8f, reverseAccel = 4f, maxSpeed = 50f, turnStrength = 180, gravityForce = 1000f;

    private float speedInput, turnInput;

    private bool grounded;

    public LayerMask whatIsGround;
    public float groundRayLength = 1f;
    public Transform groundRayPoint;

    void Start()
    {
        rb.transform.parent = null; // Ensure the Rigidbody is not a child of this GameObject
    }

    // Update is called once per frame
    void Update()
    {
        speedInput = 0;
        if(Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel * 1000f;
        }
        else if(Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel * 1000f;
        }

        turnInput = Input.GetAxis("Horizontal");


        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime, 0f));


        transform.position = rb.transform.position;
    }

    private void FixedUpdate()
    {
        grounded = false;
        RaycastHit hit;

        if(Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
        {
            grounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        if (!grounded)
        {
            rb.AddForce(-transform.up * gravityForce);
            
            if(transform.rotation.eulerAngles.x > 90f || transform.rotation.eulerAngles.x < -90f)
            {
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
            }

        }


        if (Mathf.Abs(speedInput) > 0)
        {
            rb.AddForce(transform.forward * speedInput);
        }
    }
}
