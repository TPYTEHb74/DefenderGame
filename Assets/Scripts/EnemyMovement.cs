using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //скорость движения врага
    [Header("Настройки скорости")]
    public float speed = 2f;

    [Header("Настройки здоровья")]
    public float maxHealth = 3f;
    

    private float currentHealth;

    private void Start()
    {
        // При появлении врага заполняем его здоровье на максимум
        currentHealth = maxHealth;
    }

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y < -3.6f)
        {
            FindFirstObjectByType<GameManager>().TakeCastleDamage(1);

            Destroy(gameObject);
        }
    }

    // Этот метод автоматически срабатывает в Unity,
    // когда в этот объект влетает "Trigger" (наша стрела)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что в нас врезалась именно стрела
        // (ищем на ней скрипт ArrowMovement)
        if (other.GetComponent<ArrowMovement>() != null)
        {
            // Отнимаем у врага 1 единицу здоровья
            TakeDamage(1f);

            // Уничтожаем стрелу, чтобы она не летела дальше
            Destroy(other.gameObject);
        }
    }

    void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            // Даем золото
            FindFirstObjectByType<GameManager>().AddGold(10);

            // Засчитываем убийство в статистику!
            FindFirstObjectByType<GameManager>().AddScore(1);

            Destroy(gameObject);
        }
    }
}
