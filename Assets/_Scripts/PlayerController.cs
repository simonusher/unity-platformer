using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(HealthManager))]
public class PlayerController: MonoBehaviour
{
    [SerializeField] private float movementForce = 10.0f;
    [SerializeField] private float jumpForce = 1000.0f;
    [SerializeField] private float maxVelocityX = 10.0f;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private float blinkProbability = 0.1f;
    [SerializeField] private Color untouchableColor;
    [SerializeField] private Color normalColor;
    [SerializeField] private float jumpTime = 1.0f;
    private HealthManager healthManager;
    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float currentMovementInput;
    private bool grounded = false;
    private bool jump = false;
    private bool hasJumped = false;

    private bool canMove;

    private Vector3 respawnPosition;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        respawnPosition = GetComponent<Transform>().localPosition;
        healthManager = GetComponent<HealthManager>();
        canMove = true;
    }

    private void FixedUpdate()
    {
        handleInput();
    }

    private void Update()
    {
        handleAnimator();
        handleRenderer();
        grounded = Physics2D.Linecast(transform.position, groundCheck.GetComponent<Transform>().position, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonDown("Jump") && grounded && canMove)
        {
            jump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "FallDownCollider")
        {
            respawn();
        }
        else if (collision.tag == "Finish")
        {
            canMove = false;
            GameManager.Instance.loadNextLevel();
        }
    }

    private void handleInput()
    {
        if (canMove)
        {
            //float horizontalInput = Input.GetAxis("Horizontal");
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            currentMovementInput = horizontalInput;
        } else
        {
            currentMovementInput = 0.0f;
        }
        handleRigidbody();
    }

    private void handleRenderer()
    {
        spriteRenderer.flipX = currentMovementInput < 0;
        if (healthManager.possibleToHit())
        {
            spriteRenderer.color = normalColor;
        } else
        {
            spriteRenderer.color = untouchableColor;
        }
    }

    private void handleRigidbody()
    {
        var movementVector = new Vector3(currentMovementInput * movementForce, body.velocity.y);
        //body.AddForce(movementVector);
        //capVelocity();
        body.velocity = movementVector;
            
        if (jump)
        {
            //body.AddForce(new Vector3(0.0f, jumpForce, 0.0f));
            body.velocity = new Vector3(body.velocity.x, jumpForce);
            handleJumpGravity();
            jump = false;
            hasJumped = true;
        }
    }

    private IEnumerator handleJumpGravity()
    {
        var oldGravity = body.gravityScale;
        body.gravityScale = 0;
        yield return new WaitForSeconds(jumpTime);
        body.gravityScale = oldGravity;
        body.velocity = new Vector3(body.velocity.x, 0.0f);
    }

    private void capVelocity()
    {
        if (Mathf.Abs(body.velocity.x) > maxVelocityX)
        {
            body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * maxVelocityX, body.velocity.y);
        }
    }

    private void handleAnimator()
    {
        animator.SetBool("isRunning", currentMovementInput != 0.0f);
        if (hasJumped)
        {
            animator.SetTrigger("jumpTrigger");
            hasJumped = false;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("playerIdle"))
        {
            bool shouldBlink = Random.value < blinkProbability;
            if (shouldBlink)
            {
                animator.SetTrigger("blinkTrigger");
            }
        }
    }

    private void respawn()
    {
        GetComponent<Transform>().localPosition = respawnPosition;
    }
}
