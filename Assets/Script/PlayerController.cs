using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    // Component reference
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    // Movement Variables
    [Range(3, 10)] // Add range on speed
    public float speed = 5.0f;
    public float jumpForce = 10.0f;

    //groundCheck Variables
    [Range(0.01f, 0.1f)]
    public float groundCheckRadius = 0.02f;
    public LayerMask isGroundLayer;
    public bool isGrounded = false;

    private Transform groundCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // Reset jumpForce to 5.0f, if jumpForce is less then 0
        if (jumpForce < 0) jumpForce = 5.0f;

        sr.flipX = true;

        // groundCheck Initalization
        GameObject newGameObject = new GameObject();
        newGameObject.transform.SetParent(transform);
        newGameObject.transform.localPosition = Vector3.zero;
        newGameObject.name = "GroundCheck";
        groundCheck = newGameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsGround();

        // Get button input to controll left right
        float hInput = Input.GetAxis("Horizontal");

        // Add speed
        rb.linearVelocity = new Vector2(hInput * speed, rb.linearVelocity.y);

        // Add Jump "Space Button"
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetBool("isGround", isGrounded);

            
        }

        // Add fight "q Button"
        if (Input.GetButtonDown("Fire1"))
            anim.SetBool("fight", true);

        if (Input.GetButtonUp("Fire1"))
            anim.SetBool("fight", false);

        // Add sleep "e Button"
        if (Input.GetButtonDown("Sleep"))
            anim.SetBool("sleep", true);

        if (Input.GetButtonUp("Sleep"))
            anim.SetBool("sleep", false);


        /*if (Input.GetButtonDown("Jump") && Input.GetButtonDown("Happy") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetBool("happy", true);
        }*/

        // Happy
        if (Input.GetButtonDown("Happy"))
            anim.SetBool("happy", true);

        if (Input.GetButtonUp("Happy"))
            anim.SetBool("happy", false);

        // Sprite flipping
        if (hInput != 0)
        {
            sr.flipX = (hInput < 0);
            sr.flipX = !sr.flipX;
        }
        else
        {
            sr.flipX = true;
        }

        //anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("speed", Mathf.Abs(hInput));
    }

    void CheckIsGround()
    {
        if (!isGrounded)
        {
            if (rb.linearVelocity.y <= 0) isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        }
        else
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
            anim.SetBool("isGround", isGrounded);
        }

    }
}
