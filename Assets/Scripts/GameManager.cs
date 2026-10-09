using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Текстовые элементы")]
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverText;


    [Header("Игровые параметры")]
    public int maxHP = 5;
    private int currentHP;
    private int currentGold = 0;
    private int scoreCount = 0;
    private bool isGameOver = false;

    void Start()
    {
        currentHP = maxHP;
        UpdateGoldUI();
        UpdateHPUI();
        UpdateScoreUI();

        gameOverText.SetActive(false);
        // Включаем нормальную скорость игры
        Time.timeScale = 1f;
    }

    private void Update()
    {
        // Если игра окончена и игрок нажал любую кнопку мыши
        // перезапускаем уровень заново
        if (isGameOver && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    // Этот метод мы будем вызывать из скрипта врага при его смерти
    public void AddGold(int amount)
    {
        if (isGameOver) return;
        currentGold += amount; // Прибавляем золото в кошелек
        UpdateGoldUI();        // Сразу обновляем цифры на экране
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;

        scoreCount += amount;
        UpdateScoreUI();
    }

    // Этот метод будет вызываться, когда враг прорвал линию обороны
    public void TakeCastleDamage(int damage)
    {
        if (isGameOver) return;

        currentHP -= damage;
        UpdateHPUI();

        if (currentHP <= 0)
        {
            EndGame();
        }
    }

    // Метод, который просто перерисовывает цифры на экране
    void UpdateGoldUI()
    {
        goldText.text = "Gold: " + currentGold;
    }

    void UpdateHPUI()
    {
        hpText.text = "HP: " + currentHP;
    }

    // Метод, который перерисовывает фраги на экране
    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + scoreCount;
    }

    // Этот метод проверяет, хватает ли золота, и списывает его
    public bool TrySpendGold(int price)
    {
        if (currentGold >= price)
        {
            currentGold -= price;
            UpdateGoldUI();
            return true;
        }
        return false;
    }

    void EndGame()
    {
        isGameOver = true;
        gameOverText.SetActive(true); // Показываем надпись GAME OVER
        Time.timeScale = 0f; // Полностью ОСТАНАВЛИВАЕМ время в игре
    }

}
