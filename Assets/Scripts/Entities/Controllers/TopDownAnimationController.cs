using UnityEngine;

public class TopDownAnimatorController : AnimationController
{
    private static readonly int _isWalking = Animator.StringToHash("isWalking");

    private readonly float _magnituteThreshold = 0.5f;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        controller.OnMoveEvent += Move;
    }

    private void Move(Vector2 obj)
    {
        animator.SetBool(_isWalking, obj.magnitude > _magnituteThreshold);
    }
}