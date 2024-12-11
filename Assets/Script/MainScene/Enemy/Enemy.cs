using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public partial class Enemy : HumanBody
{
    public enum GameState
    {
        Security,
        Confirmation,
        Tracking,
        Attack,
        Damage,
        Dead,
        Detection
    };

    [SerializeField] private Transform stepRayPoint;

    [SerializeField] private GameObject canvas;

    private GameState currentState;
    private Vector3 targetPosition;

    private Player player;
    private DetectionItem detectionItem;
    private Vector3 playerPosition;
    private Vector3 detectedPosition;

    private BoxCollider soundDetectionCollider;

    private NavMeshAgent agent;

    private Transform goalPoint;
    
    void Start()
    {
        Debug.Log("クラス名: Enemy , 関数名: Start");
        base.Start();

        soundDetectionCollider = GetComponentInChildren<BoxCollider>();
        goalPoint = GameObject.Find("EnemyGoalPoint").transform;
        agent = GetComponent<NavMeshAgent>();
        targetPosition = GetRandomGoalPointPosition();
        canvas.SetActive(false);
        SetCurrentGameState(GameState.Security);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case GameState.Attack:
                UpdateAttack();
                break;

            case GameState.Damage:
                UpdateDamage();
                break;

            case GameState.Dead:
                UpdateDead();
                break;
            
            case GameState.Detection:
                UpdateDetection();
                break;
        }
    }
// 段差を認識して対応する処理
    private void FixedUpdate()
    {
        switch (currentState)
        {
            case GameState.Security:
                FixedUpdateSecurity();
                break;

            case GameState.Confirmation:
                FixedUpdateConfirmation();
                break;

            case GameState.Tracking:
                FixedUpdateTracking();
                break;
        }
    }

    protected override void Move(Vector3 destination)
    {
        agent.destination = destination;
    }

    private void PlayFootSteps()
    {
        audioSource.Play();
    }

    private bool isFound(Vector3 Position)
    {
        Vector3 dir = (Position - transform.position).normalized;
        float detectionDistance = Vector3.Distance(transform.position, Position);

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

    private bool isStep()
    {
        Ray ray = new Ray(stepRayPoint.position, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(stepRayPoint.position, transform.forward, Color.blue);
        if (Physics.Raycast(ray, out hit, 0.1f))
        {
            return false;
        }
        return true;
    }

    private Vector3 GetRandomGoalPointPosition()
    {
        Debug.Log("クラス名: Enemy , 関数名: GetRandomGoalPointPosition");
        
        int childCount = goalPoint.childCount;
        if (childCount == 0)
        {
            Debug.LogWarning("goalPointに子要素がありません");
            return Vector3.zero;
        }

        int randomIndex = Random.Range(0, childCount);
        Transform randomChild = goalPoint.GetChild(randomIndex);
        return randomChild.position;
    }

    private void OnTriggerStay(Collider c)
    {
        // タグを比較して特定のタグを持つオブジェクトに対してのみ処理を行う
        if (c.gameObject.tag == "Player")
        {
            player = c.gameObject.GetComponent<Player>();
            playerPosition = c.transform.position;
            detectedPosition = c.transform.position;

            if(player.AudioIsPlaying() && GameState.Confirmation != currentState)
            {
                SetCurrentGameState(GameState.Detection);
            }
        }
        if(c.gameObject.tag == "Detection")
        {
            detectionItem = c.gameObject.GetComponent<DetectionItem>();
            detectedPosition = c.transform.position;

            if(detectionItem.GetAudioIsPlaying() && GameState.Confirmation != currentState)
            {
                SetCurrentGameState(GameState.Confirmation);
            }
        }
    }

    public void SetCurrentGameState(GameState newState)
    {
        Debug.Log("クラス名: Enemy , 関数名: SetCurrentGameState");

        currentState = newState;
        Debug.Log("ステート遷移: " + currentState);
        switch(currentState)
        {
            case GameState.Security:
                StartSecurity();
                break;

            case GameState.Confirmation:
                StartConfirmation();
                break;

            case GameState.Tracking:
                StartTracking();
                break;

            case GameState.Attack:
                StartAttack();
                break;

            case GameState.Damage:
                StartDamage();
                break;

            case GameState.Dead:
                StartDead();
                break;
            
            case GameState.Detection:
                StartDetection();
                break;
        }
    }
}
