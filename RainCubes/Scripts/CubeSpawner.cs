using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace RainCubes
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private Cube _prefab;
        [SerializeField] private Terrain _mainPlatform;
        [SerializeField] private float _spawnHeight = 10f;
        [SerializeField] private float _repeatRate = 0.5f;
        [SerializeField] private int _poolCapacity = 10;
        [SerializeField] private int _poolMaxSize = 20;

        private ObjectPool<Cube> _pool;
        private Color _baseColor;

        private void Awake()
        {
            _baseColor = Random.ColorHSV();

            _pool = new ObjectPool<Cube>
            (
                createFunc: () => Instantiate(_prefab),
                actionOnGet: OnGet,
                actionOnRelease: OnRelease,
                actionOnDestroy: cube => Destroy(cube.gameObject),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize
            );
        }

        private void Start()
        {
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            while (isActiveAndEnabled)
            {
                yield return new WaitForSeconds(_repeatRate);

                _pool.Get();
            }
        }

        private void OnGet(Cube cube)
        {
            cube.ResetState(_baseColor);

            cube.LifeEnded += ReturnToPool;

            Vector3 position = GetRandomPosition();

            cube.transform.position = position;
            cube.gameObject.SetActive(true);
        }

        private void OnRelease(Cube cube)
        {
            cube.LifeEnded -= ReturnToPool;
            cube.gameObject.SetActive(false);
        }

        private void ReturnToPool(Cube cube)
        {
            _pool.Release(cube);
        }

        private Vector3 GetRandomPosition()
        {
            const float MinPossiblePosition = 0f;

            Vector3 position = _mainPlatform.transform.position;
            Vector3 size = _mainPlatform.terrainData.size;

            float positionX = position.x + Random.Range(MinPossiblePosition, size.x);
            float positionY = position.y + _spawnHeight;
            float positionZ = position.z + Random.Range(MinPossiblePosition, size.z);

            return new Vector3(positionX, positionY, positionZ);
        }
    }
}