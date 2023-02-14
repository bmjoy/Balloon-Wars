using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]float m_JumpPower = 14f;
    private Rigidbody2D m_PlayerRigidBody;

    // Start is called before the first frame update
    private void Start()
    {
       m_PlayerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
           m_PlayerRigidBody.velocity = new Vector3(0, m_JumpPower, 0);
        }
    }
}
