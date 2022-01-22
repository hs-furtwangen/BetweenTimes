using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BetweenTime.Network.Player;

namespace BetweenTime.Player {
    public class BTPlayerMovement : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var playerInput = GetComponent<BTPlayerInput>();
            playerInput.EventOnHorizontal.AddListener(value => {
                transform.Translate(Vector3.right * value);
            });
            playerInput.EventOnVertical.AddListener(value => {
                transform.Translate(Vector3.forward * value);
            });
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
