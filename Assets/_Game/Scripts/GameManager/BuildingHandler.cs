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

    bool isBuilding = false;
    GameObject buildingPlan;
    GameObject buildingPrefab;


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
        buildingPrefab = null;
        isBuilding = false;
        IsInBuildableLocation = false;
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
        var building = Instantiate(buildingPrefab, GetMousePosition(), transform.rotation);
        OnBuildingCancel();
    }

    public void InitializeBuilding(GameObject buildingPlanPrefab, GameObject buildingPrefab)
    {
        if (buildingPlanPrefab)
        {
            OnBuildingCancel();
        }
        IsBuilding = true;
        this.buildingPrefab = buildingPrefab;
        buildingPlan = Instantiate(buildingPlanPrefab, GetMousePosition(), transform.rotation);
    }
}
