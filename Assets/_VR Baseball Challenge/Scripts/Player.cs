using Autohand;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private AudioSource _footStepSound;
    private bool _isMoving;

    private void Update()
    {
        if(_rb.linearVelocity != Vector3.zero)
        {
            if (!_isMoving)
            {
                _isMoving = true;
                _footStepSound.Play();
            }
        }
        else
        {
            if (_isMoving)
            {
                _isMoving = false;
                _footStepSound.Stop();
            }
        }
    }
}
