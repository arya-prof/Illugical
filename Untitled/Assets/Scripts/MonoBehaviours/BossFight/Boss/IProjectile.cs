using UnityEngine;

public interface IProjectile 
{
    public void StartAttack(float speed, float radius, Vector3 target, BossAttack attack);
}
