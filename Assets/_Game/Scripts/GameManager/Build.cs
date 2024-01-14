using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build : MonoBehaviour
{
    public GameObject BuildingPlanPrefab;
    public GameObject BuildingPrefab;
    public int Cost;

    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {
        button.interactable = MoneyManager.Instance.GetMoney() >= Cost;
    }

    public void InitializeBuild()
    {
        BuildingHandler.Instance.InitializeBuilding(this);
    }
}
