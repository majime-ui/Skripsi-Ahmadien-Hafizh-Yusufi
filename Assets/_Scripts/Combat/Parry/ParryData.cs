using UnityEngine;

namespace Majime.Combat.Parry
{
    public class ParryData : MonoBehaviour
    {
        public GameObject Source { get; private set; }

        public ParryData(GameObject source)
        {
            Source = source;
        }
    }
}

