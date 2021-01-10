using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class allows us to create multiple instances of a prefabs and reuse them.
/// It allows us to avoid the cost of destroying and creating objects.
/// </summary>
namespace Assets.ProjectFiles.Scripts.Pool
{
    public class Pooler
    {
        protected Stack<GameObject> _freeInstances;
        protected GameObject _originalGO;

        public Pooler(GameObject orignalGO, int initialSize)
        {
            _originalGO = orignalGO;
            _freeInstances = new Stack<GameObject>(initialSize);

            for (int i = 0; i < initialSize; i++)
            {
                var go = Object.Instantiate(orignalGO);
                go.SetActive(false);
                _freeInstances.Push(go);
            }
        }

        public GameObject Get(Vector3 position, Quaternion rotation)
        {
            var result = _freeInstances.Count > 0 ? _freeInstances.Pop() : Object.Instantiate(_originalGO);

            result.SetActive(true);
            result.transform.position = position;
            result.transform.rotation = rotation;
            
            return result;
        }

        public void Realese(GameObject go)
        {
            go.transform.SetParent(null);
            go.SetActive(false);
            _freeInstances.Push(go);
        }
    }
}
