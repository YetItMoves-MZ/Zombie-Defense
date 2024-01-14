using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBuildingStats : Stats
{
    Build savedBuild;

    private void Start()
    {
        OnDeath += OnAllyBuildingDeath;
    }

    private void OnAllyBuildingDeath()
    {
        savedBuild.Cost = (int)(savedBuild.Cost / 1.5f);
    }

    public void SaveBuild(Build build)
    {
        savedBuild = build;
    }


}
