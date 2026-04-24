using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _baseForce = 10f;
    [SerializeField] private float _baseRadius = 5f;

    private const float MinMultiplier = 0.5f;
    private const float MaxMultiplier = 3f;

    public void ApplyExplosion(Vector3 position, float cubeSize)
    {
        const float BaseMultiplier = 1f;

        float multiplier = Mathf.Clamp(BaseMultiplier / cubeSize, MinMultiplier, MaxMultiplier);
        float force = _baseForce * multiplier;
        float radius = _baseRadius * multiplier;

        Collider[] colliders = Physics.OverlapSphere(position, radius);

        float upwardsModifier = 1f;

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(force, position, radius, upwardsModifier, ForceMode.Impulse);
            }
        }
    }
}