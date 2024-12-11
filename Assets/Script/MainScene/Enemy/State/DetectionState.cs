using UnityEngine;

public partial class Enemy : HumanBody
{
    private void StartDetection()
    {
        Debug.Log("クラス名: Enemy , 関数名: StateDetection");

        animator.SetFloat("Speed", 0f);
    }

    public void UpdateDetection()
    {
        if(isFound(playerPosition))
        {
            SetCurrentGameState(GameState.Tracking);
        }
        else
        {
            SetCurrentGameState(GameState.Confirmation);
        }
    }

    
}