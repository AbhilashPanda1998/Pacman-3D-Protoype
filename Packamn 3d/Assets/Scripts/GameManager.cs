using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables
    private float m_MaxHealth;
    [SerializeField]
    private CoinSpawnner m_CoinSpawnner;
    [SerializeField]
    private EnemySpawnner m_EnemySpawnner;
    [SerializeField]
    private Text m_CurrentHealthUI;
    [SerializeField]
    private Text m_CoinCountUI;
    [SerializeField]
    private Text m_RestartingUI;
    private float m_TotalCoins;
    [SerializeField]
    private GameObject m_KillCapsule;
    #endregion

    #region UnityFunctions
    private void OnEnable()
    {
        m_MaxHealth = FindObjectOfType<PlayerController>().PlayerHealth;
        Enemy.SendMessage += Updatehealth;
        Coin.SendCoinCount += UpdateCoinCount;
        PlayerController.Restart += RestartScene;
        m_TotalCoins = 0;
    }

    private void Start()
    {
        SpawnKillCapsule();
    }

    private void OnDisable()
    {
        Enemy.SendMessage -= Updatehealth;
        Coin.SendCoinCount -= UpdateCoinCount;
        PlayerController.Restart -= RestartScene;
    }

    private void Update()
    {
        if(m_TotalCoins>=100)             //If player collects 100 coins, he wins.
        {
            m_RestartingUI.text = "YOU WON....";
            Time.timeScale = 0;
        }

        if(m_CoinSpawnner.CoinCount.Count ==0)             //Coin, Enemy and KillCapsule spawns again as soon as one set of Coin Count becomes zero
        {
            m_CoinSpawnner.SpawnCoin();
            m_EnemySpawnner.SpawnEnemy();
            SpawnKillCapsule();
        }
    }
    #endregion

    #region ClassFunctions
    private void Updatehealth(float playerUpdatedHealth)
    {
        m_CurrentHealthUI.text = playerUpdatedHealth.ToString();
    }
    
    private void UpdateCoinCount(float count)
    {
        m_CoinSpawnner.CoinCount.Remove(m_CoinSpawnner.CoinCount[m_CoinSpawnner.CoinCount.Count - 1].gameObject);
        m_TotalCoins += count;
        m_CoinCountUI.text = m_TotalCoins.ToString();
    }

    private void RestartScene(string Restart)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Restart()
    {
        RestartScene("Restart");
    }

    private void SpawnKillCapsule()
    {
        GameObject capsule = Instantiate(m_KillCapsule, m_CoinSpawnner.CoinCount[Random.Range(0, m_CoinSpawnner.CoinCount.Count)].transform.position, Quaternion.identity);
    }
    #endregion
}
