using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "new Boss", menuName = "Boss")]
public class BossData : ScriptableObject
{
    [Header("General")]
    public string BossName;

    public float Health;
    public float Damage;
    public float Speed;
    public float Acceleration;


    [Header("Resistance")]
    public bool FireResistance;
    public bool IceResistance;
    public bool WindResistance;

    [Header("Weakness")]
    public bool FireWeak;
    public bool IceWeak;
    public bool WindWeak;

    [Header("Skills")]
    public int SkillTime;
    public bool RainOfAbundance;
    public bool JumpHigh;
}

