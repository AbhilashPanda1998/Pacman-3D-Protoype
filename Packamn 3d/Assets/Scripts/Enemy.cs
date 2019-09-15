using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private float m_DamageAmount;
    [SerializeField]
    private GameObject m_Player;
    private NavMeshAgent m_Agent;
    private GameObject m_nextNode;
    private bool m_AttacKPlayerActivated;
    private bool m_isKillable;
    private WayPoint[] m_Waypoints;
    private Color m_OriginalColor;
    public static event Action<float> SendMessage;
    #endregion

    #region UnityFunctions
    private void OnEnable()
    {
        PlayerController.KillCapsule += CapsuleActivated;
    }

    private void OnDisable()
    {
        PlayerController.KillCapsule -= CapsuleActivated;
    }

    private void Start()
    {
        m_OriginalColor = GetComponent<Renderer>().material.color;
        m_isKillable = true;
        m_Waypoints = FindObjectsOfType<WayPoint>();
        m_AttacKPlayerActivated = false;
        m_Agent = GetComponent<NavMeshAgent>();
        m_Player = FindObjectOfType<PlayerController>().gameObject;
        InvokeRepeating("ActivateChase", 5f, 5f);
    }

    private void Update()     
    {
        if (!m_AttacKPlayerActivated)
        {
            if (m_nextNode)
                m_Agent.destination = m_nextNode.transform.position;   // IF chase is not activated enemy destination is found through waypoints system.
        }
        else
        {
            m_Agent.destination = m_Player.transform.position;   // IF chase is activated enemy destination is player through navmesh system.
        }

        if(!m_isKillable)     
        {
            GetComponent<Renderer>().material.color = Color.blue;   // Enemy turns blue if player is not killable i.e when player takes the capsule;
            StartCoroutine("ResetKillable",4f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            if(m_isKillable)
            {
                pc.TakeDamage(m_DamageAmount);
                if (SendMessage != null)
                {
                    SendMessage(pc.PlayerHealth);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        if (other.GetComponent<WayPoint>())
        {
            WayPoint waypoint = other.GetComponent<WayPoint>();
            m_nextNode = waypoint.NextWayPoint();
        }
    }
    #endregion

    #region ClassFunctions
    private void ActivateChase()
    {
        m_AttacKPlayerActivated = !m_AttacKPlayerActivated;
        m_nextNode = GetClosestWaypoint(m_Waypoints).gameObject;
    }

    WayPoint GetClosestWaypoint(WayPoint[] waypoint)   // After the chase, Enemy finds the nearest waypoint so that it can continue going in waypoint system.
    {
        WayPoint nearest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (WayPoint t in waypoint)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDistance)
            {
                nearest = t;
                minDistance = dist;
            }
        }
        return nearest;
    }

    private void CapsuleActivated(string activated)   //When player gets capsule he is noit killable.
    {
        m_isKillable = false;
    }

    IEnumerator ResetKillable(float delay)     // After some time player is again killable.
    {
        yield return new WaitForSeconds(delay);
        m_isKillable = true;
        GetComponent<Renderer>().material.color = m_OriginalColor;
    }
    #endregion 
}
