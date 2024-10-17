using UnityEngine;

public class HumanBody : MonoBehaviour
{
    [SerializeField] protected float speed = 1f;

    protected Rigidbody rb;

    protected Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Debug.Log("クラス名: HumanBody , 関数名: Start");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void Move(Vector3 direction, Vector3 inputDirection)
    {
        // 速度のスケーリング
        Vector3 velocity = inputDirection * speed;

        // y軸の速度を保持
        velocity.y = rb.linearVelocity.y;
        
        // 速度を直接設定
        rb.linearVelocity = velocity;
    }
}
