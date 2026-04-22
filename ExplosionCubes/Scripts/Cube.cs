using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _splitChance = 100f;

    public event Action<Cube> OnClicked;

    public float SplitChance => _splitChance;

    public void SetSplitChance(float value)
    {
        _splitChance = value;
    }

    public void Click()
    {
        OnClicked?.Invoke(this);
    }
}