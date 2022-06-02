
using UnityEngine;

public class BottomPanelAnimation : MonoBehaviour
{
    [SerializeField] Animator bottomMenuAnimator;
    [SerializeField] Animator NoteAnimator;

    private void Start()
    {
        bottomMenuAnimator.Play("BottomPart_Show");
        NoteAnimator.Play("HideNote");
    }
}
