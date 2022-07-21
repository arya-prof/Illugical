using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCannon : MonoBehaviour, IProjectile
{
    private bool _onMove;
    private bool _activate;
    private float _time;
    
    private float _speed;
    private float _radius;
    private Vector3 _target;
    private BossAttack _attack;

    private SphereCollider collider;
    
    private float _distance;

    public void StartAttack(float speed, float radius, Vector3 target, BossAttack attack)
    {

        _speed = speed;
        _radius = radius;
        _target = target;
        _attack = attack;

        collider = gameObject.GetComponent<SphereCollider>();

        _distance = Vector3.Distance(transform.position, _target);
        _attack.gameObject.SetActive(true);
        _attack.StartAttack(_radius);
        
        _onMove = true;
    }

    private void FixedUpdate()
    {
        if (_activate)
        {
            if (_time < 1)
            {
                _time += Time.deltaTime;
                collider.radius = 1 + ((_time / 1) * _radius-1);
            }
            else
            {
                _activate = false;
                _attack.gameObject.SetActive(false);
                collider.radius = 1;
                collider.isTrigger = false;
                transform.rotation = Quaternion.Euler(0,0,0);
            }
            return;
        }

        if (_onMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
            float distance = Vector3.Distance(transform.position, _target);
            float value = 1 - (distance / _distance);
            _attack.UpdateData(value);
            if (distance < .01f)
            {
                _activate = true;
                _onMove = false;
                _attack.PlaySound();
                BossMaster.boss.RemoveProjectile(gameObject);
            }
        }
    }
}
