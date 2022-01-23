using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabSwitch : MonoBehaviour
{
    [SerializeField] private GameObject tab1;
    [SerializeField] private GameObject tab2;
    private bool _toggle;

    private void Awake()
    {
        if (!_toggle)
        {
            tab1.SetActive(true);
            tab2.SetActive(false);
        }
        else
        {
            tab1.SetActive(false);
            tab2.SetActive(true);
        }
        
    }

    public void Toggle()
    {
        _toggle = !_toggle;

        if (!_toggle)
        {
            tab1.SetActive(true);
            tab2.SetActive(false);
        }
        else
        {
            tab1.SetActive(false);
            tab2.SetActive(true);
        }
        
    }
}
