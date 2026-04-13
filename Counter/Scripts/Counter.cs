using System;
using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public event Action<int> ValueChanged;

    [SerializeField] private float _delay = 0.5f;
    private int _number = 0;
    private bool _isRun = false;
    private Coroutine _coroutine;

    public int Number => _number;
    public bool IsRun() => _isRun;

    public void ToStart()
    {
        if (_isRun == false)
        {
            _isRun = true;
            _coroutine = StartCoroutine(CountRoutine());
        }
    }

    public void ToStop()
    {
        if (_isRun)
        {
            _isRun = false;

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
    }

    private IEnumerator CountRoutine()
    {
        while (_isRun)
        {
            yield return new WaitForSeconds(_delay);

            _number++;
            ValueChanged?.Invoke(_number);
        }
    }
}