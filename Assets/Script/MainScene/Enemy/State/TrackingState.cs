using UnityEngine;

public partial class Enemy : HumanBody
{
    private void StartTracking()
    {
        Debug.Log("クラス名: Enemy , 関数名: StartTracking");
        soundDetectionCollider.enabled = false;

        // 歩くアニメーションの再生
        animator.SetBool("Tracking", true);

        speed = dashSpeed;

        // 敵から視認されている間の処理
        playerTransform = player.GetPlayer();
    }

    private void FixedUpdateTracking()
    {
        Debug.Log("クラス名: Enemy , 関数名: UpdateTracking");

        if(isFound(playerTransform))
        {
            // 敵から視認されている間の処理
            playerTransform = player.GetPlayer();

            Vector3 dir = (playerTransform.position - transform.position).normalized;

            Move(dir);
            Rotation(dir);
        }
        else
        {
            Vector3 dir = (playerTransform.position - transform.position).normalized;
            Move(dir);
            Rotation(dir);

            SetCurrentGameState(GameState.Walk);
        }
        

        
    }

    private void EndTracking()
    {
        Debug.Log("クラス名: Enemy , 関数名: EndTracking");
        soundDetectionCollider.enabled = true;
        animator.SetBool("Tracking", false);
    }
}