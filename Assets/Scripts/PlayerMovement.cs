using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpStrength;
    
    [SerializeField] private Rigidbody2D rb2D;

    [SerializeField] private CapsuleCollider2D cc2D;
    [SerializeField] private BoxCollider2D groundCollider;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask movingGroundLayer;
    [SerializeField] private LayerMask climbingLayer;

    [SerializeField] private Animator animator;

    private float dirX = 0;
    private float dirY = 0;

    [SerializeField] private float fireRate;
    private float nextFire;

    [SerializeField] private float climbingSpeed;

    private float _startingGravitySacale = 0;

    [SerializeField] private float dashSpeed;

    [SerializeField] private bool isDashing = false;

    [SerializeField] private bool canUseDash = true;
    
    [SerializeField] private float dashDuration;
    
    [SerializeField] private float dashCoolDown;

    private float dash = 0;

    private Vector2 startingScale;
    private Vector2 modifiedScale;

    private void Start()
    {
        _startingGravitySacale = rb2D.gravityScale;
        dashSpeed += moveSpeed;

        startingScale = transform.localScale;
        modifiedScale = startingScale;
    }

    private void Update()
    {
        Jump();
        FlipSprite();
        AnimateJump();
        ClimbAnimation();
        FreezeClimbingAnimation();
    }

    private void FixedUpdate()
    {
        Move();
        Dash();
        Climb();
    }

    private void LateUpdate()
    {
        if (startingScale != modifiedScale)
        {
            var playerNewScale = startingScale / modifiedScale;

            if (rb2D.velocity.x != 0)
            {
                playerNewScale.x *= Mathf.Sign(rb2D.velocity.x);
                transform.localScale = playerNewScale;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.ToLower().Contains("moving platform"))
        {
            transform.parent = col.transform;

            modifiedScale = col.transform.localScale;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.name.ToLower().Contains("moving platform"))
        {
            transform.parent = null;

            transform.localScale = startingScale;
            modifiedScale = startingScale;

        }
    }

    private void Move()
    {
        if (isDashing) return;
        
        dirX = Input.GetAxis("Horizontal");
        dirY = Input.GetAxis("Vertical");

        AnimateMovement();
        
        Vector2 velocity = new Vector2(dirX * moveSpeed * Time.fixedDeltaTime, rb2D.velocity.y);

        rb2D.velocity = velocity;
    }

    private void Dash()
    {
        if (!canUseDash) return;
        
        dash = Input.GetAxis("Dash");

        if (isDashing == true) return;
        
        if (dash > 0.5f && isDashing == false)
        {
            Debug.Log("Dash started");

            isDashing = true;
            canUseDash = false;

            rb2D.velocity = Vector2.right * (dirX * dashSpeed * Time.fixedDeltaTime);

            StartCoroutine(ReEnableDash());
        }

    }
    
    private IEnumerator ReEnableDash()
    {
        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        rb2D.velocity = Vector2.zero;
        
        yield return new WaitForSeconds(dashCoolDown);

        canUseDash = true;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && groundCollider.IsTouchingLayers(groundLayer))
        {
            Vector2 velocity = new Vector2(0, jumpStrength);

            rb2D.velocity += velocity;
            return;
        }
    }

    private void AnimateJump()
    {
        if (groundCollider.IsTouchingLayers(groundLayer) == false && groundCollider.IsTouchingLayers(climbingLayer) == false)
        {
            animator.SetBool("isInAir",true);
            return;
        }
        animator.SetBool("isInAir",false);
    }

    private void AnimateMovement()
    {
        if (Mathf.Abs(dirX) > 0 && groundCollider.IsTouchingLayers(groundLayer))
        {
            animator.SetBool("isRunning",true);
            return;
        } 
        animator.SetBool("isRunning",false);
    }

    private void FlipSprite()
    {
        //|-2| = 2, apsolutna vrijednost je vazda pozitivna
        bool isPlayerMoving = Mathf.Abs(rb2D.velocity.x) > Mathf.Epsilon;

        if (isPlayerMoving)
        {
            //Mathf.Sign funkcija ukoliko je vrijednost 0 ili veca od 0 vrace pozitivan broj 1, u suprotnom -1 
            float scaleX = Mathf.Sign(rb2D.velocity.x);

            if (dirX == 0) return;
            
            transform.localScale = new Vector2(scaleX,transform.localScale.y);
        }
    }
    
    private void Climb()
    {

        if (!cc2D.IsTouchingLayers(climbingLayer))
        {
            rb2D.gravityScale = _startingGravitySacale;
            return;
        }

        Vector2 climbVelocity = new Vector2(rb2D.velocity.x, dirY * climbingSpeed);

        rb2D.velocity = climbVelocity;

        rb2D.gravityScale = 0;
    }

    private void ClimbAnimation()
    {
        if (!cc2D.IsTouchingLayers(climbingLayer))
        {
            animator.SetBool("isClimbing", false);
            return;
        }
        animator.SetBool("isClimbing", true);
    }

    private void FreezeClimbingAnimation()
    {
        if (!cc2D.IsTouchingLayers(climbingLayer))
        {
            animator.enabled = true;
            animator.speed = 1;
            return;
        }

        if (cc2D.IsTouchingLayers(climbingLayer) && dirY == 0)
        {
            animator.SetBool("isClimbing", true);
            animator.speed = 0;
            return;
        }
        animator.SetBool("isClimbing", true);
        animator.speed = 1;
    }

}
