using UnityEngine;
using UnityEngine.UI;

public class EnergyText : MonoBehaviour
{
    [SerializeField] private Text _txt;

    protected virtual void OnEnable()
    {
        SetText(EnergyController.Instance.Energy.ToString());
    }

    public virtual void SetText(string txt)
    {
        _txt.text = txt;
    }
}
