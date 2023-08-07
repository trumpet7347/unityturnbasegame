using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    private List<Unit> _unitList;
    private List<Unit> _friendlyUnitLIst;
    private List<Unit> _enemyUnitList;

    public static UnitManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one UnitManager!");
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _unitList = new List<Unit>();
        _friendlyUnitLIst = new List<Unit>();
        _enemyUnitList = new List<Unit>();
    }
    private void Start()
    {
        Unit.OnAnyUnitSpawned += Unit_OnAnyUnitSpawned;
        Unit.OnAnyUnitDead += Unit_OnAnyUnitDead;
    }

    private void Unit_OnAnyUnitDead(object sender, System.EventArgs e)
    {
        Unit unit = sender as Unit;

        if (unit.IsEnemy())
        {
            _enemyUnitList.Remove(unit);
        }
        else
        {
            _friendlyUnitLIst.Remove(unit);
        }

        _unitList.Remove(unit);
    }

    private void Unit_OnAnyUnitSpawned(object sender, System.EventArgs e)
    {
        Unit unit = sender as Unit;

        if (unit.IsEnemy())
        {
            _enemyUnitList.Add(unit);
        }
        else
        {
            _friendlyUnitLIst.Add(unit);
        }

        _unitList.Add(unit);
    }

    public List<Unit> GetUnitList()
    {
        return _unitList;
    }

    public List<Unit> GetFriendlyUnitList()
    {
        return _friendlyUnitLIst;
    }

    public List<Unit> GetEnemyUnitList()
    {
        return _enemyUnitList;
    }
}
