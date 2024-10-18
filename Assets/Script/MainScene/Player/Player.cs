using UnityEngine;
using Unity.Cinemachine;

public class Player : HumanBody
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    [SerializeField] private CinemachineCamera cinemachineCamera;
    
    private float rotationY = 0f;

    private float groundDistance = 0.1f;
    [SerializeField] private Transform groundCheck;
    void Start()
    {
        Debug.Log("クラス名: Player , 関数名: Start");
        base.Start();
        Cursor.lockState = CursorLockMode.Locked; // マウスカーソルをロック
    }

    // Update is called once per frame
    void Update()
    {
        // キャラクターのレイヤーマスクを作成
        int layerMask = ~LayerMask.GetMask("Player");
        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, layerMask);

        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
            
        }
        animator.SetBool("Jump", isGrounded);
    }

    private void FixedUpdate() 
    {
        float x = Input.GetAxis("Horizontal");

        float y = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(x, 0, y).normalized;

        // カメラのTransformを取得
        Transform cameraTransform = cinemachineCamera.transform;

        // カメラの向きに合わせて入力の変換
        Vector3 inputDirection = cameraTransform.TransformDirection(direction);

        Move(direction, inputDirection);

        RotationControl(inputDirection);

        animator.SetFloat("Speed", direction.magnitude);
    }

    protected override void Move(Vector3 direction, Vector3 inputDirection)
    {
        base.Move(direction, inputDirection);
    }

    private void Jump()
    {
        Debug.Log("クラス名: Player , 関数名: Jump");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void RotationControl(Vector3 inputDirection)
    {
        if (inputDirection.sqrMagnitude > 0.01f) // 入力がある場合のみ回転
        {
            // 入力方向に基づいてプレイヤーの回転を更新
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(inputDirection.x, 0, inputDirection.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    } 

    private void OnCollisionEnter(Collision c)
    {
        Debug.Log("クラス名: Player , 関数名: OnCollisionEnter");
        
        // ここに衝突時の処理を追加
    }
}
