using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StickItem : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _useButton;
    [SerializeField] private GameObject _selectingObj;
    [SerializeField] private int _id;
    [SerializeField] private Text _priceText;

    private void Awake()
    {
        _buyButton.onClick.AddListener(Buy);
        _useButton.onClick.AddListener(Use);
    }

    public void Init(int id, bool isOwner, float price)
    {
        _id = id;
        _icon.sprite = Resources.Load<Sprite>($"Stick 2d/stick-{id}");
        _priceText.text = $"{price}$";

        UpdateUI();
    }

    public void UpdateUI()
    {
        if (StickManager.Instance.CurrentStick == _id)
        {
            _selectingObj.SetActive(true);
            _buyButton.gameObject.SetActive(false);
            _useButton.gameObject.SetActive(false);
        }
        else
        {
            _selectingObj.SetActive(false);
            if (StickManager.Instance.CheckOwner(_id))
            {
                _buyButton.gameObject.SetActive(false);
                _useButton.gameObject.SetActive(true);
            }
            else
            {
                _buyButton.gameObject.SetActive(true);
                _useButton.gameObject.SetActive(false);
            }
        }
    }

    public void Buy()
    {
        StickManager.Instance.Buy(_id);
    }

    public void Use()
    {
        if(StickManager.Instance.Select(_id))
        {
            StickStoreView.Instance.UpdateUI();
        }
        //todo: use failed
    }
}
