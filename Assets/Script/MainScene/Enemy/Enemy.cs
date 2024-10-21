using UnityEngine;

public class Enemy : HumanBody
{
    private Vector3 targetPosition;

    [SerializeField] private float detectionRange = 1f;
    void Start()
    {
        Debug.Log("クラス名: Enemy , 関数名: Start");
        base.Start();

        targetPosition = GameObject.Find("TargetPosition").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // targetPositionの方向を計算
        Vector3 direction = (targetPosition - transform.position).normalized;

        // 目的地までの移動と回転
        Move(direction);
        Rotation(direction);



        // 歩くアニメーションの再生
        animator.SetFloat("Speed", rb.linearVelocity.magnitude);
    }

    protected override void Move(Vector3 direction)
    {
        base.Move(direction);
    }
    
    protected override void Rotation(Vector3 Direction)
    {
        base.Rotation(Direction);
    }

    private void OnTriggerStay(Collider c)
    {
        if(c.CompareTag("Detection"))
        {
            Debug.Log("アクションオブジェクトを検知");
        }
    }
}
