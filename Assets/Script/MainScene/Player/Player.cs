using UnityEngine;
using Unity.Cinemachine;

public class Player : HumanBody
{
    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private CinemachineCamera cinemachineCamera;

    private float groundDistance = 0.1f;
    [SerializeField] private Transform groundCheck;

    [SerializeField] private Transform lookAt;

    // 足音の探知範囲を返す
    private bool isSoundDetection = false;
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

        Move(inputDirection);
        
        if (inputDirection.sqrMagnitude > 0.01f) // 入力がある場合のみ回転
        {
            base.Rotation(inputDirection);
        }
        
        animator.SetFloat("Speed", direction.magnitude);
    }

    protected override void Move(Vector3 inputDirection)
    {
        base.Move(inputDirection);
    }

    private void Jump()
    {
        Debug.Log("クラス名: Player , 関数名: Jump");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    

    private void SetAudioClip(string clipName)
    {
        Debug.Log("クラス名: Player , 関数名: SetAudioClip");
        //audioSource.clip = audioClips[clipName];
    }

    public bool AudioIsPlaying()
    {
        Debug.Log("クラス名: Player , 関数名: AudioIsPlaying");
        return audioSource.isPlaying;
    }

    public Transform GetLookAt()
    {
        Debug.Log("クラス名: Player , 関数名: GetLookAt");
        return lookAt;
    }

    // アニメーションイベントから呼び出される
    private void StartAudioSource()
    {
        Debug.Log("クラス名: Player , 関数名: StartAudioSource");
        audioSource.Play();
    }

    private void OnCollisionEnter(Collision c)
    {
        Debug.Log("クラス名: Player , 関数名: OnCollisionEnter");
        
        // ここに衝突時の処理を追加
    }
}
