using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BetweenTime.Network.Player;

namespace BetweenTime.Player
{
    public class BTPlayerMovement : MonoBehaviour
    {
        readonly float sqrt2 = Mathf.Sqrt(2);
        private Camera camera;
        private BTPlayerInput playerInput;

        // Start is called before the first frame update
        void Start()
        {
            camera = GetComponentInChildren<Camera>();            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void OnApplicationFocus(bool focus)
        {
            if (playerInput == null) playerInput = GetComponent<BTPlayerInput>();
            if (focus)
            {
                Cursor.visible = false;
                playerInput.EventOnAxis.AddListener(Move);
                playerInput.EventOnMouse.AddListener(Look);
            }
            else
            {
                playerInput.EventOnAxis.RemoveListener(Move);
                playerInput.EventOnMouse.RemoveListener(Look);
            }
        }

        void Move(float x, float y)
        {
            if (x != 0 && y != 0)
            {
                x /= sqrt2;
                y /= sqrt2;
            }
            var rigidbody = GetComponent<Rigidbody>();
            transform.Translate(Time.deltaTime * x, 0, Time.deltaTime * y);
        }

        void Look(float x, float y)
        {
            camera.transform.Rotate(-y, 0, 0);
            transform.Rotate(0, x, 0);
        }
    }
}
