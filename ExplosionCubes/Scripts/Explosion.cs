using UnityEngine;
using System.Collections.Generic;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _force = 5f;
    [SerializeField] private float _radius = 3f;

    public void ApplyExplosion(Vector3 explosionPosition, List<Cube> cubes)
    {
        foreach (Cube cube in cubes)
        {
            if (cube == null)
                continue;

            if (cube.TryGetComponent(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(_force, explosionPosition, _radius, 1f, ForceMode.Impulse);
        }
    }
}