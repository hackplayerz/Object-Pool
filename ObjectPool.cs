using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T>  where T : MonoBehaviour
{
    #region Variable

    private readonly List<T> _pools = new List<T>(); // Pool
    private int _count; // Count of created object
    private readonly T _prefab; // To instantiate prefab
    private Transform _parent;

    #endregion

    public ObjectPool(T prefab)
    {
        _prefab = prefab;
    }
    
    /// <summary>
    /// Initialized pool
    /// </summary>
    /// <param name="container">Parent of spawn transform</param>
    public void InitPool(Transform container = null)
    {
        T go = Object.Instantiate(_prefab).GetComponent<T>(); // Instance
        // Set Parent transform
        if (container != null)
        {
            _parent = container;
            go.transform.SetParent(container);
        }
        _pools.Add(go); // Push
        go.gameObject.SetActive(false);
    }
    
    /// <summary>
    /// Create Pooled object
    /// </summary>
    /// <param name="position">Spawn Position</param>
    public void Create(Vector2 position)
    {
        // No more pool memory exception
        if (_count > _pools.Count - 1)
        {
            // If all pool object is active
            if (_pools[0].gameObject.activeSelf)
            {
                InitPool(_parent);
            }
            else
            {
                _count = 0;
            }
        }
        // Spawn
        _pools[_count].gameObject.SetActive(true);
        _pools[_count].transform.position = position;
        _count++;
    }
}

