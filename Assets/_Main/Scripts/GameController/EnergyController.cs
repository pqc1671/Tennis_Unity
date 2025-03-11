using System.Linq;
using UnityEngine;

public class EnergyController : SingletonPersistent<EnergyController>
{
    private int energy;
    [SerializeField] private AudioSource _audio;

    public int Energy
    {
        get => energy;
        set
        {
            energy = value;
        }
    }
    private void Start()
    {
        Energy = PlayerPrefs.GetInt("Energy", Energy);
        FindObjectsOfType<EnergyText>().ToList().ForEach(i => i.SetText(Energy.ToString()));
    }
    public void UseEnergy()
    {
        Energy = PlayerPrefs.GetInt("Energy", Energy);
        Energy -= 1;
        PlayerPrefs.SetInt("Energy", Energy);
        FindObjectsOfType<EnergyText>().ToList().ForEach(i => i.SetText(Energy.ToString()));
    }

    public void AddEnergy(int amount)
    {
        Energy += amount;
        if (_audio != null)
        {
            _audio.Play();
        }
        PlayerPrefs.SetInt("Energy", Energy);
        FindObjectsOfType<EnergyText>().ToList().ForEach(i => i.SetText(Energy.ToString()));
    }


}
