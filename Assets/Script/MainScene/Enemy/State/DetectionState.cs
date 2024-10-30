using UnityEngine;

public partial class Enemy : HumanBody
{
    [SerializeField] private float detectionDistance = 10f;

    private Transform playerTransform;
    private void StartDetection()
    {
        Debug.Log("クラス名: Enemy , 関数名: StateDetection");
        // 歩くアニメーションの停止
        animator.SetFloat("Speed", 0);

        playerTransform = player.GetPlayer();
    }

    public void UpdateDetection()
    {
        if(isFound(playerTransform))
        {
            SetCurrentGameState(GameState.Tracking);
        }
        else
        {
            SetCurrentGameState(GameState.Security);
        }
    }

    private bool isFound(Transform Player)
    {
        Vector3 dir = (Player.position - transform.position).normalized;

        Rotation(dir);

        Ray ray = new Ray(transform.position, dir);
        RaycastHit hit;

        // レイヤーマスクを設定（IgnoreRaycastレイヤーを無視）
        int layerMask = ~LayerMask.GetMask("Ignore Raycast");
        Debug.DrawRay(transform.position, dir * detectionDistance, Color.red);
        // その障害物が敵とプレイヤの間にある
        if (Physics.Raycast(ray, out hit, detectionDistance, layerMask)) 
        {
            if(hit.collider.gameObject.tag == "Player")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
        
    }
}