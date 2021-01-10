using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingTypesList")]
public class ListAllBuildingTypesSO : ScriptableObject
{
    public List<BuildingTypeSO> BuildingTypeList;
}
