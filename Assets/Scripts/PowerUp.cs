using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType { None, Pushback, Rockets, Smash }

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float speed = 45.0f;
    public PowerUpType powerUpType;
    
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * speed);
    }
}
