using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnner : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private List<GameObject> m_CoinsPosition = new List<GameObject>();
    [SerializeField]
    private GameObject m_Coins;
    private List<GameObject> m_CoinCount = new List<GameObject>();
    #endregion

    #region Properties
    public List<GameObject> CoinCount
    {
        get { return m_CoinCount; }
        set { CoinCount = m_CoinCount; }
    }
    #endregion

    #region UnityFunctions
    private void Start()
    {
        SpawnCoin();
    }

    private void Update()
    {
    }
    #endregion

    #region ClassFunction
    public void SpawnCoin()
    {
        for (int i = 0; i < m_CoinsPosition.Count; i++)
        {
            GameObject coin = Instantiate(m_Coins, m_CoinsPosition[i].transform.position, Quaternion.identity);
            m_CoinCount.Add(coin);
        }
    }
    #endregion
}
