using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

  public static GameManager instance = null;

  [SerializeField] GameObject player;
  [SerializeField] GameObject[] spawnPoints;
  [SerializeField] GameObject tanker;
  [SerializeField] GameObject soldier;
  [SerializeField] GameObject ranger;
  [SerializeField] Text levelText;

  private bool gameOver = false;
  private int currentLevel;
  private float generatedSpawnTime = 1f;
  private float currentSpawnTime = 0f;
  private GameObject newEnemy;

  private List<EnemyHealth> enemies = new List<EnemyHealth>();
  private List<EnemyHealth> killedEnemies = new List<EnemyHealth>();

  private void Start()
  {
    currentLevel = 1;
    StartCoroutine(spawn());
  }

  private void Update()
  {
    currentSpawnTime += Time.deltaTime;
  }

  public void RegisterEnemy(EnemyHealth enemy)
  {
    enemies.Add(enemy);
  }

  public void KilledEnemy(EnemyHealth enemy)
  {
    killedEnemies.Add(enemy);
  }

  public bool GameOver
  {
    get { return gameOver; }
  }

  public GameObject Player
  {
    get { return player; }
  }

  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else if (instance != this)
    {
      Destroy(gameObject);
    }

    DontDestroyOnLoad(gameObject);
  }

  public void PlayerHit(int currentHP)
  {
    if (currentHP > 0)
    {
      gameOver = false;
    }
    else
    {
      gameOver = true;
    }
  }

  IEnumerator spawn()
  {
    if (currentSpawnTime > generatedSpawnTime)
    {
      currentSpawnTime = 0;
      if (enemies.Count < currentLevel)
      {
        int randomNumber = Random.Range(0, spawnPoints.Length);
        GameObject spawnLocation = spawnPoints[randomNumber];
        int randomEnemy = Random.Range(0, 3);
        if (randomEnemy == 0)
        {
          newEnemy = Instantiate(soldier) as GameObject;
        }
        else if (randomEnemy == 1)
        {
          newEnemy = Instantiate(ranger) as GameObject;
        }
        else if (randomEnemy == 2)
        {
          newEnemy = Instantiate(tanker) as GameObject;
        }

        newEnemy.transform.position = spawnLocation.transform.position;
      }

      if (killedEnemies.Count == currentLevel)
      {
        enemies.Clear();
        killedEnemies.Clear();
        yield return new WaitForSeconds(3f);
        currentLevel++;
        levelText.text = "Level " + currentLevel;
      }
    }

    yield return null;
    StartCoroutine(spawn());
  }
}
