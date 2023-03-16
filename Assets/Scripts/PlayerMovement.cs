using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D m_Rb;
    private BoxCollider2D m_Collider;
    private Animator m_Animator;
    private SpriteRenderer m_Sprite;
    private ConstantForce2D m_ConstantForce;
    private bool wasOnGround = false;

    private enum MovementState { IDLE, RUNNING, JUMPING, FALLING }

    
    [SerializeField] [Range(1f, 30f)] private float m_JumpPower = 15f;
    [SerializeField] [Range(0.05f, 0.5f)] private float m_jumpTime = 0.1f;
    [SerializeField] [Range(1, 15)] private int m_jumpSmooth = 8;
    [SerializeField] private float m_SideMovementPower = 7f;
    [SerializeField] private LayerMask m_JumpableGround;
    [SerializeField] private float m_InflatingForce = 1f; 
    [SerializeField] private float m_IdleForce = -0.2f;
    [SerializeField] private float m_DeflatingForce = -1f; 
    private float m_DirectionX = 0f;

    [SerializeField] private AudioSource m_JumpSoundEffect;
    [SerializeField] private AudioSource m_InflatingSoundEffect;
    [SerializeField] private AudioSource m_DeflatingSoundEffect;
    [SerializeField] private AirTank airTank;

    private bool m_InflatePerformed = false;
    private bool m_DeflatePerformed = false;

    // Start is called before the first frame update
    private void Start()
    {
        m_Rb = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_Sprite = GetComponent<SpriteRenderer>();
        m_Collider = GetComponent<BoxCollider2D>();
        m_ConstantForce = GetComponent<ConstantForce2D>();
        airTank.AirFinished += InflateCancelLogic;
        m_ConstantForce.relativeForce = new Vector2(0, m_IdleForce);
    }

    // Update is called once per frame
    private void Update()
    {
        addAirToTankIfGrounded();
        updateMovementState();
        updateAnimationState();
    }

    private void addAirToTankIfGrounded()
    {
        if (!wasOnGround && isGrounded())
        {
            wasOnGround = true;
            airTank.StartAddAir();
            DeflateCancelLogic();
        }
        else if (wasOnGround && !isGrounded())
        {
            wasOnGround = false;
            airTank.StopAddAir();
        }
    }

    private void updateMovementState()
    {
        if (m_Rb.bodyType != RigidbodyType2D.Static) 
        {
            m_Rb.velocity = new Vector2(m_DirectionX * m_SideMovementPower, m_Rb.velocity.y);
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
        return Physics2D.BoxCast(m_Collider.bounds.center, m_Collider.bounds.size, 0f, Vector2.down, .1f, m_JumpableGround);
    }

    public void Move(InputAction.CallbackContext context)
    {
        m_DirectionX = context.ReadValue<float>();
    }

    public void Inflate(InputAction.CallbackContext context)
    {
        if (airTank.AirAmount != 0)
        {
            if (context.performed && !m_DeflatePerformed)
            {
                InflatePerformedLogic();
            }
            else if (context.canceled)
            {
                InflateCancelLogic();
            }
        }
    }

    private void InflatePerformedLogic()
    {
        m_InflatePerformed = true;
        airTank.StartReduceAir();
        m_InflatingSoundEffect.Play();
        ResetVerticalVelocity();
        if(wasOnGround)
        {
            m_JumpSoundEffect.Play();
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * m_JumpPower, ForceMode2D.Impulse);
            StartCoroutine(JumpStopCoroutine(m_jumpSmooth, m_jumpTime/(float)m_jumpSmooth, m_JumpPower/(float)m_jumpSmooth));
        }
        m_ConstantForce.relativeForce = new Vector2(0, m_InflatingForce);
    }

    IEnumerator JumpStopCoroutine(int iterations, float WaitSecondsPerIter, float JumpFotceReducePerIter) {
        yield return new WaitForSeconds(WaitSecondsPerIter);
        if(iterations > 0 && m_InflatePerformed == true){
            GetComponent<Rigidbody2D>().AddForce(Vector3.down * JumpFotceReducePerIter, ForceMode2D.Impulse);
            StartCoroutine(JumpStopCoroutine(iterations - 1, WaitSecondsPerIter, JumpFotceReducePerIter));
        }
    }

    private void InflateCancelLogic()
    {
        m_InflatePerformed = false;
        airTank.StopReduceAir();
        m_InflatingSoundEffect.Stop();
        if(!m_DeflatePerformed)
        {
            ResetVerticalVelocity();
            m_ConstantForce.relativeForce = new Vector2(0, m_IdleForce);
        }
    }

    public void Deflate(InputAction.CallbackContext context)
    {
        if (!isGrounded())
        {
            if (context.performed && !m_InflatePerformed)
            {
                DeflatePerformLogic();
            }
            else if (context.canceled)
            {
                DeflateCancelLogic();
            }
        }
    }

    private void DeflatePerformLogic()
    {
        m_DeflatePerformed = true;
        m_DeflatingSoundEffect.Play();
        ResetVerticalVelocity();
        m_ConstantForce.relativeForce = new Vector2(0, m_DeflatingForce);
    }

    private void DeflateCancelLogic()
    {
        m_DeflatePerformed = false;
        m_DeflatingSoundEffect.Stop();
        if (!m_InflatePerformed)
        {
            ResetVerticalVelocity();
            m_ConstantForce.relativeForce = new Vector2(0, m_IdleForce);
        }
    }

    public void ResetVerticalVelocity()
    {
        if (m_Rb.bodyType != RigidbodyType2D.Static) 
        {
            m_Rb.velocity = new Vector2(m_Rb.velocity.x, 0);
        }
    }
}
