using System;
using System.Collections.Generic;
using UnityEngine;

namespace Majime.ProjectileSystem.DataPackages
{
    [Serializable]
    public class TargetsDataPackage : ProjectileDataPackage
    {
        // The list of transforms that the Targeter weapon component detected
        public List<Transform> targets;
    }
}