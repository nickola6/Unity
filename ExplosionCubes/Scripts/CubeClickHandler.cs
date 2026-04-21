using UnityEngine;

public class CubeClickHandler : MonoBehaviour
{
    [SerializeField] private Raycaster _raycaster;

    private void OnEnable()
    {
        _raycaster.OnRaycastHit += HandleHit;
    }

    private void OnDisable()
    {
        _raycaster.OnRaycastHit -= HandleHit;
    }

    private void HandleHit(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out Cube cube))
            cube.HandleClick();
    }
}