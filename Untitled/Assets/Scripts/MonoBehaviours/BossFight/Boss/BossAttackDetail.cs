using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BossAttackDetail
{
    public string phaseName;
    public float attackStartTime;
    public float totalReloadTime;
    public float totalFireTime;
    public int totalProjectile;
    public int rewardProjectile;
    public float projectileSpeed;
    public float projectileRadius;
}
