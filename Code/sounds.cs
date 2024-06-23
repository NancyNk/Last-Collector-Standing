using UnityEngine;
using System.Collections;

public class CharacterAudio : MonoBehaviour
{
    public AudioClip moveSound;
    public AudioClip speedHitSound;
    public AudioClip jumpHitSound;
    private bool canPlayJumpSound = false;
    public AudioClip coinHitSound;
    public AudioClip finishHitSound;
    public AudioClip coHitSound; 
    public AudioSource audioSource;
    public AudioSource speedHitAudioSource;
    public AudioSource jumpHitAudioSource;
    public AudioSource coinHitAudioSource;
    public AudioSource finishHitAudioSource;
    public AudioSource coHitAudioSource; 

    public AudioClip newJumpSound; 
    public AudioSource newJumpAudioSource;

    public AudioClip background;
    public AudioSource backgroundSource;


    private Rigidbody2D rb;
    private float dirX;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (canPlayJumpSound && Input.GetKeyDown(KeyCode.Space))
        {
            PlayJumpHitSound(); 
            canPlayJumpSound = false; 
        }
        else if (Input.GetButtonDown("Jump") && rb.velocity.y == 0)
        {
            PlayNewJumpSound();

        }

        dirX = Input.GetAxisRaw("Horizontal");

        if (dirX != 0 && rb.velocity.y == 0)
        {
            PlayMoveSound();
        }
    }

    private void PlayMoveSound()
    {
        if (audioSource != null && moveSound != null)
        {
            // Check if the audio source is not playing
            if (!audioSource.isPlaying)
            {
                audioSource.clip = moveSound;
                audioSource.pitch = 1f; // Set pitch back to normal
                audioSource.Play();
            }
        }
    }

    private IEnumerator SpeedBoostSoundCoroutine()
    {
        if (speedHitAudioSource != null && speedHitSound != null)
        {
            // Double the pitch temporarily
            speedHitAudioSource.pitch *= 2;

            // Play the speed hit sound
            speedHitAudioSource.clip = speedHitSound;
            speedHitAudioSource.Play();

            // Wait for 3 seconds
            yield return new WaitForSeconds(3);

            // Revert the pitch back to normal
            speedHitAudioSource.pitch /= 2;
        }
    }

    private void PlaySpeedHitSound()
    {
        StartCoroutine(SpeedBoostSoundCoroutine());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("speed"))
        {
            PlaySpeedHitSound();
        }
        else if (collision.gameObject.CompareTag("jump"))
        {
            canPlayJumpSound = true;
        }
        else if (collision.gameObject.CompareTag("coin"))
        {
            PlayCoinHitSound();
        }
        
        else if (collision.gameObject.CompareTag("co"))
        {
            PlayCoHitSound();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "finish")
        {
            PlayFinishHitSound();
        }
    }

    private void PlayJumpHitSound()
    {
        if (jumpHitAudioSource != null && jumpHitSound != null)
        {
            jumpHitAudioSource.clip = jumpHitSound;
            jumpHitAudioSource.Play();
        }
    }

    private void PlayCoinHitSound()
    {
        if (coinHitAudioSource != null && coinHitSound != null)
        {
            coinHitAudioSource.clip = coinHitSound;
            coinHitAudioSource.Play();
        }
    }

    private void PlayFinishHitSound()
    {
        if (finishHitAudioSource != null && finishHitSound != null)
        {
            finishHitAudioSource.clip = finishHitSound;
            finishHitAudioSource.Play();
        }
    }

    private void PlayCoHitSound()
    {
        if (coHitAudioSource != null && coHitSound != null)
        {
            coHitAudioSource.clip = coHitSound;
            coHitAudioSource.Play();
        }
    }
    private void PlayNewJumpSound()
    {
        if (newJumpAudioSource != null && newJumpSound != null)
        {
            newJumpAudioSource.clip = newJumpSound;
            newJumpAudioSource.Play();
        }
    }

}
