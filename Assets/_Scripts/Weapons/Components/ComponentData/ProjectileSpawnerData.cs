namespace Majime.Weapons.Components
{
    public class ProjectileSpawnerData : ComponentData<AttackProjectileSpawner>
    {
        protected override void SetComponentDependency()
        {
            ComponentDependency = typeof(ProjectileSpawner);
        }
    }
}