using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class playerMovement : MonoBehaviour
{
    [HideInInspector]
    public static int totalDeathCount;
    [HideInInspector]
    public static string activeScene;
    public deathDisplay dd;


    public float speed;
    private float input;
    private Rigidbody2D rb;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    //jump code
    public float jumpForce;

    //better jump code
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public GameObject targetRespawn;

    public GameObject falseSpike;

    public GameObject destroyEffect;

    private Animator anim;

    private int dCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        activeScene = SceneManager.GetActiveScene().name;
        
    }

    
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        //input = Input.GetAxis("Horizontal");
        input = CrossPlatformInputManager.GetAxis("Horizontal");
        rb.velocity = new Vector2(input * speed, rb.velocity.y);
        if(input == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

    }

    void Update()
    {
        if (rb.position.y < -12f)
        {
            if (dCount == 0)
            {
                FindObjectOfType<AudioManager>().Play("Death");
                dCount = 1;
                totalDeathCount++; dd.SavePlayer();
            }
            
            if (SceneManager.GetActiveScene().name == "Level01" || SceneManager.GetActiveScene().name == "Level12")
            {
                
                rb.transform.position = new Vector2(targetRespawn.transform.position.x + Random.Range(-4f, 4f), 5f);
            }
            else
            {
                StartCoroutine(RestartLevel());
            }
        }

        //jump code
        if(CrossPlatformInputManager.GetButtonDown("Jump") && isGrounded == true)
        {
            anim.SetTrigger("takeOff");
            rb.velocity = Vector2.up * jumpForce;
            FindObjectOfType<AudioManager>().Play("Jump");
        }

        if(isGrounded == true)
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }
        
        //better Jump Code
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !CrossPlatformInputManager.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Target"))
        {
            falseSpike.SetActive(true);
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject.GetComponent<SpriteRenderer>()); speed = 0; jumpForce = 0;
            FindObjectOfType<AudioManager>().Play("Death");
            totalDeathCount++; dd.SavePlayer();
            StartCoroutine(Wait());
        }
        if (collision.gameObject.CompareTag("Villian"))
        {
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject.GetComponent<SpriteRenderer>()); speed = 0; jumpForce = 0;
            StartCoroutine(RestartLevel());
            if (dCount == 0)
            {
                FindObjectOfType<AudioManager>().Play("Death");
                totalDeathCount++; dd.SavePlayer();
                dCount = 1;
            }
        }
        if (collision.gameObject.CompareTag("LastPU"))
        {
            rb.transform.position = new Vector2(rb.transform.position.x + 2f, 0f);
            FindObjectOfType<AudioManager>().Play("Death");
            totalDeathCount++; dd.SavePlayer();
        }
       
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Level02");
    }
    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
