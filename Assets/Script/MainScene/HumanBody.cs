using UnityEngine;

public class HumanBody : MonoBehaviour
{
    [SerializeField] protected float speed = 1f;

    [SerializeField] protected float rotationSpeed = 5f;

    protected Rigidbody rb;

    protected Animator animator;

    protected AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Debug.Log("クラス名: HumanBody , 関数名: Start");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void Move(Vector3 direction)
    {
        // 速度のスケーリング
        Vector3 velocity = direction * speed;

        // y軸の速度を保持
        velocity.y = rb.linearVelocity.y;
        
        // 速度を直接設定
        rb.linearVelocity = velocity;
    }

    protected virtual void Rotation(Vector3 Direction)
    {
        // 入力方向に基づいて回転を更新
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(Direction.x, 0, Direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,rotationSpeed * Time.deltaTime);
    }
}
