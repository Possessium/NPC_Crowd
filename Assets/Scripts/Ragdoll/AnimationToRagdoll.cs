using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToRagdoll : MonoBehaviour
{
    public static AnimationToRagdoll Instance {  get; private set; }

    [SerializeField] private Collider myCollider;
    [SerializeField] private float respawnTime = 30f;
    public float Force = 30f;
    [SerializeField] private Rigidbody bodyCenter;
    public RagdollBone[] RagdollBones { get; private set; }
    public bool BIsRagdoll { get; private set; } = false;
    private readonly int walkHash = Animator.StringToHash("Walk");
    private readonly int idleHash = Animator.StringToHash("Idle");
    private Rigidbody projectileTouching;
    [SerializeField] private Transform hips;
    [SerializeField] private Vector3 startHipsPosition;
    private Vector3 normalHit;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TryGetComponent(out myCollider);
        startHipsPosition = hips.localPosition;

        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        //foreach (Rigidbody ragdollBone in rigidbodies)
        //{
        //    ragdollBone.isKinematic = true;
        //}

        foreach (Rigidbody rb in rigidbodies)
        {
            rb.gameObject.AddComponent<RagdollBone>();
        }

        RagdollBones = FindObjectsOfType<RagdollBone>();
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (!BIsRagdoll && collision.gameObject.tag == "Projectile")
    //    {
    //        projectileTouching = collision.rigidbody;
    //        normalHit = collision.GetContact(0).normal;
    //        ToggleRagdoll(false);
    //        StartCoroutine(GetBackUp());
    //    }
    //}

    public void ToggleRagdoll(bool _toggle)
    {
        BIsRagdoll = _toggle;
        //myCollider.enabled = bisAnimating;
        foreach (RagdollBone ragdollBone in RagdollBones)
        {
            ragdollBone.BonePhysics.isKinematic = !_toggle;
        }
        //if (projectileTouching)
        //    bodyCenter.AddForce(normalHit * force, ForceMode.Impulse);

        GetComponent<Animator>().enabled = !_toggle;
        if (_toggle)
        {
            StartCoroutine(GetBackUp());
        }
    }

    private IEnumerator GetBackUp()
    {
        yield return new WaitForSeconds(respawnTime);
        foreach (RagdollBone ragdollBone in RagdollBones)
        {
            ragdollBone.ToggleRagdoll(false);
        }
        hips.localPosition = startHipsPosition;
        projectileTouching = null;
        ToggleRagdoll(false);
        RandomAnimation();
    }

    private void RandomAnimation()
    {
        int randomNum = Random.Range(0, 2);
        Animator animator = GetComponent<Animator>();
        if (randomNum == 0)
            animator.SetTrigger(walkHash);
        else
            animator.SetTrigger(idleHash);
    }
}
