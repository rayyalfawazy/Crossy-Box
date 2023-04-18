using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField,Range(0,1)] float durasi;
    [SerializeField,Range(0, 1)] float jumpHeight;

    void Update()
    {
        if (DOTween.IsTweening(transform))
        {
            return;
        }

        Vector3 direction = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction += Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction += Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction += Vector3.left;
        }

        if (direction == Vector3.zero)
        {
            return;
        }
        Move(direction);

    }
    public void Move(Vector3 direction)
    {
        transform.DOMoveZ(transform.position.z + direction.z, durasi);
        transform.DOMoveX(transform.position.x + direction.x, durasi);
        transform.DOJump(transform.position + direction, jumpHeight, 1, durasi);
        transform.forward = direction;
    }
}
