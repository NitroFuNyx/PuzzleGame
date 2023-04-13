using UnityEngine;

public class KitchenCattleInteractor : MonoBehaviour,Iinteractable
{
    [SerializeField] private Animator _panAnimator;

    public void Interact()
    {
        _panAnimator.SetTrigger("Tap");
    }
}
