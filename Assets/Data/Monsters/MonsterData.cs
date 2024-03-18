using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MonsterData", menuName = "Data/Monster")]
public class MonsterData : ScriptableObject
{
    public string monsterName;
    public int health;
    public int baseDamage;
}
