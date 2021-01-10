using Assets.ProjectFiles.Scripts.Pool;

namespace Assets.ProjectFiles.Scripts.Models
{
    public static class PoolFactory
    {
        public static Pooler ArrowPool { get; set; }
        public static Pooler BuildingConstructionPool { get; set; }
        public static Pooler BuildingDestroyParticles { get; set; }
        public static Pooler DieParticles { get; set; }
        public static Pooler BuildingPlacedParticles { get; set; }
    }
}
