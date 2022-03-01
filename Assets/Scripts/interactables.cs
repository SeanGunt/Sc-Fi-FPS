using UnityEngine;

public abstract class interactables : MonoBehaviour
{
    public virtual void Awake()
    {
        gameObject.layer = 9;
    }
    public abstract void OnInteract();
    public abstract void OnFocus();
    public abstract void OnLoseFocus();
}
