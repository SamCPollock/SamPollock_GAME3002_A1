//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ArrowProjectile : MonoBehaviour
//{
//    // Launch variables
//    [SerializeField]
//    private Transform TargetObject;
//    [Range(1.0f, 6.0f)] public float TargetRadius;
//    [Range(20.0f, 75.0f)] public float LaunchAngle;

//    [Range(0f, 10f)] public float TargetHeightOffsetFromGround;
//    public bool RandomizeHeightOffset;

//    private bool bTargetReady;

//    private Rigidbody rigid;
//    private Vector3 initialPosition;
//    private Quaternion initialRotation;


//    // Start is called before the first frame update
//    void Start()
//    {
//        rigid = GetComponent<Rigidbody>();
//        bTargetReady = true;
//        initialPosition = transform.position;
//        initialRotation = transform.rotation;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            if (bTargetReady)
//            {
//                Launch();
//            }
//            else
//            {
//                ResetToInitialState();
//                SetNewTarget();
//            }
//        }
//    }

//    void Launch()
//    {
//        Vector3 projectileXZpos = new Vector3(transform.position.x, 0.0f, transform.position.z);
//        Vector3 targetXZpos = new Vector3(TargetObject.position.x, 0.0f, TargetObject.position.z);

//        transform.LookAt(targetXZpos);

//        float R = Vector3.Distance(projectileXZpos, targetXZpos);
//        float G = Physics.gravity.y;
//        float tanAlpha = Mathf.Tan(LaunchAngle * Mathf.Deg2Rad);
//        float H = (TargetObject.position.y + GetPlatformOffset())


//        bTargetReady = false;
//    }

//    void SetNewTarget()
//    {
//        Transform targetTF = TargetObject.GetComponent<Transform>();





//        Vector3 rotationAxis = Vector3.up;
//        float randomAngle = Random.Range(0.0f, 360.0f);
//        Vector3 randomVectorOnGroundPlane = Quaternion.AngleAxis(randomAngle, rotationAxis) * Vector3.right;


//        float heightOffset = (RandomizeHeightOffset ? Random.Range(0.2f, 1.0f) : 1.0f) * TargetHeightOffsetFromGround;
//        float aboveOrBelowGround = (Random.Range(0f, 1f) > 0.5f ? 1.0f : -1.0f);

//        Vector3 heightOffsetVector = new Vector3(0, heightOffset, 0) * aboveOrBelowGround;

//        Vector3 randomPoint = randomVectorOnGroundPlane * TargetRadius + heightOffsetVector;

//        //Vector3 randomPoint = randomVectorOnGroundPlane * TargetRadius + new Vector3(0, targetTF.position.y, 0);



//        TargetObject.SetPositionAndRotation(randomPoint, targetTF.rotation);
//        bTargetReady = true;
//    }

//    void ResetToInitialState()
//    {
//        rigid.velocity = Vector3.zero;
//        this.transform.SetPositionAndRotation(initialPosition, initialRotation);
//        bTargetReady = false;
//    }
//}
