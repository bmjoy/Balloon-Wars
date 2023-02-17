using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private enum MovementState { IDLE, RUNNING, JUMPING, FALLING }
    private MovementState m_MovementState = MovementState.IDLE;

    [SerializeField]float m_JumpPower = 14f;
    [SerializeField]float m_SideMovementPower = 7f;

    private Rigidbody2D m_Rb;
    private Animator m_Animator;
    private SpriteRenderer m_Sprite;
    private float m_DirectionX = 0f;

    // Start is called before the first frame update
    private void Start()
    {
       m_Rb = GetComponent<Rigidbody2D>();
       m_Animator = GetComponent<Animator>();
       m_Sprite = GetComponent<SpriteRenderer>();
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

        if (Input.GetButtonDown("Jump"))
        {
           m_Rb.velocity = new Vector2(m_Rb.velocity.x, m_JumpPower);
        }
    }

    private void updateAnimationState()
    {
        if (m_DirectionX > 0f)
        {
            m_Animator.SetBool("running", true);
            m_Sprite.flipX = false;
        }
        else if (m_DirectionX < 0f)
        {
            m_Animator.SetBool("running", true);
            m_Sprite.flipX = true;
        }
        else
        {
            m_Animator.SetBool("running", false);
        }
    }
}
