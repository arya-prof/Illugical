using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private Transform backgroundArea;
    [SerializeField] private Transform attackArea;

    private float _radios;

    public void StartAttack(float radios)
    {
        _radios = radios;

        backgroundArea.localScale = new Vector3(_radios,_radios,_radios);
        attackArea.localScale = new Vector3(1,1,1);
    }
    public void UpdateData(float value)
    {
        float size = (value * _radios); 
        attackArea.localScale = new Vector3(size,size,size);
    }
}
