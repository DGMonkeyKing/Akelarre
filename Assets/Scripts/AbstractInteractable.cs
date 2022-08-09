using UnityEngine;

public abstract class AbstractInteractable : MonoBehaviour
{
    private Collider _collider = null;

    public abstract void OnOver();
    public abstract void OnExit();
    public abstract void OnClick();

    public void MakeInteractable(bool interactable)
    {
        _collider.enabled = interactable;
    }
}