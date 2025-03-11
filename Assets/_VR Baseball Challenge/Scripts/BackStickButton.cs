using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BackStickButton : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private Image _arrow;
    [SerializeField] private Button _button;
    private string fullText = "Back Stick";
    private float typeSpeed = 0.1f;
    [SerializeField] private Stick _stick;

    private void Awake()
    {
        _button.onClick.AddListener(() =>
        {
            AudioManager.Instance.Click.PlayScheduled(0);
            var rb = _stick.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            _stick.transform.rotation = Quaternion.Euler(0, 0, 0);
            _stick.transform.position = _stick.OriginalPosition;
            StartCoroutine(EnablePhysics(rb));
        });
    }

    private IEnumerator EnablePhysics(Rigidbody rb)
    {
        yield return new WaitForFixedUpdate(); // Đợi 1 frame để chắc chắn vị trí đã cập nhật
        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero; // Reset vận tốc để tránh di chuyển không mong muốn
        rb.angularVelocity = Vector3.zero; // Reset quay
    }

    private void Start()
    {
        StartTypingLoop();
    }

    void StartTypingLoop()
    {
        _arrow.fillAmount = 0;
        _text.text = ""; // Xóa nội dung ban đầu
        DOTween.To(() => 0, x => _text.text = fullText.Substring(0, x), fullText.Length, fullText.Length * typeSpeed)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                _arrow.DOFillAmount(1, .5f).OnComplete(() =>
                {
                    _button.transform.DOShakeRotation(.1f).OnComplete(
                        () => { Invoke(nameof(StartTypingLoop), 1f); }
                    );
                });
            });
    }
}
