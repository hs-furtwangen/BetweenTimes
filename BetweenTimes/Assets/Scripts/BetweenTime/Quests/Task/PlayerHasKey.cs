using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHasKey : Tasks
{
    public string key = "Key";
    public string playerHand;

    public void Solve()
    {
        if (key == playerHand)
        {
            TaskComplete();
        }
    }


    private void Update()
    {
        Solve();
    }
}
