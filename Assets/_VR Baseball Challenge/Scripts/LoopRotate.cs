using DG.Tweening;
using UnityEngine;

public class LoopRotate : MonoBehaviour
{
    private void Start()
    {
        transform.DOScale(Vector3.one * 1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
}
