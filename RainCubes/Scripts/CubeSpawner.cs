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
            float delay = 0f;

            InvokeRepeating(nameof(Spawn), delay, _repeatRate);
        }

        private void Spawn()
        {
            _pool.Get();
        }

        private void OnGet(Cube cube)
        {
            const float MinPossiblePosition = 0f;

            float positionX = _mainPlatform.transform.position.x + Random.Range(MinPossiblePosition, _mainPlatform.terrainData.size.x + 1);
            float positionY = _mainPlatform.transform.position.y + _spawnHeight;
            float positionZ = _mainPlatform.transform.position.z + Random.Range(MinPossiblePosition, _mainPlatform.terrainData.size.z + 1);

            Vector3 position = new Vector3(positionX, positionY, positionZ);

            cube.transform.position = position;
            cube.ResetState(_pool, _baseColor);

            cube.gameObject.SetActive(true);
        }

        private void OnRelease(Cube cube)
        {
            cube.gameObject.SetActive(false);
        }
    }
}