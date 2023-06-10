using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isOnGround = true;
    private bool canJump = true;
    private float startOffset = 4.0f;
    private float startDelay = 1.0f;
    private Vector3 startingPosition;
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private GameManager gameManager;
    [SerializeField] private float jumpForce = 500.0f;
    [SerializeField] private float doubleJumpForce = 200.0f;
    [SerializeField] private float gravityModifier = 1.5f;
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private ParticleSystem dirtParticle;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crashSound;
    public float speed = 1.0f;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        startingPosition = transform.position;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameManager.isGameActive)
        {
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                float force;
                if (isOnGround)
                {
                    isOnGround = false;
                    force = jumpForce;
                }
                else
                {
                    canJump = false;
                    force = doubleJumpForce;
                }
                playerRb.AddForce(Vector3.up * force, ForceMode.Impulse);
                playerAnim.SetTrigger("Jump_trig");
                dirtParticle.Stop();
                playerAudio.PlayOneShot(jumpSound, 1.0f);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 2.0f;
            }
            else
            {
                speed = 1.0f;
            }
            playerAnim.speed = speed;

            gameManager.UpdateScore(Time.deltaTime * speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            canJump = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle") && gameManager.isGameActive)
        {
            gameManager.GameOver();
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }

    public void StartRunning()
    {
        StartCoroutine(WalkIntoScene());
    }

    IEnumerator WalkIntoScene()
    {
        float walkTime = Time.time + startDelay;

        while (Time.time < walkTime)
        {
            float delayElapsed = Time.time + startDelay - walkTime;

            transform.position = Vector3.Lerp(startingPosition, startingPosition + new Vector3(startOffset, 0, 0), delayElapsed / startDelay);
            playerAnim.SetFloat("Speed_f", 0.25f + (0.75f * delayElapsed / startDelay));

            yield return null;
        }

        transform.position = startingPosition + new Vector3(startOffset, 0, 0);
        playerAnim.SetFloat("Speed_f", 1.0f);

        gameManager.SetGameActive();
    }
}
