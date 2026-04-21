using UnityEngine;
using System.Collections.Generic;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _initSplitChance = 100f;
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private Explosion _explosion;

    private readonly List<Cube> _createdCubes = new();

    public IReadOnlyList<Cube> CreatedCubes => _createdCubes.AsReadOnly();

    public void HandleClick()
    {
        float childSplitChance = _initSplitChance / 2f;

        float randomValue = Random.value * _initSplitChance + 1;

        Vector3 scale = transform.localScale;

        if (randomValue < _initSplitChance)
        {
            var newCubes = _cubeSpawner.SpawnCubes(transform.position, scale, childSplitChance);
            _createdCubes.AddRange(newCubes);

            _explosion.ApplyExplosion(transform.position, newCubes);
        }

        Destroy(gameObject);
    }

    public void SetSplitChance(float splitChance)
    {
        _initSplitChance = splitChance;
    }
}