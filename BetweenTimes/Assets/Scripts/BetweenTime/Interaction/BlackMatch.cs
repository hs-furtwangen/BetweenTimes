using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMatch : Collectable
{
    // Start is called before the first frame update
    void Start()
    {
        EventUse.AddListener(Throw);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Throw(Interactor interactor)
    {

    }
}
