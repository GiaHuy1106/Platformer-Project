using UnityEngine;

public class enemiesSpawn : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] spawnPoints;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            SpawnEnemy();
        }
    }
    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        GameObject newEnemy = Instantiate(enemy, spawnPoints[randomIndex].position, Quaternion.identity);

        // Lật qua trái
        SpriteRenderer sr = newEnemy.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.flipX = true;
        }
    }

}
