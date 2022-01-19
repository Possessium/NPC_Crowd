using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RagdollBone : MonoBehaviour
{
    public Rigidbody BonePhysics;
    [SerializeField] private bool isRagdoll;
    Vector3 position;
    Quaternion rotation;

    private void Awake()
    {
        TryGetComponent(out BonePhysics);
        position = transform.position;
        rotation = transform.rotation;
    }
    private void Update()
    {
        if (!isRagdoll)
        {
            transform.position = position;
            transform.rotation = rotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!isRagdoll && collision.gameObject.tag == "Projectile")
        {
            Destroy(collision.gameObject);
            AnimationToRagdoll.Instance.ToggleRagdoll(true);
            BonePhysics.AddForce((collision.transform.position - collision.contacts[0].point) * AnimationToRagdoll.Instance.Force);
        }
    }

    public void ToggleRagdoll(bool _toggle)
    {
        BonePhysics.isKinematic = !_toggle;
        BonePhysics.useGravity = _toggle;
        isRagdoll = _toggle;
        if(!_toggle)
        {
            BonePhysics.velocity = Vector3.zero;
            BonePhysics.angularVelocity = Vector3.zero;
        }
    }

}
