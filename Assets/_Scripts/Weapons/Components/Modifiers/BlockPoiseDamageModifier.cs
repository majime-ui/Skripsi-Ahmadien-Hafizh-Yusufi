using Majime.Combat.PoiseDamage;
using Majime.ModifierSystem;

namespace Majime.Weapons.Modifiers
{
    public class BlockPoiseDamageModifier : Modifier<PoiseDamageData>
    {
        private readonly ConditionalDelegate isBlocked;

        public BlockPoiseDamageModifier(ConditionalDelegate isBlocked)
        {
            this.isBlocked = isBlocked;
        }

        public override PoiseDamageData ModifyValue(PoiseDamageData value)
        {
            if (isBlocked(value.Source.transform, out var blockDirectionInformation))
            {
                value.SetAmount(value.Amount * (1 - blockDirectionInformation.PoiseDamageAbsorption));
            }

            return value;
        }
    }
}