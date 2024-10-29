using UnityEngine;

public partial class Enemy : HumanBody
{
    private void StartWalk()
    {
        Debug.Log("クラス名: Enemy , 関数名: StartWalk");
    }

    private void FixedUpdateWalk()
    {
        Debug.Log("クラス名: Enemy , 関数名: UpdateWalk");

        // targetPositionの方向を計算
        Vector3 direction = (targetPosition - transform.position).normalized;

        // 目的地までの移動と回転
        Move(direction);
        Rotation(direction);

        // 歩くアニメーションの再生
        animator.SetFloat("Speed", rb.linearVelocity.magnitude);
    }
}