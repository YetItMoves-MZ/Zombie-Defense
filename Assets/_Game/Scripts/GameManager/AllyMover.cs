using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyMover : MonoBehaviour
{
    public static AllyMover Instance { get; private set; }
    public Transform MovementIndicator;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetButtonDown("CancelBuild") && !BuildingHandler.Instance.IsBuilding)
        {
            OnClick();
        }
    }

    void OnClick()
    {
        Vector3 mousePos = BuildingHandler.Instance.GetMousePosition();
        MovementIndicator.position = mousePos;
    }
}
