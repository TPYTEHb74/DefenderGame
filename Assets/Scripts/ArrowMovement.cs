using UnityEngine;

public class ArrowMovement : MonoBehaviour 
{
    //скорость стрелы
    public float speed = 8f;

    private float lifeTime = 3;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed *  Time.deltaTime);
    }
}
