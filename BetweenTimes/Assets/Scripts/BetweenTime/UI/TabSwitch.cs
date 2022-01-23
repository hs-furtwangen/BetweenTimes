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
            if(tab1 != null) tab1.SetActive(true);
            if(tab2 != null)tab2.SetActive(false);
        }
        else
        {
            if(tab1 != null)tab1.SetActive(false);
            if(tab2 != null)tab2.SetActive(true);
        }
        
    }

    public void Toggle()
    {
        _toggle = !_toggle;

        if (!_toggle)
        {
            if(tab1 != null)tab1.SetActive(true);
            if(tab2 != null)tab2.SetActive(false);
        }
        else
        {
            if(tab1 != null)tab1.SetActive(false);
            if(tab2 != null) tab2.SetActive(true);
        }
        
    }
}
