using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    public static HandController handController;
    
    private HandItem _handItem;
    private Animator _handAnim;
    private AudioSource _handAudioSource;
    [SerializeField] private Canvas mainCanvas;

    // Camera
    [Header("Hand Camera")]
    [SerializeField] private GameObject handCamera;
    private bool handCameraState;
    [SerializeField] private Animator handCameraAnim;
    [SerializeField] private Renderer handCameraPhoto;

    public IHdCmStation handCameraStation;
    private float _posDistance;
    private float _rotDistance;
    
    [SerializeField] private Image handCameraZone;
    [SerializeField] private GameObject handCameraUI;
    [SerializeField] private GameObject handCameraBlackScreen;

    [SerializeField] private AudioClip handCameraSfxZoomIn;
    [SerializeField] private AudioClip handCameraSfxZoomOut;
    [SerializeField] private AudioClip handCameraSfxZoomShoot;
    [SerializeField] private AudioClip handCameraSfxZoomSuccessShoot;
    [SerializeField] private float handCameraSfxVolume;

    // CheckList
    [Header("Check List")]
    [SerializeField] private GameObject checkList;
    private bool checkListState;
    [SerializeField] private Animator checkListAnim;

    public bool HandControlleAble
    {
        get
        {
            if (!handCameraState && !checkListState)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private bool _delay;

    private void Awake()
    {
        handController = this;
    }

    private void Start()
    {
        _handAnim = GetComponent<Animator>();
        _handAudioSource = GetComponent<AudioSource>();
        _handAudioSource.volume = handCameraSfxVolume;
        
        handCamera.SetActive(true);
        handCameraUI.SetActive(false);
        handCameraBlackScreen.SetActive(false);
        
        checkList.SetActive(false);
    }

    private void FixedUpdate()
    {
        // HandCameraStation
        if (handCameraStation != null)
        {
            if (!handCameraStation.sActive) return;
            if (handCameraStation.sFinish) return;
            Vector3 position = handCameraStation.sTransform.position + Vector3.up * 2f;
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
        if (References.Instance.freezWorld) return;
        // LeftClick
        if (Input.GetMouseButtonDown(0))
        {
            if (_handItem == HandItem.Camera && handCameraState)
            {
                _delay = true;
                StartCoroutine(Camera_TakePhoto());
                return;
            }
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
        _handAnim.SetTrigger("action");
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
            References.Instance.otherCanvas.SetActive(true);
            
            _handAudioSource.PlayOneShot(handCameraSfxZoomOut);
            
            yield return new WaitForSeconds(.5f);
            PlayerMovement.playerMovement.speed = PlayerMovement.playerMovement.walkingSpeed;
            
            handCameraState = false;
            handCameraAnim.SetBool("action", handCameraState);
            handCameraAnim.SetTrigger("active");
            
            yield return new WaitForSeconds(.5f);
        }
        else
        {
            handCameraState = true;
            handCameraAnim.SetBool("action", handCameraState);
            handCameraAnim.SetTrigger("active");
            
            yield return new WaitForSeconds(.5f);
            PlayerMovement.playerMovement.speed = PlayerMovement.playerMovement.lookingSpeed;
            
            yield return new WaitForSeconds(.5f);
            
            handCamera.SetActive(false);
            handCameraUI.SetActive(true);
            References.Instance.otherCanvas.SetActive(false);
            
            _handAudioSource.PlayOneShot(handCameraSfxZoomIn);
        }
        _delay = false;
    }
    IEnumerator Camera_TakePhoto()
    {
        bool photoTaken = false;
        if (_posDistance > .9f && _rotDistance < .3f)
        {
            photoTaken = true;
            mainCanvas.enabled = false;
            yield return null;
            string filePath = Application.dataPath + "takenPhoto.png";
            ScreenCapture.CaptureScreenshot(filePath);
            yield return new WaitForEndOfFrame();
            mainCanvas.enabled = true;
            
            handCameraStation.Activate();
            yield return null;
            _handAudioSource.PlayOneShot(handCameraSfxZoomSuccessShoot);
        }
        else
        {
            _handAudioSource.PlayOneShot(handCameraSfxZoomShoot);
        }

        handCameraBlackScreen.SetActive(true);
        yield return new WaitForSeconds(.25f);
        if (photoTaken)
        {
            string filePath = Application.dataPath + "takenPhoto.png";
            if (System.IO.File.Exists(filePath))
            {
                var bytes = System.IO.File.ReadAllBytes(filePath);
                var tex = new Texture2D(1, 1);
                tex.LoadImage(bytes);
                handCameraPhoto.material.mainTexture = tex;
            }
        }
        yield return new WaitForSeconds(.25f);
        handCameraBlackScreen.SetActive(false);
        if (photoTaken)
        {
            handCameraState = false;
            
            handCamera.SetActive(true);
            handCameraUI.SetActive(false);
            References.Instance.otherCanvas.SetActive(true);
            
            handCameraAnim.SetTrigger("watchPhoto");
            yield return new WaitForSeconds(2.4f);
            PlayerMovement.playerMovement.speed = PlayerMovement.playerMovement.walkingSpeed;
            
            handCameraZone.transform.localScale = new Vector3(0,0, 0);
        }
        else
        {
            PlayerMovement.playerMovement.speed = PlayerMovement.playerMovement.walkingSpeed;
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
            PlayerMovement.playerMovement.speed = PlayerMovement.playerMovement.walkingSpeed;
            References.Instance.otherCanvas.SetActive(false);
            checkListState = false;
        }
        else
        {
            PlayerMovement.playerMovement.speed = PlayerMovement.playerMovement.lookingSpeed;
            References.Instance.otherCanvas.SetActive(true);
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
