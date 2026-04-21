using UnityEngine;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _minCountCubes = 2;
    [SerializeField] private int _maxCountCubes = 6;

    public List<Cube> SpawnCubes(Vector3 spawnPosition, Vector3 parentScale, float childSplitChance)
    {
        int count = Random.Range(_minCountCubes, _maxCountCubes + 1);
        List<Cube> spawnedCubes = new();

        for (int i = 0; i < count; i++)
        {
            Cube newCube = Instantiate(_prefab, spawnPosition, Quaternion.identity);

            newCube.transform.localScale = parentScale / 2f;

            if (newCube.TryGetComponent(out Renderer renderer))
            {
                renderer.material.color = Random.ColorHSV();
            }

            newCube.SetSplitChance(childSplitChance);

            spawnedCubes.Add(newCube);
        }

        return spawnedCubes;
    }
}