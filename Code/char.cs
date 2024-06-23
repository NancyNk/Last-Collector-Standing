using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Character : MonoBehaviour
{
    private Rigidbody2D rb;
    private float dirX;
    private float moveSpeed = 10f;
    private float jumpForce = 1200f;
    public TextMeshProUGUI loseText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    private int score = 0;
    private bool isJumpBoosted = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
            loseText.gameObject.SetActive(false);
            
            winText.gameObject.SetActive(false);

        UpdateScoreText();

    }

    private void Update()
    {

        dirX = Input.GetAxisRaw("Horizontal") * moveSpeed;

        rb.velocity = new Vector2(dirX, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && rb.velocity.y == 0)
        {
            float jumpVelocity = isJumpBoosted ? jumpForce * 1.3f : jumpForce;
            rb.AddForce(Vector2.up * jumpVelocity);
            isJumpBoosted = false;
        }

        if (transform.position.y < -40)
        {
            ShowLoseText();
            StartCoroutine(RestartLevel());
        }
    }

   

    private void ShowLoseText()
    {
        
            loseText.gameObject.SetActive(true);
            loseText.text = "You Lose!";
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("co"))
        {
            ShowLoseText();
            StartCoroutine(RestartLevel());
        }
        else if (collision.gameObject.CompareTag("speed"))
        {
            StartCoroutine(SpeedBoost(collision.gameObject));
        }
        else if (collision.gameObject.CompareTag("jump") && !isJumpBoosted)
        {
            StartCoroutine(JumpBoost(collision.gameObject));
        }
        else if (collision.gameObject.CompareTag("coin"))
        {
            IncreaseScore();
            StartCoroutine(DisableObjectForSeconds(collision.gameObject, 10000));
        }
       
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "finish")
        {
            ShowWinText();
        }
    }
    private void IncreaseScore()
    {
        score++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    IEnumerator SpeedBoost(GameObject speedObject)
    {
        speedObject.SetActive(false);
        moveSpeed *= 2;
        yield return new WaitForSeconds(3);
        moveSpeed /= 2;
        yield return new WaitForSeconds(7);
        speedObject.SetActive(true);
    }

    IEnumerator JumpBoost(GameObject jumpObject)
    {
        jumpObject.SetActive(false);
        isJumpBoosted = true;
        float originalJumpForce = jumpForce;
        jumpForce *= 1.3f;
        yield return new WaitForSeconds(3);
        jumpForce = originalJumpForce;
        isJumpBoosted = false;
        yield return new WaitForSeconds(7);
        jumpObject.SetActive(true);
    }

    private void ShowWinText()
    {
        winText.gameObject.SetActive(true);
        winText.text = "You Win!";
        StartCoroutine(HideWinTextAfterDelay());
    }

    IEnumerator HideWinTextAfterDelay()
    {
        yield return new WaitForSeconds(4);
        winText.gameObject.SetActive(false);
    }


    IEnumerator DisableObjectForSeconds(GameObject obj, float seconds)
    {
        obj.SetActive(false);
        yield return new WaitForSeconds(seconds);
        obj.SetActive(true);
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


   
}
