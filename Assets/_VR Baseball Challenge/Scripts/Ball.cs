using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody rb;
    public AudioSource _source;
    public float bounce;

    private void ClearObject()
    {
        Destroy(gameObject);
    }

    public void Init(Vector3 direction, float forece)
    {
        rb.AddForce(direction * forece, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stick"))
        {
            _source.Play();
            ContactPoint contact = collision.contacts[0]; // Lấy điểm va chạm
            Vector3 normal = contact.normal; // Hướng pháp tuyến
            Vector3 incomingVelocity = rb.linearVelocity; // Vận tốc ban đầu

            // Phản chiếu vận tốc theo pháp tuyến
            Vector3 reflectedVelocity = Vector3.Reflect(incomingVelocity, normal);

            rb.linearVelocity = reflectedVelocity.normalized * bounce; // Cập nhật vận tốc
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("10Point"))
        {
            ScoreManager.Instance.AddScore(10);
            Invoke(nameof(ClearObject), 3);
        }
        if (other.gameObject.CompareTag("20Point"))
        {
            ScoreManager.Instance.AddScore(20);
            Invoke(nameof(ClearObject), 3);
        }
        if (other.gameObject.CompareTag("50Point"))
        {
            ScoreManager.Instance.AddScore(50);
            Invoke(nameof(ClearObject), 3);
        }
        if (other.gameObject.CompareTag("x3"))
        {
            ScoreManager.Instance.Addx3Score();
            Invoke(nameof(ClearObject), 3);
        }
        if (other.gameObject.CompareTag("Miss"))
        {
            ScoreManager.Instance.IsMiss();
            Invoke(nameof(ClearObject), 3);
        }
    }
}
