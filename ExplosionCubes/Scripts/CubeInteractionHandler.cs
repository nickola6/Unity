using UnityEngine;
using System.Collections;

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
        const float MinRandomValue = 0f;
        const float MaxRandomValue = 100f;
        const float DestroyDelay = 0.05f;
        const int SplitChanceReductionFactor = 2;

        float randomValue = Random.Range(MinRandomValue, MaxRandomValue + 1);
        float splitChance = cube.SplitChance;

        if (randomValue <= splitChance)
        {
            float childChance = splitChance / SplitChanceReductionFactor;
            _spawner.SpawnCubes(cube.transform.position, cube.transform.localScale, childChance);
        }
        else
        {
            float size = cube.transform.localScale.x;
            _explosion.ApplyExplosion(cube.transform.position, size);

            StartCoroutine(DestroyWithDelay(cube, DestroyDelay));
            
            return;
        }

        _spawner.DestroyCube(cube);
    }

    private IEnumerator DestroyWithDelay(Cube cube, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (cube != null)
            _spawner.DestroyCube(cube);
    }
}