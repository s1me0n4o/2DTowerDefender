using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ListResourceTypes")]
public class ListAllResourceTypesSO : ScriptableObject
{
    public List<ResourceTypesSO> ListAllResourceTypes;
}
