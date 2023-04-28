using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField,Range(0,1)] float durasi;
    [SerializeField,Range(0, 1)] float jumpHeight;

    public UnityEvent<Vector3> OnJumpEnd;

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
        transform.DOJump(transform.position + direction, jumpHeight, 1, durasi).onComplete = BroadcastPositionOnJumpEnd;
        transform.forward = direction;
    }

    private void BroadcastPositionOnJumpEnd()
    {
        OnJumpEnd.Invoke(transform.position);
    }
}
