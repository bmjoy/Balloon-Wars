using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private enum MovementState { IDLE, RUNNING, JUMPING, FALLING }

    [SerializeField] private float m_JumpPower = 14f;
    [SerializeField] private float m_SideMovementPower = 7f;
    [SerializeField] private LayerMask jumpableGround;

    private Rigidbody2D m_Rb;
    private BoxCollider2D m_Collider;
    private Animator m_Animator;
    private SpriteRenderer m_Sprite;
    private float m_DirectionX = 0f;

    // Start is called before the first frame update
    private void Start()
    {
        m_Rb = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_Sprite = GetComponent<SpriteRenderer>();
        m_Collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //  if you want a smooth stop and not a hard stop then change it to 
        //  GetAxis instead of GetAxis raw, so it changes the axis gradually.
        m_DirectionX = Input.GetAxisRaw("Horizontal");

        updateMovementState();
        updateAnimationState();
    }

    private void updateMovementState()
    {
        m_Rb.velocity = new Vector2(m_DirectionX * m_SideMovementPower, m_Rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            m_Rb.velocity = new Vector2(m_Rb.velocity.x, m_JumpPower);
        }
    }

    private void updateAnimationState()
    {
        MovementState state;

        if (m_DirectionX > 0f)
        {
            state = MovementState.RUNNING;
            m_Sprite.flipX = false;
        }
        else if (m_DirectionX < 0f)
        {
            state = MovementState.RUNNING;
            m_Sprite.flipX = true;
        }
        else
        {
            state = MovementState.IDLE;
        }

        if (m_Rb.velocity.y > .1f)
        {
            state = MovementState.JUMPING;
        }
        else if (m_Rb.velocity.y < -.1f)
        {
            state = MovementState.FALLING;
        }

        m_Animator.SetInteger("state", (int)state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(m_Collider.bounds.center, m_Collider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
