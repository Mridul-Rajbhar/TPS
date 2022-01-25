using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HumanBones
{
    public HumanBodyBones bone;
    [Range(0, 1)]
    public float weights;
}


public class AiSetTarget : MonoBehaviour
{
    public float reach_angle = 100;
    public HumanBones[] bones;
    public Transform[] boneTransforms;
    public Transform gunAim;
    [Range(0, 1)]

    public Vector3 offset;
    public bool insight = false;
    AiAgent aiAgent;
    public Transform target;

    //public GameObject testGameobject;
    public Vector3 defaultPosition;

    public RaycastHit raycastHit;

    void initializeBones(ref HumanBones[] bones, ref Transform[] boneTransform)
    {
        for (int i = 0; i < bones.Length; i++)
        {
            boneTransforms[i] = aiAgent.GetComponent<Animator>().GetBoneTransform(bones[i].bone);
        }
    }
    private void Awake()
    {

        aiAgent = GetComponent<AiAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {

        boneTransforms = new Transform[bones.Length];

        initializeBones(ref bones, ref boneTransforms);

    }

    void RotateBody(Vector3 playerDirection)
    {
        Quaternion aim = Quaternion.FromToRotation(transform.forward, playerDirection);
        Quaternion blendedMotion = Quaternion.Slerp(Quaternion.identity, aim, Time.deltaTime * 6f);
        transform.rotation *= blendedMotion;
    }

    void aimAtTarget(Transform bone, Transform targetTransform, float weight)
    {

        Vector3 aimDirection = gunAim.forward;
        Vector3 targetDirection = targetTransform.position - gunAim.position;


        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        Quaternion blendedRotation = Quaternion.Slerp(Quaternion.identity, aimTowards, weight);
        bone.rotation *= blendedRotation;


    }
    private void Update()
    {
        //Debug.DrawRay(gunAim.position, gunAim.forward * 10f, Color.red);
    }



    // Update is called once per frame
    void LateUpdate()
    {
        if (gunAim == null)
            return;
        Vector3 direction = gunAim.forward;
        Debug.DrawRay(gunAim.position, direction * aiAgent.attackDistance, Color.black);
        if (Physics.Raycast(gunAim.position, direction, out raycastHit, aiAgent.attackDistance))
        {
            if (!raycastHit.collider.transform.root.CompareTag("Player"))
            {
                insight = false;
                //Debug.Log(raycastHit.collider.name);
            }
            else
            {
                insight = true;
            }
        }


        if (aiAgent.stateMachine.currentState == AiStateId.Attack)
        {


            target.transform.position = aiAgent.playerTransform.position + offset;
            //hips.transform.LookAt(aiAgent.playerTransform.position);
            Vector3 playerDirection = aiAgent.playerTransform.position - transform.position;
            Vector3 newDirection = new Vector3(playerDirection.x, 0, playerDirection.z);
            Debug.DrawRay(transform.position, transform.forward * 10f, Color.red);
            Debug.DrawRay(transform.position, playerDirection * 10f, Color.green);
            reach_angle = Vector3.Angle(transform.forward, playerDirection);
            if (reach_angle > 1f)
                RotateBody(newDirection);

            // for (int j = 0; j < 30; j++)
            // {
            //     for (int i = 0; i < boneTransforms.Length; i++)
            //     {
            //         aimAtTarget(boneTransforms[i], target, bones[i].weights * overallWeight);
            //     }
            // }

        }
        else
        {
            target.localPosition = Vector3.Lerp(target.localPosition, defaultPosition, 5 * Time.deltaTime);
        }

    }
}
