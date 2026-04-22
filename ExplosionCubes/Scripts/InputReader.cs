using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action OnClick;

    private void Update()
    {
        const int LeftMouseButtonIndex = 0;

        if (Input.GetMouseButtonDown(LeftMouseButtonIndex))
            OnClick?.Invoke();
    }
}
