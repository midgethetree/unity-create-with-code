using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PowerUpType currentPowerUp = PowerUpType.None;
    private float powerupStrength = 15.0f;
    private float smashDuration = 0.5f;
    private float smashSpeed = 30.0f;
    private float explosionForce = 25.0f;
    private float explosionRadius = 25.0f;
    private bool isSmashing = false;
    private float forwardInput;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private Coroutine powerupCountdown;
    private GameObject rocket;
    private GameManager gameManager;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private GameObject powerupIndicator;
    [SerializeField] private GameObject rocketPrefab;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

            if (!isSmashing)
            {
                forwardInput = Input.GetAxis("Vertical");

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (currentPowerUp == PowerUpType.Rockets)
                    {
                        LaunchRockets();
                    }
                    else if (currentPowerUp == PowerUpType.Smash)
                    {
                        isSmashing = true;
                        StartCoroutine(Smash());
                    }
                }
            }

            if (transform.position.y < -10)
            {
                Destroy(gameObject);
                gameManager.GameOver();
            }
        }
    }

    void FixedUpdate()
    {
        if (!isSmashing)
        {
            playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            currentPowerUp = other.GetComponent<PowerUp>().powerUpType;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);

            if(powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        currentPowerUp = PowerUpType.None;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Pushback)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    private void LaunchRockets()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            rocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
            rocket.GetComponent<Rocket>().Launch(enemy.transform);
        }
    }

    IEnumerator Smash()
    {
        float smashTime = Time.time + smashDuration;
        while (Time.time < smashTime)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, smashSpeed);
            yield return null;
        }

        while (transform.position.y > 0)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, -smashSpeed * 2);
            yield return null;
        }

        var enemies = FindObjectsOfType<Enemy>();
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
            }
        }

        isSmashing = false;
    }
}
