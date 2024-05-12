using UnityEngine;

public class TopDownAimRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _characterRenderer;

    private TopDownController _controller;
    private void Awake()
    {
        _controller = GetComponent<TopDownController>();
    }

    private void Start()
    {
        _controller.OnLookEvent += OnAim;
    }

    public void OnAim(Vector2 newdirection)
    {
        RotateArm(newdirection);
    }

    private void RotateArm(Vector2 direction)
    {
        float rotZ = -Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _characterRenderer.flipX = Mathf.Abs(rotZ) > 90f;
    }

}