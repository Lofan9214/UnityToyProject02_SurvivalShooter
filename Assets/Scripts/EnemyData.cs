using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptables/EnemyData",fileName ="Enemy Data")]
public class EnemyData : ScriptableObject
{
    public AudioClip hurtSound;
    public AudioClip dieSound;

    public float hp = 100f;
    public float damage = 20f;
    public float speed = 2f;

    public float attackRate = 1f;
    internal AudioClip hitSound;
}
