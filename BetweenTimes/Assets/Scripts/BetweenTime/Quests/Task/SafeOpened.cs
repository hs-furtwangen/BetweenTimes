using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeOpened : Task
{
    public int safenum;
    private int pin;

    private void OnEnable()
    {
        safenum = 0;
        pin = 4433;

        reset = false;
    }
    public void Solve()
    {
        if (safenum == pin)
        {
            TaskComplete();
        }
    }



    void Update()
    {
        Solve();
        Restart();
    }
}
