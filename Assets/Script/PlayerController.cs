using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
[RequireComponent (typeof(GroundCheck))]
public class PlayerController : MonoBehaviour
{
    // Component reference
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private GroundCheck gndChk;

    // Movement Variables
    [Range(3, 10)] // Add range on speed
    public float speed = 5.0f;
    public float jumpForce = 10.0f;

    public bool isGrounded = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gndChk = GetComponent<GroundCheck>();

        // Reset jumpForce to 5.0f, if jumpForce is less then 0
        if (jumpForce < 0) jumpForce = 5.0f;

        //sr.flipX = true;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        CheckIsGround();

        // Get button input to controll left right
        float hInput = Input.GetAxis("Horizontal");

        if(curPlayingClips.Length > 0)
        {
            if (!(curPlayingClips[0].clip.name == "Fire"))
            {
                // Add speed
                rb.linearVelocity = new Vector2(hInput * speed, rb.linearVelocity.y);

                // Add fight "q Button"
                if (Input.GetButtonDown("Fire1"))
                {
                    anim.SetTrigger("fire");
                } 
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }
        }

        // Add speed
        rb.linearVelocity = new Vector2(hInput * speed, rb.linearVelocity.y);

        // Add Jump "Space Button"
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetBool("isGround", isGrounded);
        }

        //if (Input.GetButtonUp("Fire1"))
        //    anim.SetBool("fight", false);

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
        //else
        //{
        //    sr.flipX = true;
        //}

        //anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("speed", Mathf.Abs(hInput));
    }

    void CheckIsGround()
    {
        if (!isGrounded)
        {
            if (rb.linearVelocity.y <= 0) isGrounded = gndChk.isGrounded();
        }
        else
        {
            isGrounded = gndChk.isGrounded();
            anim.SetBool("isGround", isGrounded);
        }

    }
}
