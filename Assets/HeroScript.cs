using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroScript : MonoBehaviour
{
    public Vector3 facing = Vector3.right;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpSpeed = 30f;
    [SerializeField] private float maxJumpTime = 0.6f;
    [SerializeField] private float gravityScale = 10f;
    
    private Animator animator;
    private Rigidbody2D rb;
    private float jumpTimeRemaining;
    private float horizontal;
    private float vertical;
   
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        
        
        Turn(horizontal);

        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        
        if (!state.IsName("HeroKneel"))
        {
            
            animator.SetFloat("speed", Mathf.Abs(horizontal));
            transform.Translate(horizontal * speed * Time.deltaTime, 0f, 0f);

            
            if (Input.GetButtonDown("Jump") && animator.GetBool("grounded"))
            {
                StartCoroutine(Jump());
            }

            
            if (vertical < 0f && animator.GetBool("grounded"))
            {
                StartCoroutine(Kneel());
            }
        
        }
    }

    private IEnumerator Kneel()
    {
        animator.SetBool("kneel", true);
        while (vertical < 0f)
        {
            yield return null;
        }
        animator.SetBool("kneel", false);
    }

    private IEnumerator Jump()
    {
        rb.gravityScale = 0f;
        jumpTimeRemaining = maxJumpTime;
        animator.SetTrigger("jump");

        while (jumpTimeRemaining > 0 && Input.GetButton("Jump"))
        {
            transform.Translate(0f, jumpSpeed * Time.deltaTime, 0f);
            yield return null;

            jumpTimeRemaining -= Time.deltaTime;
        }

        rb.gravityScale = gravityScale;
    }
    
    private void Turn(float horizontal)
    {
        if (facing == Vector3.right && horizontal < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            facing = Vector3.left;
        }
        else if (facing == Vector3.left && horizontal > 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            facing = Vector3.right;
        }
    }
}
