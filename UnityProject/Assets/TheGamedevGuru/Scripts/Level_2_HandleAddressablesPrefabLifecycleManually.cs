using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TheGamedevGuru
{
public class Level_2_HandleAddressablesPrefabLifecycleManually : MonoBehaviour
{
    [SerializeField] private Transform spawnAnchor = null;
    [SerializeField] private float separation = 1f;
    [SerializeField] private int instanceCount = 10;
    [SerializeField] private AssetReference prefabReference = null;
    
    private AsyncOperationHandle<GameObject> _asyncOperationHandle;
    List<GameObject> _instances = new List<GameObject>();

    public void HandleLifecycle()
    {
        var hasSpawnedInstances = _asyncOperationHandle.IsValid(); 
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
        _asyncOperationHandle = prefabReference.LoadAssetAsync<GameObject>();
        _asyncOperationHandle.Completed += handle =>
        {
            var prefab = handle.Result;
            for (var i = 0; i < instanceCount; i++)
            {
                var newGameObject = Instantiate(prefab, spawnAnchor.position + i *separation * Vector3.right, spawnAnchor.rotation);
                _instances.Add(newGameObject);
            }
        };
    }

    private void Despawn()
    {
        foreach (var instance in _instances)
        {
            Destroy(instance);
        }

        _instances.Clear();
        Addressables.Release(_asyncOperationHandle);
    }
}
}
