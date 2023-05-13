using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView m_PhotonView;
    private Rigidbody2D m_RigidBody;
    private BoxCollider2D m_Collider;
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;
    private ConstantForce2D m_ConstantForce;
    private bool m_WasOnGround = false;
    public AirTank PlayerAirTank { get; private set; }
    private bool m_InflatePerformed = false;
    private bool m_DeflatePerformed = false;
    private float m_DirectionX = 0f;
    private enum MovementState { IDLE, RUNNING, JUMPING, FALLING }
    [SerializeField] [Range(1f, 30f)] private float m_JumpForce = 15f;
    [SerializeField] [Range(0.05f, 0.5f)] private float m_jumpTime = 0.1f;
    [SerializeField] [Range(1, 15)] private int m_jumpSmooth = 8;
    [SerializeField] private float m_SideMovementPower = 7f;
    [SerializeField] private LayerMask m_JumpableGround;
    [SerializeField] private float m_InflatingForce = 1f; 
    [SerializeField] private float m_IdleForce = -0.2f;
    [SerializeField] private float m_DeflatingForce = -1f; 
    [SerializeField] private AudioSource m_JumpSoundEffect;
    [SerializeField] private AudioSource m_InflatingSoundEffect;
    [SerializeField] private AudioSource m_DeflatingSoundEffect;
    public float JumpForce
    {
         get{return m_JumpForce;}
         set{m_JumpForce = value;} 
    }

    private void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        PlayerAirTank = FindObjectOfType<AirTank>();
        PlayerAirTank.AirFinished += InflateCancelLogic;
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Collider = GetComponent<BoxCollider2D>();
        m_ConstantForce = GetComponent<ConstantForce2D>();
        m_ConstantForce.relativeForce = new Vector2(0, m_IdleForce);
    }

    private void Update()
    {
        if(m_PhotonView.IsMine)
        {
            addAirToTankIfGrounded();
            updateMovementState();
            updateAnimationState();
        }
    }

    private void addAirToTankIfGrounded()
    {
        if (!m_WasOnGround && isGrounded())
        {
            m_WasOnGround = true;
            PlayerAirTank.StartAddAir();
            DeflateCancelLogic();
        }
        else if (m_WasOnGround && !isGrounded())
        {
            m_WasOnGround = false;
            PlayerAirTank.StopAddAir();
        }
    }

    private void updateMovementState()
    {
        if (m_RigidBody.bodyType != RigidbodyType2D.Static) 
        {
            m_RigidBody.velocity = new Vector2(m_DirectionX * m_SideMovementPower, m_RigidBody.velocity.y);
        }
    }

    private void updateAnimationState()
    {
        MovementState state;

        if (m_DirectionX > 0f)
        {
            state = MovementState.RUNNING;
            m_PhotonView.RPC("SetFlipX", RpcTarget.AllBuffered, false);
        }
        else if (m_DirectionX < 0f)
        {
            state = MovementState.RUNNING;
            m_PhotonView.RPC("SetFlipX", RpcTarget.AllBuffered, true);
        }
        else
        {
            state = MovementState.IDLE;
        }

        if (m_RigidBody.velocity.y > .1f)
        {
            state = MovementState.JUMPING;
        }
        else if (m_RigidBody.velocity.y < -.1f)
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
        if(m_PhotonView.IsMine)
        {
            m_DirectionX = context.ReadValue<float>();
        }
    }

    public void Inflate(InputAction.CallbackContext context)
    {
        if(m_PhotonView.IsMine)
        {
            if (PlayerAirTank.AirAmount != 0)
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
            else
            {
                Debug.Log("No air in the AirTank, cannot inflate");
            }
        }
    }

    private void InflatePerformedLogic()
    {
        // Debug.Log("Inflate performed");
        m_InflatePerformed = true;
        PlayerAirTank.StartReduceAir();
        m_InflatingSoundEffect.Play();
        ResetVerticalVelocity();
        if(m_WasOnGround)
        {
            // Debug.Log("Adding jump boost");
            m_JumpSoundEffect.Play();
            m_RigidBody.AddForce(Vector3.up * m_JumpForce, ForceMode2D.Impulse);
            StartCoroutine(JumpStopCoroutine(m_jumpSmooth, m_jumpTime/(float)m_jumpSmooth, m_JumpForce/(float)m_jumpSmooth));
        }
        m_ConstantForce.relativeForce = new Vector2(0, m_InflatingForce);
    }

    IEnumerator JumpStopCoroutine(int iterations, float WaitSecondsPerIter, float JumpForceReducePerIter) 
    {
        yield return new WaitForSeconds(WaitSecondsPerIter);
        if(iterations > 0 && m_InflatePerformed == true){
            m_RigidBody.AddForce(Vector3.down * JumpForceReducePerIter, ForceMode2D.Impulse);
            StartCoroutine(JumpStopCoroutine(iterations - 1, WaitSecondsPerIter, JumpForceReducePerIter));
        }
    }

    private void InflateCancelLogic()
    {
        // Debug.Log("Inflate canceled");
        m_InflatePerformed = false;
        PlayerAirTank.StopReduceAir();
        m_InflatingSoundEffect.Stop();
        if(!m_DeflatePerformed)
        {
            ResetVerticalVelocity();
            m_ConstantForce.relativeForce = new Vector2(0, m_IdleForce);
        }
    }

    public void Deflate(InputAction.CallbackContext context)
    {
        if(m_PhotonView.IsMine)
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
            else
            {
                Debug.Log("Player grounded, cannot deflate");
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
        if (m_RigidBody.bodyType != RigidbodyType2D.Static) 
        {
            m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, 0);
        }
    }

    [PunRPC]
    void SetFlipX(bool value)
    {
        m_SpriteRenderer.flipX = value;
    }
}
