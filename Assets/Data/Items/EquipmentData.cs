using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentData", menuName = "Data/Equipment")]
public class EquipmentData : ScriptableObject
{
    public enum Slot
    {
        Head,
        Torso,
        Hands,
        Legs
    }

    public string equipmentName;
    public int modifier;
    public Slot slot;
}
