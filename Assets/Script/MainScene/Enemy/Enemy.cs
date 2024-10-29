using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public partial class Enemy : HumanBody
{
    public enum GameState
    {
        Idle,
        Walk,
        Tracking,
        Attack,
        Damage,
        Dead,
        Detection
    };

    private GameState currentState;
    private Vector3 targetPosition;

    private Player player;

    private BoxCollider soundDetectionCollider;

    private NavMeshAgent agent;

    private Transform goalPoint;
    void Start()
    {
        Debug.Log("クラス名: Enemy , 関数名: Start");
        base.Start();

        currentState = GameState.Walk;
        soundDetectionCollider = GetComponentInChildren<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
        goalPoint = GameObject.Find("EnemyGoalPoint").transform;
        targetPosition = GetRandomGoalPointPosition();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case GameState.Idle:
                UpdateIdle();
                break;

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

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case GameState.Walk:
                FixedUpdateWalk();
                break;

                case GameState.Tracking:
                FixedUpdateTracking();
                break;
        }
    }

    protected override void Move(Vector3 direction)
    {
        base.Move(direction);
    }
    
    protected override void Rotation(Vector3 Direction)
    {
        base.Rotation(Direction);
    }

    private void StartWalkAudio()
    {
        audioSource.Play();
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

            if(player.AudioIsPlaying())
            {
                SetCurrentGameState(GameState.Detection);
            }
            
        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            StartCoroutine(DelayedExit());
        }
    }

    private IEnumerator DelayedExit()
    {
        yield return new WaitForSeconds(2f); // 2秒待機

        player = null;
        SetCurrentGameState(GameState.Walk);
    }

    public void SetCurrentGameState(GameState newState)
    {
        Debug.Log("クラス名: Enemy , 関数名: SetCurrentGameState");

        currentState = newState;
        Debug.Log("ステート遷移: " + currentState);
        switch(currentState)
        {
            case GameState.Idle:
                StartIdle();
                break;

            case GameState.Walk:
                StartWalk();
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
