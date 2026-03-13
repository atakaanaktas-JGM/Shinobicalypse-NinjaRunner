using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{

    [SerializeField] Animator animator;
    float collisionCooldown = 1f;   
    const string hitString = "Hit";
    float nextHitTime = 0f;
    [SerializeField] float adjustMoveSpeedAmount = -2f;
    LevelGenerator levelGenerator;
    private void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }



    void OnCollisionEnter(Collision other)
    {
        if (Time.time < nextHitTime) return;

        levelGenerator.ChangeChunkSpeed(adjustMoveSpeedAmount);
        animator.SetTrigger(hitString);
        nextHitTime = Time.time + collisionCooldown;
    }
}
