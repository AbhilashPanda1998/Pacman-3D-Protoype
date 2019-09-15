using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    #region Variables
    [SerializeField]
    List<GameObject> m_NearestNode = new List<GameObject>();
    #endregion

    #region ClassFunction
    public GameObject NextWayPoint()                //Every Waypoint has a connecting node to another waypoint
    {
        GameObject nextWayPoint = m_NearestNode[Random.Range(0, m_NearestNode.Count)];
        return nextWayPoint;
    }
    #endregion
}
