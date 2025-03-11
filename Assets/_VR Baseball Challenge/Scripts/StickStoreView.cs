using System.Collections.Generic;
using UnityEngine;

public class StickStoreView : Singleton<StickStoreView>
{
    [SerializeField] private StickItem _stickItemPrefab;
    [SerializeField] private List<StickItem> _stickItems = new List<StickItem>();
    [SerializeField] private Transform _stickItemContainer;

    private void OnEnable()
    {
        Init(StickManager.Instance.Data);
    }

    public void Init(List<StickData> data)
    {
        if (data.Count > _stickItems.Count)
        {
            for (int i = _stickItems.Count; i < data.Count; i++)
            {
                var item = Instantiate(_stickItemPrefab, _stickItemContainer);
                _stickItems.Add(item);
            }
        }
        if (data.Count < _stickItems.Count)
        {
            for (int i = data.Count; i < _stickItems.Count; i++)
            {
                _stickItems[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < data.Count; i++)
        {
            _stickItems[i].Init(data[i].id, data[i].isOwner, data[i].price);
            _stickItems[i].gameObject.SetActive(true);
        }
    }

    public void UpdateUI()
    {
        foreach (var item in _stickItems)
        {
            item.UpdateUI();
        }
    }
}
