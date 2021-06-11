using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManagerController : MonoBehaviour
{
    public GameObject enemyPrefab;

    public int lowerSpawnFrequency;
    public int upperSpawnFrequency;

    private float _elapsedTime;

    private int _respawnFrequency;

    public bool enemyMovesLeft;

    private List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {
        _respawnFrequency = Random.Range(lowerSpawnFrequency, upperSpawnFrequency);
        _elapsedTime = 0;
        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _respawnFrequency)
        {
            _respawnFrequency = Random.Range(lowerSpawnFrequency, upperSpawnFrequency);
            _elapsedTime = 0;

            var enemy = Instantiate(enemyPrefab, transform.position, transform.rotation, transform);
            enemy.GetComponent<EnemyController>().moveLeft = enemyMovesLeft;
            enemies.Add(enemy);


            //if (enemyMovesLeft)
            //    enemy.GetComponent<Animator>().SetBool("MoveLeft", true);
        }
    }
}
