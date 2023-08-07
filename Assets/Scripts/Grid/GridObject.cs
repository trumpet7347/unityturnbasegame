using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridObject
{
    private GridPosition _gridPosition;
    private GridSystem<GridObject> _gridSystem;

    public List<Unit> unitList;

    public GridObject(GridSystem<GridObject> gridSystem, GridPosition gridPositon)
    {
        _gridSystem = gridSystem;
        _gridPosition = gridPositon;

        unitList = new List<Unit>();
    }

    public override string ToString()
    {
        var unitString = "";
        foreach (var unit in unitList)
        {
            unitString += unit + "\n";
        }
        return _gridPosition.ToString() + "\n" + unitString;
    }

    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }
    
    public List<Unit> GetUnitList()
    {
        return unitList;
    }

    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit); ;
    }

    public bool HasAnyUnit()
    {
        return unitList.Count > 0;
    }

    public Unit GetUnit()
    {
        return unitList.FirstOrDefault();
    }
}
