using UnityEngine;

public enum StatType { MoveSpeed, DashForce, InventorySlots, AttackSpeed, VisionRange }

[CreateAssetMenu(fileName = "PowerUpData", menuName = "Game/PowerUpData")]
public class PowerUpData : ScriptableObject
{
    public StatType targetStat;
    public float amout;
    // public bool isPorcentual; //Pregunta si lo manejamos por porcentaje o aumento fijo?
}
