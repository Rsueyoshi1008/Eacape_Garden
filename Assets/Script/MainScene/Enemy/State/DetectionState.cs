using UnityEngine;

public partial class Enemy : HumanBody
{
    [SerializeField] private float detectionDistance = 10f;

    private Transform lookAt;
    private void StartDetection()
    {
        Debug.Log("クラス名: Enemy , 関数名: StateDetection");
        // 歩くアニメーションの停止
        animator.SetFloat("Speed", 0);

        lookAt = player.GetLookAt();
        soundDetectionCollider.enabled = false;
    }

    public void UpdateDetection()
    {
        Debug.Log("クラス名: Enemy , 関数名: UpdateDetection");
        Debug.Log(isFound(lookAt.transform));
        if(isFound(lookAt.transform))
        {
            //currentState = GameState.Run;
        }
        else
        {
            currentState = GameState.Walk;
        }
    }

    private bool isFound(Transform lookAt)
    {
        Vector3 dir = (lookAt.position - transform.position).normalized;

        base.Rotation(dir);

        Ray ray = new Ray(transform.position, dir);
        RaycastHit hit;

        // レイヤーマスクを設定（IgnoreRaycastレイヤーを無視）
        int layerMask = ~LayerMask.GetMask("Ignore Raycast");
        Debug.DrawRay(transform.position, dir * detectionDistance, Color.red);
        // その障害物が敵とプレイヤの間にある
        if (Physics.Raycast(ray, out hit, detectionDistance, layerMask)) 
        {
            Debug.Log(hit.collider.gameObject.name);
            return false;
        }
        else
        {
            // プレイヤは壁に隠れておらず、敵の視野内にいるので見つかっている
            return true;
        }
        
    }
}