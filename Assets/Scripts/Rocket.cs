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

    void Update()
    {
        if (targetLocked && target != null)
        {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.LookAt(target);
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
