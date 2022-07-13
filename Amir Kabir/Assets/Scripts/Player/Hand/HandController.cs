using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    public static HandController handController;
    
    private HandItem _handItem;
    private Animator handAnim;

    // Camera
    [Header("Hand Camera")]
    [SerializeField] private GameObject handCamera;
    private bool handCameraState;
    [SerializeField] private Animator handCameraAnim;

    public IHdCmStation handCameraStation;
    [SerializeField] private Image handCameraZone;
    
    // CheckList
    [Header("Check List")]
    [SerializeField] private GameObject checkList;
    private bool checkListState;
    [SerializeField] private Animator checkListAnim;

    private bool _delay;

    private void Awake()
    {
        handController = this;
    }

    private void Start()
    {
        handAnim = GetComponent<Animator>();
        
        handCamera.SetActive(true);
        checkList.SetActive(false);
    }

    private void Update()
    {
        if(_delay) return;

        // HandCameraStation
        if (handCameraStation != null)
        {
        }

        // LeftClick
        if (Input.GetMouseButtonDown(0))
        {
            
            return;
        }

        // RightClick
        if (Input.GetMouseButtonDown(1))
        {
            _delay = true;
            switch (_handItem)
            {
                case HandItem.Camera:
                    StartCoroutine(Camera_LookAt());
                    return;
                case HandItem.CheckList:
                    StartCoroutine(CheckList_LookAt());
                    return;
            }
        }
        
        // PressTab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _delay = true;
            switch (_handItem)
            {
                case HandItem.Camera:
                    _handItem = HandItem.CheckList;
                    StartCoroutine(ChangeItem(handCamera,checkList));
                    return;
                case HandItem.CheckList:
                    _handItem = HandItem.Camera;
                    StartCoroutine(ChangeItem(checkList,handCamera));
                    return;
            }
        }
    }

    IEnumerator ChangeItem(GameObject hideObj, GameObject showObj)
    {
        handAnim.SetTrigger("action");
        yield return new WaitForSeconds(1.0f);
        hideObj.SetActive(false);
        showObj.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        _delay = false;
    }

    #region HandCamera
    IEnumerator Camera_LookAt()
    {
        handCameraAnim.SetTrigger("state");
        if (handCameraState)
        {
            handCameraState = false;
        }
        else
        {
            handCameraState = true;
        }
        yield return new WaitForSeconds(1.0f);
        _delay = false;
    }
    #endregion

    #region CheckList
    IEnumerator CheckList_LookAt()
    {
        checkListAnim.SetTrigger("state");
        if (checkListState)
        {
            checkListState = false;
        }
        else
        {
            checkListState = true;
        }
        yield return new WaitForSeconds(1.0f);
        _delay = false;
    }
    #endregion
}
