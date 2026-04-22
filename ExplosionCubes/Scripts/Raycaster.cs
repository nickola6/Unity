using System;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private InputReader _inputReader;

    public event Action<Cube> OnCubeHit;

    private void OnEnable()
    {
        _inputReader.OnClick += HandleClick;
    }

    private void OnDisable()
    {
        _inputReader.OnClick -= HandleClick;
    }

    private void HandleClick()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out Cube cube))
            {
                OnCubeHit?.Invoke(cube);
            }
        }
    }
}