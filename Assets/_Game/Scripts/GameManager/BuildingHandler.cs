using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandler : MonoBehaviour
{
    public static BuildingHandler Instance { get; private set; }
    public bool IsBuilding
    {
        get
        {
            return isBuilding;
        }
        set
        {
            isBuilding = value;
            if (!value)
                OnBuildingCancel();
        }
    }
    [HideInInspector] public bool IsInBuildableLocation;
    public float MaxDistanceFromBase;

    Build building;
    bool isBuilding = false;
    GameObject buildingPlan;


    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsBuilding)
            return;
        OnMouseFollow();
        if (Input.GetButtonDown("CancelBuild"))
        {
            IsBuilding = false;
        }
        if (Input.GetButtonDown("Build"))
        {
            if (!IsInBuildableLocation)
                return;
            OnSuccessfulBuild();
        }
    }

    void OnBuildingCancel()
    {
        Destroy(buildingPlan);
        buildingPlan = null;
        isBuilding = false;
        IsInBuildableLocation = false;
        building = null;
    }

    void OnMouseFollow()
    {
        Vector3 mousePosition = GetMousePosition();
        buildingPlan.transform.position = mousePosition;
    }

    public Vector3 GetMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(transform.up, transform.position);
        if (plane.Raycast(ray, out var enterDistance))
        {
            var pointInWorld = ray.origin + ray.direction * enterDistance;
            return pointInWorld;
        }
        return Vector3.zero;
    }
    void OnSuccessfulBuild()
    {
        if (MoneyManager.Instance.Purchase(building.Cost))
        {
            GameObject newBuilding = Instantiate(building.BuildingPrefab, GetMousePosition(), transform.rotation);

            // adjust new costs for buildings of the same kind (if it dies reduce the cost)
            building.Cost = (int)(1.5f * building.Cost);
            newBuilding.GetComponent<AllyBuildingStats>().SaveBuild(building);

            OnBuildingCancel();
        }
    }

    public void InitializeBuilding(Build newBuilding)
    {
        if (buildingPlan)
        {
            OnBuildingCancel();
        }
        building = newBuilding;
        IsBuilding = true;
        buildingPlan = Instantiate(building.BuildingPlanPrefab, GetMousePosition(), transform.rotation);
    }
}
