
using UnityEngine;

public class CapitalShip : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        Invoke("PlayAnimation", 75);
    }

    void PlayAnimation()
    {
        animator.Play("CapitalShip");
    }
}
