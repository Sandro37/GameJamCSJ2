using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [Header("CheckGround")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform checkGroundLeft;
    [SerializeField] private Transform checkGroundRight;
    [SerializeField] private float radiusGround;

    private bool isCheckGroundLeft = false;
    private bool isCheckGroundRight = false;
    private bool isGround = false;

    [Header("Movement")]
    [SerializeField] private float speedX;
    private float horizontal;
    
    [Header("Jump")]
    [SerializeField] private float jumpForce;
    private bool canJump = false;

    [Header("Wall jump")]
    [SerializeField] private LayerMask wallMask;
    [SerializeField] private Transform checkWallPosition;
    [SerializeField] private float radiusWall;
    [Range(-1f, 0f)]
    [SerializeField] private float maxFallSpeed = -1;
    [SerializeField] private float jumpForceHorizontal = 6f;
    [SerializeField] private float jumpFinish;
    [Range(0.25f,1f)]
    [SerializeField] private float wallJumpDuration = 0.25f;
    private bool isCheckWallPosition = false;
    private bool isWall = false;
    private bool jumpFromWall = false;
    private bool canWallJump = false;
    public bool IsRunning { get; private set; }
    public bool IsWall
    {
        private set => isWall = value;
        get => isWall;
    }
    public bool IsGround
    {
        private set => isGround = value;
        get => isGround;
    }
    public bool IsInteracting { get; set; }
    public bool CanMove { get; set; }
    private Rigidbody2D rig;
    private int direction = 1;
    private SpriteRenderer sprite;
    
    // Start is called before the first frame update
    void Start()
    {
        GameController.instance.lastCheckPoint = transform.position;
        rig = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(CanMove);
        GetInputs();
        CheckGround();
        CheckWall();
        Rotate();
    }
    private void FixedUpdate()
    {
        SlipWall();
        Movement();
        Jump();
    }
    private void GetInputs()
    {
        if (IsInteracting) return;
        horizontal = Input.GetAxisRaw("Horizontal");
 
        IsRunning = horizontal != 0;
        if (Input.GetButtonDown("Jump") && IsGround)
            canJump = true;

        if(Input.GetButtonDown("Jump") && IsWall && !isGround)
        {
            canWallJump = true;
        }
        if (jumpFromWall)
        {
            if(Time.time > jumpFinish)
            {
                jumpFromWall = false;
            }
        }
        if(!jumpFromWall && !CanMove && sprite.enabled)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || IsGround)
                CanMove = true;
        }
    }
    private void Movement()
    {
        if (!CanMove || IsInteracting) return;


        float x = horizontal * speedX * Time.fixedDeltaTime;
        rig.velocity = new Vector2(x, rig.velocity.y);

        if (x * direction < 0f)
            Flip();
    }
    private void Rotate()
    {
        if (direction > 0)
            rig.transform.eulerAngles = new Vector2(0, 0);
        else if (direction < 0)
            rig.transform.eulerAngles = new Vector2(0, 180);
    }
    private void Jump()
    {
        if (canWallJump)
        {
            CanMove = false;
            jumpFinish = Time.time + wallJumpDuration;
            canWallJump = false;
            jumpFromWall = true;
            Flip();

            rig.velocity = Vector2.zero;
            Debug.Log(jumpForceHorizontal * direction);
            AudioController.instace.PlaySoundSFX(0);
            rig.AddForce(new Vector2(jumpForceHorizontal * direction, jumpForce), ForceMode2D.Impulse);
        }
        else if (canJump)
        {
            rig.velocity = Vector2.zero;
            AudioController.instace.PlaySoundSFX(0);
            rig.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            canJump = false;
        }
    }
    private void SlipWall()
    {
        if (IsWall)
        {
            if (rig.velocity.y < maxFallSpeed)
            {
                rig.velocity = new Vector2(rig.velocity.x, maxFallSpeed);
            }
        }
    }
    private void Flip()
    {
        direction *= -1;
    }
    #region physhicsCheck
    private void CheckGround()
    {
        isCheckGroundLeft = Physics2D.Raycast(checkGroundLeft.position, Vector2.down, radiusGround, groundMask);
        isCheckGroundRight = Physics2D.Raycast(checkGroundRight.position, Vector2.down, radiusGround, groundMask);

        Debug.DrawRay(checkGroundLeft.position , Vector2.down * radiusGround, Color.red);
        Debug.DrawRay(checkGroundRight.position , Vector2.down * radiusGround, Color.red);

        if (isCheckGroundLeft || isCheckGroundRight)
            IsGround = true;
        else
            IsGround = false;
    }

    private void CheckWall()
    {
        isCheckWallPosition = Physics2D.OverlapCircle(checkWallPosition.position, radiusWall, wallMask);

        if (isCheckWallPosition)
            IsWall = true;
        else
            IsWall = false;
    }
    #endregion
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkWallPosition.position, radiusWall);
    }

    public void SoundMovement()
    {
        if (AudioController.instace.sonsPassos.Length > 0)
            AudioController.instace.TocarAudio(AudioController.instace.sonsPassos[Random.Range(0, AudioController.instace.sonsPassos.Length)], 1f);
    }

}
