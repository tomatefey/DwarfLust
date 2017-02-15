using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK_Handling : MonoBehaviour {

    Animator anim;

    [Header("LEGS")]
    //[Range(0.0f, 1.0f)]public float leftFootWeight;
    public float feetOffset;

    PlayerController playerControl;

    Transform leftFoot;
    Transform rightFoot;

    Vector3 lFPos;
    Vector3 rFPos;

    Vector3 lpos;
    Vector3 rpos;

    Quaternion lFRot;
    Quaternion rFRot;

    float leftFootWeight = 1;
    float rightFootWeight = 1;
    
    RaycastHit leftHit;
    RaycastHit rightHit;
    

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

        leftFoot = anim.GetBoneTransform(HumanBodyBones.LeftFoot);
        rightFoot = anim.GetBoneTransform(HumanBodyBones.RightFoot);

        playerControl = GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {

        leftFoot = anim.GetBoneTransform(HumanBodyBones.LeftFoot);
        rightFoot = anim.GetBoneTransform(HumanBodyBones.RightFoot);

        lpos = leftFoot.TransformPoint(Vector3.zero);
        rpos = rightFoot.TransformPoint(Vector3.zero);

        if(Physics.Raycast(lpos, -Vector3.up, out leftHit, 2))
        {
            lFPos = leftHit.point;
            lFPos.y += feetOffset;
            lFRot = Quaternion.FromToRotation(transform.up, leftHit.normal) * transform.rotation; 
        }
        if (Physics.Raycast(rpos, -Vector3.up, out rightHit, 2))
        {
            rFPos = rightHit.point;
            rFPos.y += feetOffset;
            rFRot = Quaternion.FromToRotation(transform.up, rightHit.normal) * transform.rotation;
        }

    }

    private void OnAnimatorIK()
    {
        //LEGS
        Foot();
        
    }

    void Foot()
    {
        leftFootWeight = anim.GetFloat("LeftFoot");
        rightFootWeight = anim.GetFloat("RightFoot");
        Debug.Log(leftFootWeight);
        Debug.Log(rightFootWeight);

        leftFootWeight *= 0.8f;
        rightFootWeight *= 0.8f;

        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootWeight);
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootWeight);

        anim.SetIKPosition(AvatarIKGoal.LeftFoot, lFPos);
        anim.SetIKPosition(AvatarIKGoal.RightFoot, rFPos);

        anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftFootWeight);
        anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightFootWeight);

        anim.SetIKRotation(AvatarIKGoal.LeftFoot, lFRot);
        anim.SetIKRotation(AvatarIKGoal.RightFoot, rFRot);
    }
    
}
