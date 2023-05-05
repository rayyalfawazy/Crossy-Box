using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    float moveDuration = 0.15f;
    float jumpHeight = 0.5f;

    [SerializeField] int leftMoveLimit;
    [SerializeField] int rightMoveLimit;
    [SerializeField] int backMoveLimit;

    public UnityEvent<Vector3> OnJumpStart;
    public UnityEvent<Vector3> OnJumpEnd;
    public UnityEvent<Vector3> OnDeadSituation;

    // bool isMoving = false;
    bool isMovable = true;

    void Update()
    {
        if (!isMovable)
        {
            return;
        }

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

    public void SetMovable(bool value)
    {
        isMovable = value;
    }

    public void Move(Vector3 direction)
    {
        var targetPosition = transform.position + direction;

        if (targetPosition.x < leftMoveLimit || 
            targetPosition.x > rightMoveLimit || 
            targetPosition.z < backMoveLimit || 
            Obstacle.Position.Contains(targetPosition)) 
        {
            targetPosition = transform.position;
        }

        BroadcastPositionOnJumpStart();
        transform.DOJump(targetPosition, jumpHeight, 1, moveDuration).onComplete = BroadcastPositionOnJumpEnd;
        transform.forward = direction;
    }

    public void UpdateMoveLimit(int horizontalSize, int backLimit)
    {
        leftMoveLimit = -horizontalSize/2;
        rightMoveLimit = horizontalSize/2;
        backMoveLimit = backLimit;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isMovable == false)
        {
            return; // Player tidak bisa bergerak, sebelum ada trigger
        }
        MakeItDie();
    }

    void MakeItDie()
    {
        BroadcastPositionOnDeadSituation();
        transform.DOScale(new Vector3(1, 0.1f, 2f), 0.2f); // Player Gepeng
        isMovable = false;
    }

    private void BroadcastPositionOnJumpEnd()
    {
        OnJumpEnd.Invoke(transform.position);
    }

    private void BroadcastPositionOnJumpStart()
    {
        OnJumpStart.Invoke(transform.position);
    }

    private void BroadcastPositionOnDeadSituation()
    {
        OnDeadSituation.Invoke(transform.localScale);
    }
}
