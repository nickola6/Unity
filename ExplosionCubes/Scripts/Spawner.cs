using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _minCount = 2;
    [SerializeField] private int _maxCount = 6;

    public List<Cube> SpawnCubes(Vector3 position, Vector3 scale, float chance)
    {
        const float ChildCubeScaleReductionFactor = 2f;

        int count = Random.Range(_minCount, _maxCount + 1);

        List<Cube> cubes = new();

        for (int i = 0; i < count; i++)
        {
            Cube newCube = Instantiate(_prefab, position, Quaternion.identity);

            newCube.transform.localScale = scale / ChildCubeScaleReductionFactor;
            newCube.SetSplitChance(chance);

            if (newCube.TryGetComponent(out Renderer renderer))
                renderer.material.color = Random.ColorHSV();

            cubes.Add(newCube);
        }

        return cubes;
    }

    public void DestroyCube(Cube cube)
    {
        Destroy(cube.gameObject);
    }
}