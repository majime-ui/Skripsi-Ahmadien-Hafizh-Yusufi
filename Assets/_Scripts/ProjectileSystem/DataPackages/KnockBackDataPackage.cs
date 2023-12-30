using System;
using UnityEngine;

namespace Majime.ProjectileSystem.DataPackages
{
    [Serializable]
    public class KnockBackDataPackage : ProjectileDataPackage
    {
        [field: SerializeField] public float Strength;
        [field: SerializeField] public Vector2 Angle;
    }
}