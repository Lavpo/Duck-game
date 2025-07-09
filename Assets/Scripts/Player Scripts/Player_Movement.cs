using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float PlayerSpeed, JumpPower;
    [SerializeField] private float PlayerHeight;

    private float StandartPlayerSpeed;

    //Provides coyote jump
    [SerializeField] private float CTime;
    private float CTimeCounter;

    [SerializeField] private LayerMask LayerToBeTouched;

    //Provides crouching
    [SerializeField] private BoxCollider2D mainCol;
    [SerializeField] private BoxCollider2D crouchCol;
    [SerializeField] private Object box; //for test

    //Other stuff
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;
    private Transform tbottom;

    private bool isCrouching; //slowers down the speed of the player
    private bool isGrounded;
    private bool check; //is used for one if in crouching method

    void Start()
    {
        StandartPlayerSpeed = PlayerSpeed;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        tbottom = transform.Find("Bottom");
        animator = GetComponent<Animator>();
    }

    float PlayerHeightCounter()
    {
        if (sr != null)
        {
            return tbottom.transform.position.y;
        }
        else return 0;
    }
    void Update()
    {
        PlayerHeight = PlayerHeightCounter();

        //checks, if player has the correct height to change IsJumping state
        if (PlayerHeight > 1)
        {
            animator.SetBool("IsJumping", true);
        }
        else animator.SetBool("IsJumping", false);

        //Resets the scene
        if (Input.GetButtonDown("Reset"))
        {
            SceneManager.LoadScene("Scene");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Vector3 ijefe = new (14, 1.5f, 0);
            Instantiate(box, ijefe, Quaternion.identity);
        }

        //Writes something in a log
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("something");
        }
        //Calls ColorRandomizer
        if (Input.GetKeyDown(KeyCode.L))
        {
            ColorRandomizer();
        }

        DrawGizmos();
        PlayerMove();
        isGrounded = IsGrounded();
        Jumping();
        Crouching();
    }
    void PlayerMove()
    {
        // CONTROLS
        float moveX = Input.GetAxis("Horizontal");
        if (isCrouching) 
        {
            rb.velocity = new Vector2(moveX * PlayerSpeed / 2, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(moveX * PlayerSpeed, rb.velocity.y);
        }
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }
    void Jumping()
    {
        //Makes player just jumping 
        //if (Input.GetKeyDown(KeyCode.Space) && CTimeCounter > 0f)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, JumpPower);
        //}
        //if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        //    CTimeCounter = 0f;
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpPower);
            animator.SetBool("IsJumping", true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("IsJumping", false);
        }


        //in this case instead of using velocity, as an parameter to provide sprite to load, instead 
        //it would be better to use height as an metric by creating a formula.
    }
    void Crouching()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("isDoing", true);
            isCrouching = true;
            mainCol.enabled = false;
            crouchCol.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            if (!IsCeiling())
            {
                animator.SetBool("isDoing", false);
                isCrouching = false;
                mainCol.enabled = true;
                crouchCol.enabled = false;
            }
            check = true;
        }

        //checks if player is under something while crouching and when isn't, disables crouching
        if (!IsCeiling() && isGrounded && check)
        {
            animator.SetBool("isDoing", false);
            isCrouching = false;
            mainCol.enabled = true;
            crouchCol.enabled = false;
            Debug.Log("something over there");
            check = false;
            Debug.Log("script is working");
        }
    }
    void DrawGizmos()
    {
        if (!isGrounded)
        {
            Debug.DrawRay(gameObject.transform.position, Vector2.down, Color.green);
            CTimeCounter -= Time.deltaTime;
        }
        else if (isGrounded)
        {
            Debug.DrawRay(gameObject.transform.position, Vector2.down, Color.red);
            CTimeCounter = CTime;
        }
            
    }
    bool IsCeiling()
    {
        RaycastHit2D hit = Physics2D.BoxCast(gameObject.transform.position, new Vector2(0.5f, 1), 90, Vector2.up, 1f, LayerToBeTouched);
        return hit.collider != null;
    }
    void ColorRandomizer()
    {
        sr = GetComponent<SpriteRenderer>();
        Color randomColor = new(Random.value, Random.value, Random.value, 1.0f);
        sr.color = randomColor;
    }
    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, 1f, LayerToBeTouched);
        return hit.collider != null;
    }

    public void SpeedReducer()
    {
        float speed = 0.5f * StandartPlayerSpeed;
        PlayerSpeed = speed;
    }
    public void SpeedNormaliser()
    {
        PlayerSpeed = StandartPlayerSpeed;
    }
}