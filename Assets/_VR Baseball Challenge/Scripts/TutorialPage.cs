using UnityEngine;
using UnityEngine.UI;

public class TutorialPage : MonoBehaviour
{
    [SerializeField] private Button _homeButton;

    [SerializeField] private GameObject _homePage;

    private void Awake()
    {
        _homeButton.onClick.AddListener(() =>
        {
            _homePage.SetActive(true);
            gameObject.SetActive(false);
        });
    }
}
