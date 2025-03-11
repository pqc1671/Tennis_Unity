using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class ButtonOVR : MonoBehaviour
{
    public Button btn;
    public bool autoFixCollider = false;
    private void Start()
    {
        btn = GetComponent<Button>();
        if (autoFixCollider)
        {
            GetComponent<BoxCollider>().size = new Vector3(GetComponent<RectTransform>().rect.size.x, GetComponent<RectTransform>().rect.size.y, .1f);
        }
    }
    public void OnRaycastClick()
    {
        transform.DOScale(new Vector3(.8f, .8f, .8f), .1f).onComplete += ()=>
        {
            transform.DOScale(new Vector3(1, 1, 1), .1f).onComplete += ()=> btn.onClick.Invoke();
        };
    }
}
