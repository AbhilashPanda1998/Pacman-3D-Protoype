using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private float m_Health;
    [SerializeField]
    private float m_Speed;
    private Vector3 m_Direction;
    private Rigidbody m_RigidBody;
    public static event Action<string> Restart;
    public static event Action<string> KillCapsule;
    #endregion

    #region Properties
    public float PlayerHealth
    {
        get { return m_Health; }
    }
    #endregion

    #region UnityFunctions
    private void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    private void Update()    //Pacman type input
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            m_Direction = Vector3.left;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            m_Direction = Vector3.right;
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            m_Direction = -Vector3.forward;
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            m_Direction = Vector3.forward;
        }
        m_RigidBody.velocity = m_Direction * m_Speed;
        transform.forward = m_Direction;
    }

    private void OnTriggerEnter(Collider other)    //If finds capsule then activates it and destroys the cpasule.
    {
        if(other.gameObject.tag == "KillCapsule")
        {
            if(KillCapsule!=null)
            {
                KillCapsule("CapsuleActivated");
            }
            Destroy(other.gameObject);
        }
    }
    #endregion

    #region ClassFunctions
    public void TakeDamage(float damage)
    {
        m_Health -= damage;
        if(m_Health<=0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (Restart != null)
        {
            Restart("Restart");
        }
    }
    #endregion
}