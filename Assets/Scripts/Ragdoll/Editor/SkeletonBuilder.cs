using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SkeletonBuilder : EditorWindow
{
    [SerializeField] List<GameObject[]> everyJoints = new List<GameObject[]>();

    [MenuItem("Window/Skeleton builder")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        SkeletonBuilder window = (SkeletonBuilder)EditorWindow.GetWindow(typeof(SkeletonBuilder));
        window.Show();
    }

    private void OnGUI()
    {
        if(everyJoints.Count == 0)
        {
            everyJoints.Add(new GameObject[2]);
        }
        for (int i = 0; i < everyJoints.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Parent Bone");
            everyJoints[i][0] = (GameObject)EditorGUILayout.ObjectField(everyJoints[i][0], typeof(GameObject), true);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Child Bone");
            everyJoints[i][1] = (GameObject)EditorGUILayout.ObjectField(everyJoints[i][1], typeof(GameObject), true);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }
        if(everyJoints[everyJoints.Count - 1][0] != null || everyJoints[everyJoints.Count - 1][1] != null)
        {
                everyJoints.Add(new GameObject[2]);
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (GUILayout.Button("Create Bones"))
            BuildSkeleton();
    }

    private void BuildSkeleton()
    {
        for (int i = 0; i < everyJoints.Count - 1; i++)
        {
            GameObject parentBone = everyJoints[i][0];
            GameObject childBone = everyJoints[i][1];
            CapsuleCollider colliderParent;
            CharacterJoint jointChild;
            if (!parentBone.GetComponent<Rigidbody>())
                parentBone.AddComponent<Rigidbody>();
            if (!parentBone.GetComponent<CapsuleCollider>())
                colliderParent = parentBone.AddComponent<CapsuleCollider>();
            else
                colliderParent = parentBone.GetComponent<CapsuleCollider>();

            if (!childBone.GetComponent<Rigidbody>())
                childBone.AddComponent<Rigidbody>();
            if (!childBone.GetComponent<CapsuleCollider>())
                childBone.AddComponent<CapsuleCollider>();
            if (!childBone.GetComponent<CharacterJoint>())
                jointChild = childBone.AddComponent<CharacterJoint>();
            else
                jointChild = childBone.GetComponent<CharacterJoint>();

            jointChild.connectedBody = parentBone.GetComponent<Rigidbody>();
            colliderParent.height = Vector3.Distance(parentBone.transform.position, childBone.transform.position);
            colliderParent.center = (childBone.transform.localPosition / 2);
            colliderParent.direction = 0;
            colliderParent.radius = .08f;
        }
    }
}
