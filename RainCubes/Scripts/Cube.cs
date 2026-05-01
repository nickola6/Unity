using System;
using System.Collections;
using UnityEngine;

namespace RainCubes
{
    public class Cube : MonoBehaviour
    {
        private const float MinLifeTime = 2f;
        private const float MaxLifeTime = 5f;

        protected Renderer _renderer;
        protected bool _activated;

        public event Action<Cube> LifeEnded;

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

        protected virtual void Activate()
        {
            _activated = true;

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
            _activated = false;
            _renderer.material.color = baseColor;

            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }
}