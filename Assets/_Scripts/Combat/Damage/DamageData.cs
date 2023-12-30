using UnityEngine;

namespace Majime.Combat.Damage
{
    public class DamageData
    {
        public float Amount { get; private set; }
        public GameObject Source { get; private set; }

        public DamageData(float amount, GameObject source)
        {
            Amount = amount;
            Source = source;
        }

        public void SetAmount(float amount)
        {
            Amount = amount;
        }
    }
}

