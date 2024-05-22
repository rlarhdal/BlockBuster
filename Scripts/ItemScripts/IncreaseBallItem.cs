using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class IncreaseBallItem : BaseItem
{
    public override void ApplyEffect(GameObject player)
    {
        base.ApplyEffect(player);

        // 플레이어 컨트롤러 컴포넌트를 찾습니다.
        PaddleController playerController = player.GetComponent<PaddleController>();
        if (playerController != null)
        {
            // 공을 추가합니다.
            playerController.IncreaseBallCount();
        }
    }
}
