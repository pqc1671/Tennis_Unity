using System;
using System.Linq;
using UnityEngine;

public class EnergyManager : SingletonPersistent<EnergyManager>
{
    [SerializeField] private int _energy;

    public int Energy
    {
        get
        {
            return _energy;
        }
        set
        {
            _energy = value;
            PlayerPrefs.SetInt("Energy", _energy);

            //set UI
            FindObjectsOfType<EnergyText>().ToList().ForEach(i => i.SetText(Energy.ToString()));
        }
    }

    private void Start()
    {
        CheckDay();
    }

    private void CheckDay()
    {
        //new game
        if(!PlayerPrefs.HasKey("LastTime"))
        {
            ResetDay();
            return;
        }
        //
        string lateTimeString = PlayerPrefs.GetString("LastTime");
        if(DateTime.TryParse(lateTimeString, out var lateTime))
        {
            if(DateTime.Now.Subtract(lateTime).TotalDays >= 1)
            {
                ResetDay();
            }
            else
            {
                Energy = PlayerPrefs.GetInt("Energy");
            }
        }
        else
        {
            //new game || error
            ResetDay();
        }
    }

    private void ResetDay()
    {
        Energy = 10;
        PlayerPrefs.SetString("LastTime", DateTime.Now.ToString());
    }
}
