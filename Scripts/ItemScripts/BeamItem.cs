using UnityEngine;

public class BeamItem : BaseItem
{
    public GameObject beamFirePrefab; // Beamfire 프리팹을 할당

    public override void ApplyEffect(GameObject player)
    {
        // 패들과 충돌하여 아이템 효과가 발동될 때 실행될 코드
        Vector3 collisionPoint = player.transform.position;
        float xCoordinate = collisionPoint.x;

        // 빔 생성해서 1초 띄워주기
        Quaternion beamRotation = Quaternion.Euler(0f, 0f, -90f); // Z축 회전값을 -90으로 고정
        GameObject beamFire = Instantiate(beamFirePrefab, new Vector3(xCoordinate, 0f, 0f), beamRotation);
        Destroy(beamFire, 1f);
    }
}
