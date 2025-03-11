using UnityEngine;
using UnityEngine.UI;

public class StorePage : MonoBehaviour
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _turnButton;
    [SerializeField] private Button _stickButton;

    [SerializeField] private GameObject _turnPanel;
    [SerializeField] private GameObject _stickPanel;

    [SerializeField] private GameObject _homePage;

    private void Awake()
    {
        _homeButton.onClick.AddListener(() =>
        {
            _homePage.SetActive(true);
            gameObject.SetActive(false);
        });

        _turnButton.onClick.AddListener(() =>
        {
            _turnPanel.SetActive(true);
            _stickPanel.SetActive(false);
        });

        _stickButton.onClick.AddListener(() =>
        {
            _stickPanel.SetActive(true);
            _turnPanel.SetActive(false);
        });
    }
}
