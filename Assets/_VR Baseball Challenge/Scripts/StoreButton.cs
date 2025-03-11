using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StoreButton : MonoBehaviour
{
    [SerializeField] private Image _iconCoin;

    private void Start()
    {
        AnimIconCoin();
    }

    private void AnimIconCoin()
    {
        _iconCoin.rectTransform.anchoredPosition = new Vector2(0, 100);
        _iconCoin.rectTransform.DOAnchorPosY(0, 1f).OnComplete(() =>
        {
            Invoke(nameof(AnimIconCoin), 1);
        });
    }
}
