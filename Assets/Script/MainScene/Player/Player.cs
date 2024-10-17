using UnityEngine;
using Unity.Cinemachine;

public class Player : HumanBody
{

    [SerializeField] private float mouseSensitivity = 10f;

    [SerializeField] private CinemachineCamera cinemachineCamera;
    
    private float rotationY = 0f;
    void Start()
    {
        Debug.Log("クラス名: Player , 関数名: Start");
        base.Start();
        Cursor.lockState = CursorLockMode.Locked; // マウスカーソルをロック
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        float x = Input.GetAxis("Horizontal");

        float y = Input.GetAxis("Vertical");

        

        RotationControl();

        Vector3 direction = new Vector3(x, 0, y).normalized;

        // カメラのTransformを取得
        Transform cameraTransform = cinemachineCamera.transform;

        // カメラの向きに合わせて入力の変換
        Vector3 inputDirection = cameraTransform.TransformDirection(direction);

        Move(direction, inputDirection);

        animator.SetFloat("Speed", direction.magnitude);
    }

    protected override void Move(Vector3 direction, Vector3 inputDirection)
    {
        base.Move(direction, inputDirection);
    }

    private void RotationControl()
    {
        // マウスの入力を取得
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        
        // プレイヤーの回転を更新
        rotationY += mouseX;

        transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);
    } 

    private void OnCollisionEnter(Collision c)
    {
        Debug.Log("クラス名: Player , 関数名: OnCollisionEnter");
        Debug.Log("接触したオブジェクト" + c.gameObject.name);
        // ここに衝突時の処理を追加
    }
}
