using System.Collections.Generic;
using UnityEngine;

public class StickManager : SingletonPersistent<StickManager>
{
    [SerializeField] private int _currentStick;
    [SerializeField] private List<StickData> _data = new List<StickData>();

    public int CurrentStick => _currentStick;
    public List<StickData> Data => _data;

    protected override void Awake()
    {
        base.Awake();
        _data.Add(new StickData(0, true, 0));
        _data.Add(new StickData(1, false, 9.9f));
        _data.Add(new StickData(2, false, 19.9f));
        _data.Add(new StickData(3, false, 49.9f));
        _data.Add(new StickData(4, false, 99.9f));

        foreach (var item in _data)
        {
            if (!PlayerPrefs.HasKey($"stick-{item.id}"))
            {
                PlayerPrefs.SetInt($"stick-{item.id}", item.isOwner ? 1 : 0);
            }
            else
            {
                item.isOwner = PlayerPrefs.GetInt($"stick-{item.id}") == 1;
            }
        }

        _currentStick = PlayerPrefs.GetInt("current-stick", 0);
    }

    public bool Select(int id)
    {
        if (!_data.Exists(x => x.id == id))
        {
            return false;
        }
        _currentStick = id;
        PlayerPrefs.SetInt("current-stick", id);
        return true;
    }

    public void Buy(int id)
    {
        OculusIAP.Instance.Buy(_data[id].GetSku());
    }

    public void BuySuccess(int id)
    {
        if (!_data.Exists(x => x.id == id))
        {
            return;
        }
        _data[id].isOwner = true;
        PlayerPrefs.SetInt($"stick-{id}", _data[id].isOwner ? 1 : 0);
        StickStoreView.Instance.UpdateUI();
    }

    public bool CheckOwner(int id)
    {
        if (!_data.Exists(x => x.id == id))
        {
            return false;
        }
        return _data[id].isOwner;
    }
}

public class StickData
{
    public int id;
    public bool isOwner;
    public float price;

    public StickData(int id, bool isOwner, float price)
    {
        this.id = id;
        this.isOwner = isOwner;
        this.price = price;
    }

    public string GetSku()
    {
        return $"stick-{id}";
    }
}
