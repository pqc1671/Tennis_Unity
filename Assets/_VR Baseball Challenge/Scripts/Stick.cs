using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    [SerializeField] private List<GameObject> _models;
    private Vector3 _originalPosition;

    public Vector3 OriginalPosition => _originalPosition;

    private void Start()
    {
        _originalPosition = transform.position;
    }

    public void SetModel(int id)
    {
        _models.ForEach(i => i.SetActive(false));
        _models[id].SetActive(true);
    }
}
