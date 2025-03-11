using Unity.VisualScripting;
using UnityEngine;

public class CanTurnOnLine : MonoBehaviour
{
    [SerializeField]Raycast ray;
    
    private void Start()
    {
        GamePlayManager.Instance.OnGameDone += TurnOnLine;
        GamePlayManager.Instance.OnGamePlay += TurnOffLine;
    }

    void TurnOnLine()
    {
        ray.IsActive = true;
    }

    void TurnOffLine()
    {
        Debug.Log("Turn off line");
        ray.IsActive = false;
    }
}
