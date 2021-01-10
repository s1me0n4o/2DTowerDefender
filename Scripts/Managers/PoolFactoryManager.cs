using Assets.ProjectFiles.Scripts.Models;
using Assets.ProjectFiles.Scripts.Pool;
using UnityEngine;

public class PoolFactoryManager : MonoBehaviour
{
    void Start()
    {
        var pfArrowProjectile = Resources.Load<GameObject>("pfArrowProjectile");
        PoolFactory.ArrowPool = new Pooler(pfArrowProjectile, 100);

        var pfConstruction = Resources.Load<GameObject>("pfBuildingConstruction");
        PoolFactory.BuildingConstructionPool = new Pooler(pfConstruction, 10);

        var buildingDestroyedParticles = Resources.Load<GameObject>("pfBuildingDestroyedParticles");
        PoolFactory.BuildingDestroyParticles = new Pooler(buildingDestroyedParticles, 20);

        var dieParticles = Resources.Load<GameObject>("pfEnemyDieParticles");
        PoolFactory.DieParticles = new Pooler(dieParticles, 40);

        var buildingPlacedParticles = Resources.Load<GameObject>("pfBuildingPlacedParticles");
        PoolFactory.BuildingPlacedParticles = new Pooler(buildingPlacedParticles, 50);
    }
}