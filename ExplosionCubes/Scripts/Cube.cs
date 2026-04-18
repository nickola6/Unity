using UnityEngine;
using System.Collections.Generic;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _initialSplitChance = 100f;
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private Explosion _explosion;

    private readonly List<GameObject> _createdCubes = new();

    public IReadOnlyList<GameObject> CreatedCubes => _createdCubes.AsReadOnly();

    public void OnMouseDown()
    {
        float childSplitChance = _initialSplitChance / 2f;
        float randomValue = Random.value * _initialSplitChance + 1;

        if (randomValue < _initialSplitChance)
        {
            List<GameObject> newCreatedCubes = _cubeSpawner.SpawnCubes(transform.position, this, childSplitChance);
            _createdCubes.AddRange(newCreatedCubes);
            _explosion.ApplyExplosion(transform.position, newCreatedCubes);
        }

        Destroy(gameObject);
    }

    public void SetSplitChance(float splitChance)
    {
        _initialSplitChance = splitChance;
    }
}