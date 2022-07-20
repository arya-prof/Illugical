using UnityEngine;

public class CannonProjectile : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, BossMaster.boss.transform.position, speed * Time.deltaTime);
    }
}
