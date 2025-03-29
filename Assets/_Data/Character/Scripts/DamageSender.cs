using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class DamageSender : DinoBehaviour
{
    [SerializeField] protected CircleCollider2D _collider;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCollider();
    }

    protected void LoadCollider()
    {
        if (this._collider != null) return;
        this._collider = GetComponent<CircleCollider2D>();
        this._collider.enabled = false;
        Debug.Log(transform.name + ": LoadCollider", gameObject);
    }

    public void EnableCollider()
    {
        this._collider.enabled = true;
    }

    public void DisableCollider()
    {
        this._collider.enabled = false;
    }
}