using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun;

    private PlayerInput input;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if(input.Fire)
        {
            gun.Fire();
        }
    }
}
