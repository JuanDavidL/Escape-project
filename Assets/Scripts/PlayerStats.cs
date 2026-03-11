using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [Header("Estadisticas Base")]
    [SerializeField] private float baseMoveSpeed = 5f;

    private Dictionary<StatType, float> statModifiers = new Dictionary<StatType, float>();

    public float currentBaseMove { get; private set; }
    public float CurrentMoveSpeed { get; private set; }
    public float CurrentDashForce { get; private set; }

    private void Awake()
    {
        CurrentMoveSpeed = baseMoveSpeed;
        currentBaseMove = baseMoveSpeed;
    }

    public void ApplyPowerUp(PowerUpData powerUp)
    {
        if (powerUp.targetStat == StatType.InventorySlots)
        {
            // Buscamos el InventoryManager
            if (InventoryManager.Instance != null)
            {
                Debug.Log("vamo bien");
                InventoryManager.Instance.InvokeOnGrowth();
            }
            return;
        }
        if (!statModifiers.ContainsKey(powerUp.targetStat))
            statModifiers[powerUp.targetStat] = 0f;

        statModifiers[powerUp.targetStat] += powerUp.amout;
        UpdateCalculateStats();
    }

    private void UpdateCalculateStats()
    {
        Debug.Log("Update");
        if (statModifiers.TryGetValue(StatType.MoveSpeed, out float boost))
        {
            Debug.Log("boost");
            CurrentMoveSpeed = baseMoveSpeed + boost;
        }
        else
        {
            Debug.Log("base");
            CurrentMoveSpeed = baseMoveSpeed;
        }

        if (statModifiers.TryGetValue(StatType.DashForce, out float dash))
        {
            Debug.Log("dash" + dash);
            CurrentDashForce = dash;
        }
    }




}
