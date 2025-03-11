using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button play, homeButton1,homeButton2,homeButton3;

    [SerializeField] private GameObject notEnoughtEnergy, main,shop,tutorial;

    private void Start()
    {
        Button();
    }

    private void Button()
    {
        play.onClick.AddListener(Play);
        homeButton1.onClick.AddListener(Home);
        homeButton2.onClick.AddListener(Home);
        homeButton3.onClick.AddListener(Home);
    }

    private void Play()
    {

        if (EnergyController.Instance.Energy > 0)
        {
            Debug.Log("e");
            EnergyController.Instance.UseEnergy();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Tennis");
        }
        else
        {
            Debug.Log("ab");
            notEnoughtEnergy.SetActive(true);
            Debug.Log("abcd");
            main.SetActive(false);
        }
    }
    private void Home()
    {
        main.SetActive(true);
        if(notEnoughtEnergy.activeSelf)
        {
            notEnoughtEnergy.SetActive(false);
        }
        if (shop.activeSelf)
        {
            shop.SetActive(false);
        }
        if(tutorial.activeSelf)
        {
            tutorial.SetActive(false);
        }
    }
}
