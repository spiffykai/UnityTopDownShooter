using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public GameObject deathPanel;
    [SerializeField] public TMP_Text totalCoinsText;
    public float spawnTime = 1.0f;
    public int enemiesSpawned = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(spawnTime);
        SpawnEnemy();
    }

    private void SpawnEnemy() {
        int random = Random.Range(0, 2);
        Vector3 spawnPosition;
        
        if (random == 1) {
            spawnPosition = new Vector3(Random.Range(-10f, 10f), 10, 0);
        }
        else {
            spawnPosition = new Vector3(Random.Range(-10f, 10f), -10, 0);
        }
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, new Quaternion(0, 0, 0, 0));
        enemiesSpawned++;
        newEnemy.GetComponent<EnemyController>().baseSpeed += (enemiesSpawned * .1f);
        StartCoroutine(SpawnTimer());
    }

    public void quitGame()
    {   
        Application.Quit();
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayerDead(int coins)
    {
        deathPanel.SetActive(true);
        totalCoinsText.text = "Coins: " + coins;
    }
}
