using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorControl : MonoBehaviour
{    
    void Awake()
    {
        if (gameObject.GetComponent<Camera>() != null) gameObject.AddComponent<Interactor>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
