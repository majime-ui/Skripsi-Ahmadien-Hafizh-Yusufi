using Majime.Weapons.Components;
using UnityEngine;

namespace Majime.Weapons.Modifiers
{
    public delegate bool ConditionalDelegate(Transform source, out DirectionalInformation directionalInformation);
}