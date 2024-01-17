using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUpgrades : MonoBehaviour
{
    public enum Type
    {
        SoldierCount,
        Soldier,
        Turret
    }
    public static BuildingUpgrades Instance { get; private set; }

    public delegate void ParameterChangedEvent(int newValue);

    public Upgrade SoldierCount;
    public Upgrade Soldier;
    public Upgrade Turret;

    private void Awake()
    {
        Instance = this;

        SoldierCount = new Upgrade();
        Soldier = new Upgrade();
        Turret = new Upgrade();
    }

    public Upgrade GetUpgradeByType(Type type)
    {
        switch (type)
        {
            case Type.SoldierCount:
                return SoldierCount;
            case Type.Soldier:
                return Soldier;
            case Type.Turret:
                return Turret;
        }
        return null;
    }
}
