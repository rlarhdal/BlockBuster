using UnityEngine;

public class SizeDownItem : BaseItem
{
    public float reductionAmount = 1.5f; // 패들의 길이를 줄일 양

    public override void ApplyEffect(GameObject player)
    {
        base.ApplyEffect(player);

        // 패들 컨트롤러 찾기
        PaddleController playerController = player.GetComponent<PaddleController>();
        if (playerController != null)
        {
            // 패들의 길이를 줄인다.
            playerController.ShrinkPaddle(reductionAmount);
        }
    }
}