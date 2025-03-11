using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButton : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private Image _arrow;
    [SerializeField] private Button _button;
    private string fullText = "Tutorial";
    private float typeSpeed = 0.1f;

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
