using System;
using UnityEngine;

namespace Majime.ProjectileSystem.DataPackages
{
    [Serializable]
    public class DrawModifierDataPackage : ProjectileDataPackage
    {
        public float DrawPercentage
        {
            get => drawPercentage;
            set => drawPercentage = Mathf.Clamp01(value);
        }

        private float drawPercentage;
    }
}