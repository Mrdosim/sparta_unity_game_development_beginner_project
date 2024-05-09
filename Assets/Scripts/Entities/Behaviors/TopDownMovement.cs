using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    private TopDownController _movementController;
    private Rigidbody2D _movementRigidbody2D;
    private Vector2 _movementDirection = Vector2.zero;

    private void Awake()
    {
        _movementController = GetComponent<TopDownController>();
        _movementRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _movementController.OnMoveEvent += Move;
    }

    private void FixedUpdate()
    {
        ApplyMovement(_movementDirection);
    }

    private void Move(Vector2 direction)
    {
        _movementDirection = direction;
    }
    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * 5;
        _movementRigidbody2D.velocity = direction;
    }
}
