using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private GameObject ConnectedPortal;
    private Material m_Material;
    #endregion

    #region UnityFunctions
    private void Start()
    {
        m_Material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        m_Material.color = Color.Lerp(Color.green, Color.yellow, Mathf.PingPong(Time.time, 1f));    //Portal Blinks
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            other.transform.position = ConnectedPortal.transform.position;       //Player changes position to other portal.
        }
    }
    #endregion
}
