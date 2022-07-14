using UnityEngine;

public class Func_PlayAnim : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public void StartAnimation()
    {
        anim.SetTrigger("active");
    }
}
