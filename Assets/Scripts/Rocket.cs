using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private float speed = 15.0f;
    private float rocketStrength = 15.0f;
    private float duration = 5.0f;
    private bool targetLocked = false;
    private Transform target;
    private Rigidbody rocketRb;

    void Awake()
    {
        rocketRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (targetLocked && target == null)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (targetLocked && target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;

            Vector3 deltaPosition = direction * Time.fixedDeltaTime * speed;
            rocketRb.MovePosition(rocketRb.position + deltaPosition);

            Quaternion newRotation = Quaternion.LookRotation(direction, Vector3.up);
            rocketRb.MoveRotation(newRotation);
        }
    }

    public void Launch(Transform newTarget)
    {
        target = newTarget;
        targetLocked = true;
        Destroy(gameObject, duration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (target != null)
        {
            if (collision.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -collision.contacts[0].normal;
                targetRigidbody.AddForce(away * rocketStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }
}
