using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class IncreaseBallItem : BaseItem
{
    public override void ApplyEffect(GameObject player)
    {
        base.ApplyEffect(player);

        // �÷��̾� ��Ʈ�ѷ� ������Ʈ�� ã���ϴ�.
        PaddleController playerController = player.GetComponent<PaddleController>();
        if (playerController != null)
        {
            // ���� �߰��մϴ�.
            playerController.IncreaseBallCount();
        }
    }
}
