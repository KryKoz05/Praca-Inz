using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Controller : MonoBehaviour
{
    
    public Transform check;
    public bool gorundCheck;
    private Vector3 v = Vector3.zero;
    private Rigidbody2D rb;
    public bool jumping = false;
    public LayerMask IsGround;
    const float checkRad = .4f;
    private bool rightTurn = true;
    [SerializeField] private float jumpForce = 400f;
    private float smothEffect = .005f;
    [Header("Events")]
    [Space]

    public UnityEvent landing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (landing == null)
        {
            landing = new UnityEvent();
        }
    }
    
    void FixedUpdate()
    {
        bool bnZ = gorundCheck;
        gorundCheck = false;

        
        Collider2D[] colide = Physics2D.OverlapCircleAll(check.position, checkRad, IsGround);
        for (int i = 0; i < colide.Length; i++)
        {
            if (colide[i].gameObject != gameObject)
            {
                gorundCheck = true;
                if (!bnZ)
                    landing.Invoke();
            }
        }
        
    }

    public void Move(float dir, bool jumping)
    {


        if (gorundCheck || jumping || !gorundCheck && !jumping)
        {



            
            Vector3 targetVelocity = new Vector2(dir * 10f, rb.velocity.y);
            
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref v, smothEffect);

            
            if (dir > 0 && !rightTurn)
            {
                
                Flip();
            }
            
            else if (dir < 0 && rightTurn)
            {
                
                Flip();
            }

            
            if (gorundCheck && jumping)
            {
                
                gorundCheck = false;
                rb.AddForce(new Vector2(0f, jumpForce));
            }
        }
    } 


        private void Flip()
        {
            
            rightTurn = !rightTurn;

            
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    
}
