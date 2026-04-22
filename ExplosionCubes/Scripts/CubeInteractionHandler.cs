using System.Collections.Generic;
using UnityEngine;

public class CubeInteractionHandler : MonoBehaviour
{
    [SerializeField] private Raycaster _raycaster;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Explosion _explosion;

    private void OnEnable()
    {
        _raycaster.OnCubeHit += HandleCubeInteraction;
    }

    private void OnDisable()
    {
        _raycaster.OnCubeHit -= HandleCubeInteraction;
    }

    private void HandleCubeInteraction(Cube cube)
    {
        const float minRandomValue = 0f;
        const float maxRandomValue = 100f;
        const float SplitChanceReductionFactor = 2f;

        float randomValue = Random.Range(minRandomValue, maxRandomValue + 1);
        float childChance = cube.SplitChance / SplitChanceReductionFactor;

        if (randomValue <= cube.SplitChance)
            _spawner.SpawnCubes(cube.transform.position, cube.transform.localScale, childChance);

        _explosion.ApplyExplosion(cube.transform.position);
        _spawner.DestroyCube(cube);
    }
}