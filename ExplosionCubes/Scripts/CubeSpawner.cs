using UnityEngine;
using System.Collections.Generic;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _minCountCubes = 2;
    [SerializeField] private int _maxCountCubes = 6;

    public List<GameObject> SpawnCubes(Vector3 spawnPosition, Cube parentCube, float childSplitChance)
    {
        int count = Random.Range(_minCountCubes, _maxCountCubes + 1);
        List<GameObject> spawnedCubes = new();

        for (int i = 0; i < count; i++)
        {
            GameObject newCube = Instantiate(_prefab, spawnPosition, Quaternion.identity);
            newCube.transform.localScale = parentCube.transform.localScale / 2f;

            Renderer renderer = newCube.GetComponent<Renderer>();
            renderer.material.color = Random.ColorHSV();

            Cube newCubeComponent = newCube.GetComponent<Cube>();

            if (newCubeComponent != null)
            {
                newCubeComponent.SetSplitChance(childSplitChance);
            }

            spawnedCubes.Add(newCube);
        }

        return spawnedCubes;
    }
}