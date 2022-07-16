using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Env_WindSystem : MonoBehaviour
{
    //
    [SerializeField] private Env_Wind[] particles;
    private int objNo
    {
        get
        {
            return Random.Range(0, particles.Length);
        }
    }
    //
    [SerializeField] private Env_WindArea[] areas;
    [SerializeField] private bool showAreas;
    //
    private void Start()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].prefabPool = new List<GameObject>();
        }
        Invoke("SpawnParticle",2f);
    }
    private void SpawnParticle()
    {
        for (int i = 0; i < areas.Length; i++)
        {
            GameObject obj = GetPoolObj(objNo);
            obj.transform.localRotation = transform.rotation;

            Vector3 center = areas[i].center;
            Vector3 size = areas[i].size;
            
            obj.transform.position = center + new Vector3(Random.Range(-size.x/2,size.x/2),Random.Range(-size.y/2,size.y/2),Random.Range(-size.z/2,size.z/2));
            
            StartCoroutine(DisableObj(obj));
        }

        Invoke("SpawnParticle",15f);
    }
    private GameObject GetPoolObj(int _objNo)
    {
        for (int i = 0; i < particles[_objNo].prefabPool.Count; i++)
        {
            if (!particles[_objNo].prefabPool[i].activeInHierarchy)
            {
                GameObject obj = particles[_objNo].prefabPool[i];
                obj.SetActive(true);
                return obj;
            }
        }
        
        GameObject objNew = Instantiate(particles[_objNo].prefab, transform);
        particles[_objNo].prefabPool.Add(objNew);
        return objNew;
    }
    IEnumerator DisableObj(GameObject obj)
    {
        yield return new WaitForSeconds(15f);
        obj.SetActive(false);
    }

    void OnDrawGizmos()
    {
        if(!showAreas) return;
        if (areas.Length != 0)
        {
            for (int i = 0; i < areas.Length; i++)
            {
                Gizmos.DrawWireCube(areas[i].center, areas[i].size);   
            }
        }
    }
}
