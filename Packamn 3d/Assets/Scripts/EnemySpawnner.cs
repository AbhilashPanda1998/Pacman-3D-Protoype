using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnner : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private int n_NoOfEnemy;
    [SerializeField]
    private GameObject m_Enemy;
    [SerializeField]
    private List<GameObject> SpawnPos = new List<GameObject>();
    #endregion

    #region UnityFunction
    private void Start()
    {
        SpawnEnemy();
    }
    #endregion

    #region ClassFunction
    public void SpawnEnemy()                     //Spawn Enemy
    {
        for (int i = 0; i < n_NoOfEnemy; i++)
        {
            GameObject enemy = Instantiate(m_Enemy, SpawnPos[Random.Range(0, SpawnPos.Count)].transform.position, Quaternion.identity);
        }
    }
    #endregion
}
