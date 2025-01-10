using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptables/EnemyData",fileName ="Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float hp = 100f;
    public float damage = 20f;
    public float speed = 2f;
}
