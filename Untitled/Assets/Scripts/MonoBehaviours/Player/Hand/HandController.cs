using System.Collections;
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
    private float _posDistance;
    private float _rotDistance;
    
    [SerializeField] private Image handCameraZone;
    [SerializeField] private GameObject handCameraUI;
    [SerializeField] private GameObject handCameraTakePhoto;
    
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
        handCameraUI.SetActive(false);
        handCameraTakePhoto.SetActive(false);
        
        checkList.SetActive(false);
    }

    private void FixedUpdate()
    {
        // HandCameraStation
        if (handCameraStation != null)
        {
            if (!handCameraStation.sActive) return;
            
            Vector3 position = handCameraStation.sTransform.position + handCameraStation.sTransform.up * 2f;
            float distance = Vector3.Distance(transform.position, position);
            _posDistance = 1-(distance / handCameraStation.sRadius);
            handCameraZone.transform.localScale = new Vector3(_posDistance,_posDistance, _posDistance);
            
            Vector3 targetPos = position + handCameraStation.sTransform.forward * 3f;
            Vector3 forward = transform.forward * 3f + transform.position;
            _rotDistance = Vector3.Distance(targetPos, forward);
            if (_rotDistance < 2f)
            {
                float value = _rotDistance / 2;
                handCameraZone.transform.localRotation = Quaternion.Euler(0f,0f,90f * value);
            }
            else
            {
                handCameraZone.transform.localRotation = Quaternion.Euler(0f,0f,90f);
            }
        }
        else
        {
            handCameraZone.transform.localScale = new Vector3(0,0, 0);
        }
    }

    private void Update()
    {
        if(_delay) return;

        // LeftClick
        if (Input.GetMouseButtonDown(0))
        {
            if (handCameraStation != null)
            {
                if (!handCameraStation.sActive) return;
                _delay = true;
                StartCoroutine(Camera_TakePhoto());
            }
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
                    if (handCameraState) return;
                    _handItem = HandItem.CheckList;
                    StartCoroutine(ChangeItem(handCamera,checkList));
                    return;
                case HandItem.CheckList:
                    if (checkListState) return;
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
        if (handCameraState)
        {
            handCamera.SetActive(true);
            handCameraUI.SetActive(false);
            
            handCameraState = false;
            handCameraAnim.SetBool("action", handCameraState);
            handCameraAnim.SetTrigger("active");
            
            yield return new WaitForSeconds(1.1f);
        }
        else
        {
            handCameraState = true;
            handCameraAnim.SetBool("action", handCameraState);
            handCameraAnim.SetTrigger("active");
            
            yield return new WaitForSeconds(1f);
            
            handCamera.SetActive(false);
            handCameraUI.SetActive(true);
            
            yield return new WaitForSeconds(.1f);
        }
        _delay = false;
    }
    IEnumerator Camera_TakePhoto()
    {
        handCameraTakePhoto.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        handCameraTakePhoto.SetActive(false);
        if (_posDistance > .9f && _rotDistance < .3f)
        {
            handCameraStation.Activate();
            handCameraZone.color = Color.cyan;
        }
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

    private void OnDrawGizmos()
    {
        if (handCameraStation != null)
        {
            Vector3 targetPos = handCameraStation.sTransform.position + handCameraStation.sTransform.up * 2f + handCameraStation.sTransform.forward * 3f;
            Vector3 forward = transform.forward * 3f + transform.position;
            Gizmos.DrawWireSphere(targetPos,2f);
            Gizmos.DrawWireSphere(forward,2f);
        }
    }
}
