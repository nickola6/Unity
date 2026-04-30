using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace RainCubes
{
    public class Cube : MonoBehaviour
    {
        private const float MinLifeTime = 2f;
        private const float MaxLifeTime = 5f;

        private Renderer _renderer;
        private ObjectPool<Cube> _pool;
        private bool _activated;

        public void ResetState(ObjectPool<Cube> pool, Color baseColor)
        {
            _pool = pool;
            _activated = false;

            _renderer.material.color = baseColor;

            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_activated)
                return;

            if (collision.gameObject.TryGetComponent(out Platform _))
                Activate();
        }

        private void Activate()
        {
            _activated = true;

            _renderer.material.color = Random.ColorHSV();

            float lifeTime = Random.Range(MinLifeTime, MaxLifeTime + 1);

            StartCoroutine(LifeRoutine(lifeTime));
        }

        private IEnumerator LifeRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);

            _pool.Release(this);
        }
    }
}