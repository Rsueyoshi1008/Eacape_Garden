using UnityEngine;
using System.Collections;
public partial class Enemy : HumanBody
{
    private Transform currentTransform;

    private float tmp;
    private void StartConfirmation()
    {
        Debug.Log("クラス名: Enemy , 関数名: StartConfirmation");

        tmp = agent.speed;
        agent.speed = 0f;
        animator.SetFloat("Speed", agent.speed);
        canvas.SetActive(true);

        Vector3 direction = (detectedPosition - transform.position).normalized;
        base.Rotation(direction);

        StartCoroutine(WaitAndSetPosition());
    }

    private void FixedUpdateConfirmation()
    {
        if(agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            // 移動完了後の処理
            StartCoroutine(WaitAndSetState());
            Debug.Log("移動完了");
        }
        if(isFound(playerPosition))
        {
            SetCurrentGameState(GameState.Tracking);
            Debug.Log("発見");
        }
    }

    private IEnumerator WaitAndSetPosition()
    {
        // 2秒間待機
        yield return new WaitForSeconds(2f);

        agent.speed = tmp;
        canvas.SetActive(false);

        Move(detectedPosition);

        animator.SetFloat("Speed", agent.speed);
    }

    private IEnumerator WaitAndSetState()
    {
        animator.SetFloat("Speed", 0f);

        canvas.SetActive(true);

        // 5秒間待機
        yield return new WaitForSeconds(5f);

        canvas.SetActive(false);

        animator.SetFloat("Speed", agent.speed);

        SetCurrentGameState(GameState.Security);
    }
}
