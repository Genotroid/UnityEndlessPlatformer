using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    [SerializeField] private int _chance;
    [SerializeField] private int _maxCount;
    [SerializeField] private int _emptyCellCountAfter;

    public int Chance => _chance;
    public int MaxCount => _maxCount;
    public int EmptyCellCountAfter => _emptyCellCountAfter;

    private void OnValidate()
    {
        _chance = Mathf.Clamp(_chance, 1, 100);
    }
}
