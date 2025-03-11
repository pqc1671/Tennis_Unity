using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private string _sku;

    private void Awake()
    {
        _button.onClick.AddListener(() =>
        {
            OculusIAP.Instance.Buy(_sku);

        });
    }
}
