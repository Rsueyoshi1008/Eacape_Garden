using UnityEngine;

public partial class Enemy : HumanBody
{
    [SerializeField] private AudioClip trackingClip;
    private void StartTracking()
    {
        Debug.Log("クラス名: Enemy , 関数名: StartTracking");
        soundDetectionCollider.enabled = false;

        audioSource.clip = trackingClip;

        // 走るアニメーションの再生
        animator.SetBool("Tracking", true);

        agent.speed = dashSpeed;
    }

    private void FixedUpdateTracking()
    {
        if(isFound(playerPosition))
        {
            Debug.Log("追跡中");
            Move(playerPosition);
        }
        else
        {
            Debug.Log("確認中");
            if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                // 走るアニメーションの停止
                animator.SetBool("Tracking", false);
                SetCurrentGameState(GameState.Confirmation);
            }
        }        
    }
}