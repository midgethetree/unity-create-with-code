using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Private Variables
    [SerializeField] private float horsePower = 40000.0f;
    [SerializeField] private float turnSpeed = 15.0f;
    [SerializeField] private GameObject centerOfMass;
    [SerializeField] private TextMeshProUGUI speedometerText;
    [SerializeField] private TextMeshProUGUI rpmText;
    [SerializeField] private List<WheelCollider> allWheels;
    protected float horizontalInput;
    protected float forwardInput;
    protected Rigidbody playerRb;
    [SerializeField] float speed;
    [SerializeField] float rpm;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        playerRb.centerOfMass = centerOfMass.transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();

        if (IsOnGround())
        {
            // We move the vehicle forward
            playerRb.AddRelativeForce(Vector3.forward * forwardInput * horsePower);
            // We turn the vehicle
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

            speed = Mathf.RoundToInt(playerRb.velocity.magnitude * 2.237f);
            rpm = Mathf.Round((speed % 30) * 40);
        }
        else
        {
            speed = 0;
            rpm = 0;
        }

        //print speed
        speedometerText.SetText("Speed: " + speed + " mph");
        //print RPM
        rpmText.SetText("RPM: " + rpm);
    }

    protected virtual void GetInput()
    {
        // This is where we get player input
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
    }

    private bool IsOnGround()
    {
        foreach (WheelCollider wheel in allWheels)
        {
            if (!wheel.isGrounded)
            {
                return false;
            }
        }
        return true;
    }
}
