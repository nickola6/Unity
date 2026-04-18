using UnityEngine;
using System.Collections.Generic;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _force = 5f;
    [SerializeField] private float _radius = 3f;

    public void ApplyExplosion(Vector3 explosionPosition, List<GameObject> targetObjects)
    {
        for (int i = 0; i < targetObjects.Count; i++)
        {
            GameObject targetObject = targetObjects[i];

            if (targetObject == null)
                continue;

            Rigidbody rigidbody = targetObject.GetComponent<Rigidbody>();

            if (rigidbody == null)
                continue;

            rigidbody.AddExplosionForce(_force, explosionPosition, _radius, 1f, ForceMode.Impulse);
        }
    }
}