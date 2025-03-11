using Oculus.Platform;
using Oculus.Platform.Models;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Sku
{
    public string sku;
    public string price;

    public Sku(string sku, string price)
    {
        this.sku = sku;
        this.price = price;
    }

    public void Buy()
    {
        if (sku.Contains("energy"))
        {
            int amount;
            if (int.TryParse(sku.Split('_')[1], out amount))
            {
                EnergyController.Instance.AddEnergy(amount);
                Debug.Log($"[IAP] Added {amount} energy.");
            }
            else
            {
                Debug.LogError($"[IAP] Failed to parse energy amount from SKU: {sku}");
            }
        }
    }
}

public class OculusIAP : MonoBehaviour
{
    private static OculusIAP _instance;
    public static OculusIAP Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<OculusIAP>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("OculusIAP");
                    _instance = obj.AddComponent<OculusIAP>();
                    DontDestroyOnLoad(obj);
                }
            }
            return _instance;
        }
    }

    private Dictionary<string, Sku> skuDictionary;
    private Dictionary<UInt64, string> pendingConsumptions = new Dictionary<UInt64, string>();

    void Start()
    {
        InitializeSKUs();
        Core.AsyncInitialize().OnComplete(InitCallback);
    }

    private void InitializeSKUs()
    {
        skuDictionary = new Dictionary<string, Sku>
        {
            { "energy_1", new Sku("energy_1", "0.99") },
            { "energy_5", new Sku("energy_5", "4.99") },
            { "energy_10", new Sku("energy_10", "9.99") },
            { "energy_20", new Sku("energy_20", "19.99") },
            { "energy_50", new Sku("energy_50", "49.99") }
        };
    }

    private void InitCallback(Message<PlatformInitialize> msg)
    {
        if (msg.IsError)
        {
            Debug.LogError($"[IAP] Error initializing Oculus Platform: {msg.GetError().Message}");
            return;
        }

        Debug.Log("[IAP] Oculus Platform initialized successfully.");
        Entitlements.IsUserEntitledToApplication().OnComplete(EntitlementCheckCallback);
    }

    private void EntitlementCheckCallback(Message msg)
    {
        if (msg.IsError)
        {
            Debug.LogError("[IAP] User not entitled to application.");
            return;
        }

        Debug.Log("[IAP] User is entitled.");
        GetPrices();
        GetPurchases();
    }

    private void GetPrices()
    {
        string[] skus = new string[skuDictionary.Count];
        skuDictionary.Keys.CopyTo(skus, 0);
        IAP.GetProductsBySKU(skus).OnComplete(GetPricesCallback);
    }

    private void GetPricesCallback(Message<ProductList> msg)
    {
        if (msg.IsError)
        {
            Debug.LogError("[IAP] Failed to retrieve prices.");
            return;
        }

        foreach (var prod in msg.GetProductList())
        {
            Debug.Log($"[IAP] {prod.Name} - {prod.FormattedPrice}");
        }
    }

    private void GetPurchases()
    {
        IAP.GetViewerPurchases().OnComplete(GetPurchasesCallback);
    }

    private void GetPurchasesCallback(Message<PurchaseList> msg)
    {
        if (msg.IsError)
        {
            Debug.LogError("[IAP] Failed to retrieve purchases.");
            return;
        }

        foreach (var purchase in msg.GetPurchaseList())
        {
            Debug.Log($"[IAP] Consuming purchase: {purchase.Sku}");
            ConsumePurchase(purchase.Sku);
        }
    }

    private void ConsumePurchase(string skuName)
    {
        var request = IAP.ConsumePurchase(skuName);
        pendingConsumptions[request.RequestID] = skuName;
        request.OnComplete(ConsumePurchaseCallback);
    }

    private void ConsumePurchaseCallback(Message msg)
    {
        if (msg.IsError)
        {
            Debug.LogError($"[IAP] Error consuming purchase: {msg.GetError().Message}");
            return;
        }

        if (pendingConsumptions.TryGetValue(msg.RequestID, out string sku))
        {
            Debug.Log($"[IAP] Purchase consumed successfully: {sku}");
            AllocateCoins(sku);
            pendingConsumptions.Remove(msg.RequestID);
        }
        else
        {
            Debug.LogWarning("[IAP] Purchase consumed, but SKU not found.");
        }
    }

    public bool AllocateCoins(string skuName)
    {
        if (skuDictionary.TryGetValue(skuName, out Sku sku))
        {
            sku.Buy();
            return true;
        }
        Debug.LogError($"[IAP] SKU not found: {skuName}");
        return false;
    }

    public void Buy(string skuName)
    {
#if UNITY_EDITOR
        AllocateCoins(skuName);
#else
        IAP.LaunchCheckoutFlow(skuName).OnComplete(BuyCallBack);
#endif
    }

    private void BuyCallBack(Message<Purchase> msg)
    {
        if (msg.IsError)
        {
            Debug.LogError("[IAP] Purchase failed.");
            return;
        }

        Debug.Log("[IAP] Purchase successful.");
        GetPurchases();
    }
}
