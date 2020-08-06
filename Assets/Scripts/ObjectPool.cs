using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _objectCount;
    [SerializeField] private List<GameObject> _pool = new List<GameObject>();

    private Camera _camera;
    

    protected void Init(GameObject prefab)
    {
        _camera = Camera.main;
        for (int i = 0; i < _objectCount; i++)
        {
            GameObject spawnObject = Instantiate(prefab, _container.transform);
            spawnObject.SetActive(false);
            _pool.Add(spawnObject);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false) ;
        return result != null;
    }

    protected void DisableObjectAbroadScreen()
    {
        foreach(GameObject item in _pool)
        {
            if (item.activeSelf == true)
            {
                Vector3 point = _camera.WorldToViewportPoint(item.transform.position);
                if (point.x < 0)
                    item.SetActive(false);
            }
        }
    }
}
