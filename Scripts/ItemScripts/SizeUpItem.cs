using UnityEngine;

public class SizeUpItem : BaseItem
{
    public float extensionAmount = 1.5f; // 패들의 길이를 늘릴 양

    public override void ApplyEffect(GameObject player)
    {
        base.ApplyEffect(player);

        // 패들 컨트롤러 찾기
        PaddleController playerController = player.GetComponent<PaddleController>();
        if (playerController != null)
        {
            // 패들의 길이를 늘린다
            playerController.ExtendPaddle(extensionAmount);
        }
    }
}