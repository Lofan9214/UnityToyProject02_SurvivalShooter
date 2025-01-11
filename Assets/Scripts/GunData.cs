using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/GunData",fileName = "Gun Data")]
public class GunData : ScriptableObject
{
    public AudioClip fireSound;

    public float damage = 25f;

    public float fireRate = 0.12f;
}
