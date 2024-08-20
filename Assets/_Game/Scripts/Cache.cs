using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache : Singleton<Cache>
{
    private Dictionary<Collider, object> cache = new Dictionary<Collider, object>();

    public T GetCachedComponent<T>(Collider collider) where T : Component
    {
        if (cache.TryGetValue(collider, out object cachedComponent))
        {
            return cachedComponent as T;
        }
        else
        {
            T component = collider.GetComponent<T>();
            if (component != null)
            {
                cache.Add(collider, component);
            }
            return component;
        }
    }
}
