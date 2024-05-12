using UnityEngine;

public class NpcAnimationController : MonoBehaviour
{
    private Animator _animator;  // Animator 컴포넌트
    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    // 움직임 벡터를 기반으로 애니메이션 상태를 업데이트하는 메소드
    public void UpdateAnimation(Vector2 movementDirection)
    {
        bool isWalking = movementDirection.magnitude > 0.1f;
        _animator.SetBool(IsWalking, isWalking);
    }
}
