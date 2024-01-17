using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build : MonoBehaviour
{
    public GameObject BuildingPlanPrefab;
    public GameObject BuildingPrefab;
    public int Cost;
    [SerializeField] TMPro.TMP_Text textCost;

    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {
        button.interactable = MoneyManager.Instance.GetMoney() >= Cost;
        textCost.text = Cost + " $";
        if (button.interactable)
            textCost.color = Color.green;
        else
            textCost.color = Color.red;
    }

    public void InitializeBuild()
    {
        BuildingHandler.Instance.InitializeBuilding(this);
    }
}
