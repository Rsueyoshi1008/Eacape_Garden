using UnityEngine;

public class Test : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private Transform goalPoint;
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(goalPoint.position);
    }

    private void Move(Vector3 direction)
    {
        agent.destination = direction;
    }
}
