using System.Collections;
using UnityEngine;

public class MachineShootBall : MonoBehaviour
{
    [SerializeField] private Ball _ballPrefab;
    [SerializeField] float force = 10;
    [SerializeField] float duration = 2.8f;
    [SerializeField] int count = 20;
    bool isSpawnx3Point;
    [SerializeField] private GameObject _x3PointPrefab;

    [SerializeField] private Transform _shootPoint1, _shootPoint2;
    public AudioSource audioSource;

    [SerializeField] private Transform dirTransform;

    public int Count => count;

    private void Start()
    {
        //StartCoroutine(StartShootBall(1));
    }

    public IEnumerator ShootBall()
    {
        while (true)
        {
            yield return new WaitForSeconds(duration);
            audioSource.PlayScheduled(0);
            var ball = Instantiate(_ballPrefab, _shootPoint1.position, Quaternion.identity);
            Vector3 direction = (_shootPoint2.position - _shootPoint1.position).normalized;
            float deltaForce = Random.Range(-.05f, 0.1f);
            dirTransform.localEulerAngles = new Vector3(dirTransform.localEulerAngles.x, Random.Range(-5f, 5f), dirTransform.localEulerAngles.z);
            ball.Init(direction, force + deltaForce);
            count--;
            if (count < 15 && !isSpawnx3Point)
            {
                int i = count > 1 ? Random.Range(0, 2) : 1;
                if (i == 1)
                {
                    _x3PointPrefab.SetActive(true);
                    Invoke(nameof(Destroyx3Point), 5);
                    isSpawnx3Point = true;
                }
            }
            if (count == 0)
            {
                break;
            }
        }
    }



    void Destroyx3Point()
    {
        _x3PointPrefab.SetActive(false);
    }

    public IEnumerator StartShootBall()
    {
        yield return StartCoroutine(ShootBall());
    }
}
