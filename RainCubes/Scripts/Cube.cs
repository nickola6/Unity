using System;
using System.Collections;
using UnityEngine;

namespace RainCubes
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Renderer))]

    public class Cube : MonoBehaviour
    {
        private const float MinLifeTime = 2f;
        private const float MaxLifeTime = 5f;

        protected Rigidbody _rigidbody;
        protected Renderer _renderer;
        protected bool _isActive;

        public event Action<Cube> LifeEnded;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _renderer = GetComponent<Renderer>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_isActive)
                return;

            if (collision.gameObject.TryGetComponent(out Platform _))
                Activate();
        }

        protected virtual void Activate()
        {
            _isActive = true;

            _renderer.material.color = UnityEngine.Random.ColorHSV();

            float lifeTime = UnityEngine.Random.Range(MinLifeTime, MaxLifeTime + 1);

            StartCoroutine(LiveRoutine(lifeTime));
        }

        private IEnumerator LiveRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);

            LifeEnded?.Invoke(this);
        }

        public virtual void ResetState(Color baseColor)
        {
            _isActive = false;
            _renderer.material.color = baseColor;

            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}