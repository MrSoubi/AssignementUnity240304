using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    EquipmentData head;
    EquipmentData torso;
    EquipmentData hands;
    EquipmentData legs;

    public void Equip(EquipmentData equipment)
    {
        switch (equipment.slot)
        {
            case EquipmentData.Slot.Head:
                head = equipment;
                break;
            case EquipmentData.Slot.Torso:
                torso = equipment;
                break;
            case EquipmentData.Slot.Hands:
                hands = equipment;
                break;
            case EquipmentData.Slot.Legs:
                legs = equipment;
                break;
        }
    }

    public int GetGlobalModifier()
    {
        return head.modifier + torso.modifier + hands.modifier + legs.modifier;
    }
}
