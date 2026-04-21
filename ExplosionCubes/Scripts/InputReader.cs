using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action OnClick;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnClick?.Invoke();
    }
}