using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private int nEnemies;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float frecuencia = 60f;
    private float timer;
    [SerializeField] private Transform spawnParent;
    private Vector3[] spawnPoints;

    void OnValidate()
    {
        nEnemies = Mathf.Max(nEnemies, 0);
    }

    void Start()
    {
        spawnPoints = new Vector3[spawnParent.childCount];
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = spawnParent.GetChild(i).position;
        }
        timer = frecuencia;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            if(frecuencia > 10) frecuencia -= 5;
            timer = frecuencia;
            int n1 = Random.Range(1, nEnemies);
            SpawnEnemy(enemies[0], n1);
            SpawnEnemy(enemies[1], nEnemies - n1);
        }

        void SpawnEnemy(GameObject enemy, int n)
        {
            for (int i = 0; i < n; i++)
            {
                Instantiate(enemy, spawnPoints[Random.Range(0, spawnPoints.Length)], Quaternion.identity);
            }
        }
    }
}
