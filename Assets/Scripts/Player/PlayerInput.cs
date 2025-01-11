using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private readonly string verticalAxis = "Vertical";
    private readonly string horizontalAxis = "Horizontal";
    private readonly string fireAxis = "Fire1";

    public LayerMask floorLayer;
    public Vector3 Move { get; private set; }
    public bool Fire { get; private set; }


    public Vector3 MousePosition { get; private set; }

    private void Update()
    {
        Move = new Vector3(Input.GetAxis(horizontalAxis), 0f, Input.GetAxis(verticalAxis));
        if (Move.sqrMagnitude > 1f)
        {
            Move.Normalize();
        }
        Fire = Input.GetButton(fireAxis);
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100, floorLayer))
        {
            MousePosition = hit.point;
        }
    }
}
