using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }

    public event EventHandler OnAnyUnitMoveGrid;

    [SerializeField] private Transform _gridDebugObjectPrefab;
    private GridSystem<GridObject> _gridSystem;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one LevelGrid!");
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _gridSystem = new GridSystem<GridObject>(10, 10, 2, (GridSystem<GridObject> g, GridPosition gridPosition) => new GridObject(g, gridPosition));
        _gridSystem.CreateDebugObjects(_gridDebugObjectPrefab);
    }

    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        var gridObject = _gridSystem.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
    }

    public List<Unit> GetUnitListAtGridPosition(GridPosition gridPostiton)
    {
        var gridObject = _gridSystem.GetGridObject(gridPostiton);
        return gridObject.GetUnitList(); ;
    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        var gridObject = _gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveUnitAtGridPosition(fromGridPosition, unit);
        AddUnitAtGridPosition(toGridPosition, unit);

        OnAnyUnitMoveGrid?.Invoke(this, EventArgs.Empty);
    }

    public bool HasAnyUnitOnGridPostion(GridPosition gridPosition)
    {
        var gridObject = _gridSystem.GetGridObject(gridPosition);
        return gridObject.HasAnyUnit();
    }

    public Unit GetUnitAtGridPostion(GridPosition gridPosition)
    {
        var gridObject = _gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnit();
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => _gridSystem.GetGridPosition(worldPosition);

    public bool IsValidGridPosition(GridPosition gridPosition) => _gridSystem.IsValidGridPosition(gridPosition);

    public Vector3 GetWorldPosition(GridPosition gridPosition) => _gridSystem.GetWorldPosition(gridPosition);

    public int GetWidth() => _gridSystem.GetWidth();
    public int GetHeight() => _gridSystem.GetHeight();
}
