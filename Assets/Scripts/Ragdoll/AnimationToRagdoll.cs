using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToRagdoll : MonoBehaviour
{
    public static AnimationToRagdoll Instance {  get; private set; }

    [SerializeField] private float respawnTime = 30f;
    public float Force = 30f;
    private Animator animator;
    public RagdollBone[] RagdollBones { get; private set; }
    public bool BIsRagdoll { get; private set; } = false;
    private readonly int walkHash = Animator.StringToHash("Distance");
    private readonly int idleHash = Animator.StringToHash("Stand");
    private readonly int clapHash = Animator.StringToHash("Clapping");
    private readonly int sitHash = Animator.StringToHash("Sit");
    [SerializeField] private Transform hips;
    [SerializeField] private Vector3 startHipsPosition;
    [SerializeField] private Quaternion startHipsRotation;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        startHipsPosition = hips.position;
        startHipsRotation = hips.rotation;

        animator = GetComponent<Animator>();

        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in rigidbodies)
        {
            rb.gameObject.AddComponent<RagdollBone>();
        }

        RagdollBones = FindObjectsOfType<RagdollBone>();
        foreach (RagdollBone ragdollBone in RagdollBones)
        {
            ragdollBone.ToggleRagdoll(false);
        }
        ToggleRagdoll(false);

    }

    public void ToggleRagdoll(bool _toggle)
    {
        BIsRagdoll = _toggle;
        if (_toggle)
        {
            foreach (RagdollBone ragdollBone in RagdollBones)
            {
                ragdollBone.ToggleRagdoll(true);
            }
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
        hips.position = startHipsPosition;
        hips.rotation = startHipsRotation;
        ToggleRagdoll(false);
        //RandomAnimation();
    }

    private void RandomAnimation()
    {
        animator.SetBool(clapHash, false);
        animator.SetFloat(walkHash, 0);
        int randomNum = Random.Range(0, 4);
        if (randomNum == 0)
            animator.SetTrigger(sitHash);
        else if (randomNum == 1)
            animator.SetTrigger(idleHash);
        else if (randomNum == 2)
            animator.SetBool(clapHash, true);
        else
            animator.SetFloat(walkHash, 1);
    }
}
