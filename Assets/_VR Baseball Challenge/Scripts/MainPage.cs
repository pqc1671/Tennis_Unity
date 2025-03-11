using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPage : MonoBehaviour
{
    [SerializeField] private Button _tutorialButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _storeButton;

    [SerializeField] private GameObject _storePage;
    [SerializeField] private GameObject _tutorialPage;

    private void Awake()
    {
        _tutorialButton.onClick.AddListener(OnTutorialButtonClicked);
        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _storeButton.onClick.AddListener(OnStoreButtonClicked);
    }

    private void OnTutorialButtonClicked()
    {
        gameObject.SetActive(false);
        _tutorialPage.SetActive(true);
    }

    private void OnPlayButtonClicked()
    {
        SceneManager.LoadSceneAsync("Gameplay");
    }

    private void OnStoreButtonClicked()
    {
        gameObject.SetActive(false);
        _storePage.SetActive(true);
    }
}
