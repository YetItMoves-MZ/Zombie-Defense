using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUpgrade : MonoBehaviour
{
    [SerializeField] BuildingUpgrades.Type myType;
    Stats myStats;

    // Start is called before the first frame update
    void Start()
    {
        myStats = GetComponent<Stats>();
        myStats.OnDeath += OnDeath;
        BuildingUpgrades.Instance.GetUpgradeByType(myType).UpgradeValue++;
    }

    private void OnDeath()
    {
        BuildingUpgrades.Instance.GetUpgradeByType(myType).UpgradeValue--;
    }
}
