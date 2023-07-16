using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    private List<Unit> unitList;
    private List<Unit> friendlyUnitLIst;
    private List<Unit> enemyUnitList;

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

        unitList = new List<Unit>();
        friendlyUnitLIst = new List<Unit>();
        enemyUnitList = new List<Unit>();
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
            enemyUnitList.Remove(unit);
        }
        else
        {
            friendlyUnitLIst.Remove(unit);
        }

        unitList.Remove(unit);
    }

    private void Unit_OnAnyUnitSpawned(object sender, System.EventArgs e)
    {
        Unit unit = sender as Unit;

        if (unit.IsEnemy())
        {
            enemyUnitList.Add(unit);
        }
        else
        {
            friendlyUnitLIst.Add(unit);
        }

        unitList.Add(unit);
    }

    public List<Unit> GetUnitList()
    {
        return unitList;
    }

    public List<Unit> GetFriendlyUnitList()
    {
        return friendlyUnitLIst;
    }

    public List<Unit> GetEnemyUnitList()
    {
        return enemyUnitList;
    }
}
