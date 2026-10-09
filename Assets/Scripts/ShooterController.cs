using UnityEngine;
using TMPro;

public class ShooterController : MonoBehaviour 
{
    public GameObject arrowPrefab;
    public Transform firePoint;

    [Header("Настройки Магазина")]
    public TextMeshProUGUI buttonText;
    private int upgradePrice = 50;

    [Header("Настройка стрельбы")]
    public float fireRate = 1f;

    private float nextFireTime = 0f;

    private void Start()
    {
        UpdateButtonText();
    }

    private void Update()
    {
        //Поворот арбалета за пальцем
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, 0f, 180f);
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        //Автострельба
        if (Input.GetMouseButton(0) && Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
    }

    public void UpgradeFireRate()
    {
        if (FindFirstObjectByType<GameManager>().TrySpendGold(upgradePrice))
        {
            fireRate = fireRate * 0.8f;
            if (fireRate < 0.05f) fireRate = 0.05f;

            upgradePrice += 50;

            UpdateButtonText();

            Debug.Log("Арбалет прокачан через UI! Новая скорость: " +  fireRate);
        }
    }

    void UpdateButtonText()
    {
        buttonText.text = "Speed UP\n" + upgradePrice + "Gold";
    }

}
