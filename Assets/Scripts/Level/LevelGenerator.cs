using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] LevelObject[] _templates;
    [SerializeField] Ground _groundTemplate;
    [SerializeField] private Transform _player;
    [SerializeField] private float _viewRadius;
    [SerializeField] private float _cellSize;
    [SerializeField] private Transform _container;

    private List<Vector3> _collisionMatrix = new List<Vector3>();
    private float _startGenerateObjectsTime = 2f;
    private float _passedTime = 0f;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Update()
    {
        _passedTime += Time.deltaTime;
        Fillradius(_player.position, _viewRadius);
    }

    private void Fillradius(Vector3 center, float viewRadius)
    {
        var cellCountOnAxis = (int)(viewRadius / _cellSize);

        var groundAreaCenter = WorldToGridPosition(center);
        var objectAreaCenter = WorldToGridPosition(transform.position);

        for (int x = -cellCountOnAxis; x < cellCountOnAxis; x++)
        {
            if(_passedTime > _startGenerateObjectsTime)
                TryCreateObject(objectAreaCenter + new Vector3Int(x, 0, 0));
            TryCreateGround(groundAreaCenter + new Vector3Int(x, 0, 0));
        }
    }

    private void TryCreateObject(Vector3Int gridPosition)
    {
        gridPosition.y = 1;
        if (_collisionMatrix.Contains(gridPosition))
            return;
        else
            _collisionMatrix.Add(gridPosition);

        var template = GetRandomTemplate();
        if (template == null)
            return;

        int maxCount = Random.Range(1, template.MaxCount);
        for(int i = 0; i < maxCount; i++)
        {
            gridPosition.x += i;
            _collisionMatrix.Add(gridPosition);
            Vector3 position = GridToWorldPosition(gridPosition);
            Instantiate(template, position, Quaternion.identity, _container);
        }
        for(int i = 0; i < template.EmptyCellCountAfter; i++)
        {
            gridPosition.x += 1;
            _collisionMatrix.Add(gridPosition);
        } 
    }

    private void TryCreateGround(Vector3Int gridPosition)
    {
        gridPosition.y = 0;
        if (_collisionMatrix.Contains(gridPosition))
            return;
        else
            _collisionMatrix.Add(gridPosition);

        Vector3 groundPosition = new Vector3(gridPosition.x, 0, 0);
        groundPosition = GridToWorldPosition(groundPosition);
        Instantiate(_groundTemplate, groundPosition, Quaternion.identity, _container);
    }

    private LevelObject GetRandomTemplate()
    {
        foreach(LevelObject template in _templates)
        {
            if (template.Chance > Random.Range(0, 100))
                return template;
        }

        return null;
    }

    private Vector3 GridToWorldPosition(Vector3 gridPosition)
    {
        return new Vector3(
           gridPosition.x * _cellSize,
           gridPosition.y * _cellSize,
           gridPosition.z * _cellSize);
    }

    private Vector3Int WorldToGridPosition(Vector3 worldPosition)
    {
        return new Vector3Int(
            (int)(worldPosition.x / _cellSize),
            (int)(worldPosition.y / _cellSize),
            (int)(worldPosition.z / _cellSize));
    }
}
