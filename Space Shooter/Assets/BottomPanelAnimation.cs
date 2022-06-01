
using UnityEngine;

public class BottomPanelAnimation : MonoBehaviour
{
    [SerializeField] Animator bottomMenuAnimator;

    private void Start()
    {
        bottomMenuAnimator.Play("BottomPart_Show");
    }
}
