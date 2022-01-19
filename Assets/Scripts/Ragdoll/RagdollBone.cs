using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RagdollBone : MonoBehaviour
{
    public Rigidbody BonePhysics;
    private bool isRagdoll;
    RagdollBone[] ragdollBones;

    private void Start()
    {
        TryGetComponent(out BonePhysics);
        BonePhysics.isKinematic = true;
        BonePhysics.useGravity = false;
        ragdollBones = FindObjectsOfType<RagdollBone>();
        ragdollBones = ragdollBones.Where(x => x != this).ToArray();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!isRagdoll && collision.gameObject.tag == "Projectile")
        {
            Destroy(collision.gameObject);
            ToggleRagdoll(true);
            AnimationToRagdoll.Instance.ToggleRagdoll(true);
            BonePhysics.AddForce((collision.contacts[0].normal + collision.contacts[0].thisCollider.attachedRigidbody.velocity) * AnimationToRagdoll.Instance.Force);
        }
    }

    public void ToggleRagdoll(bool _toggle)
    {
        BonePhysics.isKinematic = _toggle;
        BonePhysics.useGravity = !_toggle;
        isRagdoll = _toggle;
        if(!_toggle)
        {
            BonePhysics.velocity = Vector3.zero;
            BonePhysics.angularVelocity = Vector3.zero;
        }
    }

}
