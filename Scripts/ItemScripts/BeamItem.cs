using UnityEngine;

public class BeamItem : BaseItem
{
    public GameObject beamFirePrefab; // Beamfire �������� �Ҵ�

    public override void ApplyEffect(GameObject player)
    {
        // �е�� �浹�Ͽ� ������ ȿ���� �ߵ��� �� ����� �ڵ�
        Vector3 collisionPoint = player.transform.position;
        float xCoordinate = collisionPoint.x;

        // �� �����ؼ� 1�� ����ֱ�
        Quaternion beamRotation = Quaternion.Euler(0f, 0f, -90f); // Z�� ȸ������ -90���� ����
        GameObject beamFire = Instantiate(beamFirePrefab, new Vector3(xCoordinate, 0f, 0f), beamRotation);
        Destroy(beamFire, 1f);
    }
}
