using Mirror;
using UnityEngine;

namespace BetweenTime.Network
{
    public class BTCustomNetworkManager : NetworkManager
    {


        [ContextMenu("Start Client")]
        public void TestStart()
        {
            StartClient();
        }
    }
}
