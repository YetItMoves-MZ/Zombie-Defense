using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public GameObject BuildingPlanPrefab;
    public GameObject BuildingPrefab;


    public void InitializeBuild()
    {
        BuildingHandler.Instance.InitializeBuilding(BuildingPlanPrefab, BuildingPrefab);
    }
}
