using UnityEngine;

namespace StarterAssets.BetweenTime.Utils
{
    [System.Serializable]
    public class Tuple<T,G> : MonoBehaviour
    {
        public T Item1;
        public G Item2;
        
        public Tuple(){}

        public Tuple(T Item1, G Item2)
        {
            this.Item1 = Item1;
            this.Item2 = Item2;
        }
    }
}