using UnityEngine;

public class VongTron : MonoBehaviour
{
    private float speed = 5f; // Tốc độ di chuyển
    private Rigidbody rb;
    private Vector3 direction;
    private Bounds bounds;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        direction = Random.insideUnitCircle.normalized; // Hướng ngẫu nhiên

        bounds = transform.parent.GetComponent<BoxCollider>().bounds;
    }
    public float Speed()
    {
        return speed + ((speed * (float)GamePlayManager.Instance.Lv) / 10);
    }

    void FixedUpdate()
    {
        if(GamePlayManager.Instance.Lv == 0)
        {

        }
        else
        {
            Vector3 newPosition = transform.position + direction * speed * Time.fixedDeltaTime;

            // Kiểm tra va chạm với viền BoxCollider từ bên trong
            if (newPosition.x - transform.localScale.x / 2 < bounds.min.x || newPosition.x + transform.localScale.x / 2 > bounds.max.x)
            {
                direction.x *= -1; // Đảo hướng X
            }

            if (newPosition.y - transform.localScale.y / 2 < bounds.min.y || newPosition.y + transform.localScale.y / 2 > bounds.max.y)
            {
                direction.y *= -1; // Đảo hướng Y
            }

            rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
        }
    }

}
