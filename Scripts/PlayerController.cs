using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] bool isMainMenu;

    [Header("Lane Settings")]
    [SerializeField] float laneWidth = 3f;
    [SerializeField] int laneCount = 3;
    [SerializeField] float snapSpeed = 20f;

    [Header("Swipe Settings")]
    [SerializeField] float minSwipePixels = 60f;
    [SerializeField] float maxSwipeTime = 0.6f;

    Rigidbody rb;

    int currentLane = 1;
    float targetX;

    Vector2 startPos;
    double startTime;
    bool tracking;

    Pointer activePointer;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentLane = Mathf.Clamp(currentLane, 0, laneCount - 1);
        targetX = LaneToX(currentLane);
    }

    public void OnPress(InputAction.CallbackContext ctx)
    {
        if (isMainMenu) return;

        var pointer = ctx.control?.device as Pointer;
        if (pointer == null) return;

        if (ctx.started)
        {
            tracking = true;
            activePointer = pointer;
            startPos = activePointer.position.ReadValue();
            startTime = Time.timeAsDouble;
            return;
        }

        if (tracking && ctx.canceled)
        {
            Vector2 endPos = activePointer != null
                ? activePointer.position.ReadValue()
                : pointer.position.ReadValue();

            tracking = false;
            activePointer = null;

            EvaluateSwipe(endPos);
        }
    }

    void EvaluateSwipe(Vector2 endPos)
    {
        if (isMainMenu) return;

        float dt = (float)(Time.timeAsDouble - startTime);
        if (dt > maxSwipeTime) return;

        Vector2 delta = endPos - startPos;

        if (Mathf.Abs(delta.x) >= minSwipePixels &&
            Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            ChangeLane(delta.x > 0 ? +1 : -1);
        }
    }

    void FixedUpdate()
    {
        if (isMainMenu) return;

        Vector3 pos = rb.position;

        float newX = Mathf.MoveTowards(
            pos.x,
            targetX,
            snapSpeed * Time.fixedDeltaTime
        );

        rb.MovePosition(new Vector3(newX, pos.y, pos.z));
    }

    void ChangeLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, 0, laneCount - 1);
        targetX = LaneToX(currentLane);
    }

    float LaneToX(int laneIndex)
    {
        float centerIndex = (laneCount - 1) * 0.5f;
        return (laneIndex - centerIndex) * laneWidth;
    }
}