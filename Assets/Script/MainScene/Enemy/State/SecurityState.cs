using UnityEngine;

public partial class Enemy : HumanBody
{
    [SerializeField] private AudioClip securityClip;
    private void StartSecurity()
    {
        Debug.Log("クラス名: Enemy , 関数名: StartSecurity");

        audioSource.clip = securityClip;

        soundDetectionCollider.enabled = true;

        agent.speed = speed;

        // 歩くアニメーションの再生
        animator.SetFloat("Speed", agent.speed);
    }

    private void FixedUpdateSecurity()
    {
        // 目的地までの移動と回転
        Move(targetPosition);

        if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            // 新たな目的地の設定
            targetPosition = GetRandomGoalPointPosition();
        }
    }
}