using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    private Animator animator;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        if(rigidbody2D == null)
        {
            Debug.LogError("No Rigidbody2D found on the PlayerController");
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No Animator found on the PlayerController");
        }
    }

    public Transform bottom;

    [SerializeField]
    private float speed = 6.5f;
    private float inAirSpeedMultiplier = 0.85f;
    private float jumpPower = 500f;
    private float doubleJumpDecrease = 0.8f;

    private bool grounded = false;
    float groundRadius = 0.15f;
    public LayerMask whatIsGround;

    private bool hasDoubleJumped = false;
    private bool onWall = false;

    private float startScale;

    public SpriteRenderer currentColor;
    public Sprite[] colorSprites;
    public Animator orbAnim;
    public GameManager.ItemColor myColor;

    private void Start()
    {
        startScale = transform.localScale.x;
        SetColor(GameManager.ItemColor.NONE);
    }

    private void FixedUpdate()
    {
        if(!grounded && Physics2D.OverlapCircle(bottom.position, groundRadius, whatIsGround) && rigidbody2D.velocity.y < 0f)
        {
            Land();
        }
        else if(!Physics2D.OverlapCircle(bottom.position, groundRadius, whatIsGround))
        {
            grounded = false;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(grounded)
            {
                Jump();
            }
            else if(!hasDoubleJumped)
            {
                DoubleJump();
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            Explode();
        }

        float dirX = Input.GetAxis("Horizontal");

        if (!onWall)
        {
            rigidbody2D.velocity = new Vector2(dirX * speed * (grounded ? 1 : inAirSpeedMultiplier), rigidbody2D.velocity.y);
        }

        if(grounded && dirX != 0)
        {
            animator.SetBool("Walking", true);
            animator.speed = Mathf.Abs(dirX);
        }
        else
        {
            animator.SetBool("Walking", false);
            animator.speed = 1f;
        }

        if(dirX < 0)
        {
            transform.localScale = new Vector3(-startScale, transform.localScale.y, transform.localScale.z);
        }
        else if(dirX > 0)
        {
            transform.localScale = new Vector3(startScale, transform.localScale.y, transform.localScale.z);
        }
    }

    private void Explode()
    {
        Vector3 startPosition = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 3f);

        foreach(Collider2D hit in colliders)
        {
            if (hit.GetComponent<Rigidbody2D>() && hit.gameObject.tag == "Treasure")
            {
                Debug.Log("Adding explosive force to: " + hit.name);
                hit.GetComponent<Rigidbody2D>().AddExplosionForce(1000f, startPosition, 25f);
            }
        }
    }

    private void Jump()
    {
        animator.SetTrigger("Jump");
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
        rigidbody2D.AddForce(new Vector2(0f, jumpPower));
        grounded = false;
        AudioManager.instance.PlaySFX(AudioManager.AudioSFX.Jump);
    }

    private void DoubleJump()
    {
        animator.SetTrigger("DoubleJump");
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
        rigidbody2D.AddForce(new Vector2(0f, jumpPower * doubleJumpDecrease));
        grounded = false;
        hasDoubleJumped = true;
        AudioManager.instance.PlaySFX(AudioManager.AudioSFX.Jump);
    }

    private void Land()
    {
        animator.SetTrigger("Land");
        grounded = true;
        hasDoubleJumped = false;
        Spawner.instance.SpawnLandEffect(new Vector3(bottom.position.x, bottom.position.y, 0f));
        AudioManager.instance.PlaySFX(AudioManager.AudioSFX.Land);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" && !onWall)

        {
            onWall = true;
            rigidbody2D.velocity = new Vector2(0f, rigidbody2D.velocity.y);
            AudioManager.instance.PlaySFX(AudioManager.AudioSFX.Splat);
        }
        else if (collision.gameObject.tag == "Treasure")
        {
            if (myColor == collision.gameObject.GetComponent<Treasure>().color || myColor == GameManager.ItemColor.MULTI)
            {
                GameManager.instance.treasureGenerator.CashInTreasure(collision.gameObject.GetComponent<Treasure>());
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" && onWall)
        {
            onWall = false;
        }
    }

    public void SetColor(GameManager.ItemColor color)
    {
        if ((int)color < colorSprites.Length)
        {
            if (currentColor.sprite != colorSprites[(int)color])
            {
                currentColor.sprite = colorSprites[(int)color];
                orbAnim.SetTrigger("NewColor");
            }
        }
        else
        {
            currentColor.sprite = null;
        }

        myColor = color;
    }
}
