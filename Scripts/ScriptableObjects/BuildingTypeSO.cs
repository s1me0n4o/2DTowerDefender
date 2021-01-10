using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string Name;
    public Transform Prefab;
    public ResourceGeneartorData ResourceGeneratorData;
    public Sprite Sprite;
    public float MinConstructionRadios;
    public ResourceAmount[] BuildingCosts;
    public int MaxHealth;
    public float ConstructionTimerMax;
}
