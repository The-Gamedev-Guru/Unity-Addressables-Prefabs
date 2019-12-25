using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TheGamedevGuru
{
public class Level_3_HandleAddressablesPrefabLifecycleReferenceCounting : MonoBehaviour
{
    [SerializeField] private Transform spawnAnchor = null;
    [SerializeField] private float separation = 1f;
    [SerializeField] private int instanceCount = 10;
    [SerializeField] private AssetReference prefabReference = null;

    private readonly List<GameObject> _instances = new List<GameObject>();

    public void HandleLifecycle()
    {
        var hasSpawnedInstances = _instances.Count > 0; 
        if (hasSpawnedInstances)
        {
            Despawn();
        }
        else
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        for (var i = 0; i < instanceCount; i++)
        {
            var asyncOperationHandle = prefabReference.InstantiateAsync(spawnAnchor.position + i *separation * Vector3.right, spawnAnchor.rotation);
            asyncOperationHandle.Completed += handle => { _instances.Add(handle.Result); };
        }
    }

    private void Despawn()
    {
        foreach (var instance in _instances)
        {
            Addressables.ReleaseInstance(instance);
        }
        _instances.Clear();
    }
}
}
