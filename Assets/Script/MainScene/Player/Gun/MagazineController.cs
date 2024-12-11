using UnityEngine;

public class MagazineController : MonoBehaviour
{
    [SerializeField] private float forceMagnitude = 1f;
    private Vector3 forceDirection;
    
    private float count;

    private Rigidbody rb;
    void Start()
    {
        Debug.Log("クラス名: MagazineController , 関数名: Start");
        
        rb = GetComponent<Rigidbody>();

        AddCurveForce();
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;

        if(count > 5f)
        {
            Destroy(gameObject);
        }
    }

    private void AddCurveForce()
    {
        // 力の方向を正規化して、力の大きさを掛ける
        Vector3 force = forceDirection.normalized * forceMagnitude;
        rb.AddForce(force, ForceMode.Impulse);
    }

    public void GetPlayerForce(Vector3 playerDirection)
    {
        Debug.Log("クラス名: MagazineController , 関数名: GetPlayerForce");
        forceDirection = playerDirection;
    }

    private void OnCollisionEnter(Collision c) 
    {
        if (c.gameObject.tag == "Ground")
        {
            // バウンドを防ぐために速度を制御
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
