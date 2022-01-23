using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Delayer : MonoBehaviour
{

    [SerializeField] private float delayAmount = 1f;

    private Coroutine c_delayer;

    public UnityEvent EventDelayFinished;
    
    public void StartDelay()
    {
        if(c_delayer != null)
            StopCoroutine(c_delayer);

        c_delayer = StartCoroutine(DelayerCoroutine());
    }

    public void AfterDelay()
    {
        EventDelayFinished?.Invoke();
    }

    IEnumerator DelayerCoroutine()
    {
        yield return new WaitForSeconds(delayAmount);
        AfterDelay();

    }
}
