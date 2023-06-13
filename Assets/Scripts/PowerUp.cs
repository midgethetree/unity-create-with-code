using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType { None, Pushback, Rockets, Smash }

public class PowerUp : MonoBehaviour
{
    private Rigidbody powerupRb;
    [SerializeField] private Vector3 rotationVelocity = new Vector3(0.0f, 45.0f, 0.0f);
    public PowerUpType powerUpType;

    void Awake()
    {
        powerupRb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(rotationVelocity * Time.fixedDeltaTime);
        powerupRb.MoveRotation(powerupRb.rotation * deltaRotation);
    }
}
