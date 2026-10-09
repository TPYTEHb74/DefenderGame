using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
    // —юда мы перетащим шаблон нашего красного врага
    public GameObject[] enemyPrefabs;

    //  ак часто по€вл€ютс€ враги (раз в 2 секунды)
    [SerializeField]
    public float spawnRate = 2f;

    public float minX = -2.5f;
    public float maxX = 2.5f;

    private void Start()
    {
        // «апускаем автоматический повтор создани€ врагов
        // ћетод SpawnEnemy будет вызыватьс€ каждые spawnRate секунд
        InvokeRepeating("SpawnEnemy", 1f, spawnRate);
    }

    private void SpawnEnemy()
    {
        // ¬ыбираем случайную позицию по горизонтали (X)
        float randomX = Random.Range(minX, maxX);

        // —оздаем финальную точку по€влени€ (Y берем от самого спавнера)
        Vector3 spawnPosition = new Vector3(randomX,transform.position.y, 0f);

        int randomIndex = Random.Range(0, enemyPrefabs.Length);

        // —оздаем врага в этой точке
        Instantiate(enemyPrefabs[randomIndex], spawnPosition, Quaternion.identity);
    }

}
