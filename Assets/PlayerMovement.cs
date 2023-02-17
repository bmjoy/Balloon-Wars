using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]float m_JumpPower = 14f;
    [SerializeField]float m_SideMovementPower = 7f;

    private Rigidbody2D m_Rb;

    // Start is called before the first frame update
    private void Start()
    {
       m_Rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //  if you want a smooth stop and not a hard stop then change it to 
        //  GetAxis instead of GetAxis raw, so it changes the axis gradually.
        float directionX = Input.GetAxisRaw("Horizontal");
        
        m_Rb.velocity = new Vector2(directionX * m_SideMovementPower, m_Rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
           m_Rb.velocity = new Vector2(m_Rb.velocity.x, m_JumpPower);
        }
    }
}
