using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    public delegate void ParameterChangedEvent(int newValue);
    public ParameterChangedEvent UpgradeValueChanged;
    [HideInInspector]
    public int UpgradeValue
    {
        get
        {
            return upgradeValue;
        }
        set
        {
            upgradeValue = value;
            UpgradeValueChanged?.Invoke(value);
        }
    }
    private int upgradeValue;

    public Upgrade()
    {
        upgradeValue = 0;
    }
}
