//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BallPhysics : MonoBehaviour
//{
//    [SerializeField]
//    private Vector3 m_vTargetPos;
//    [SerializeField]
//    private Vector3 m_vInitialVel;
//    [SerializeField]
//    private bool m_bDebugKickBall = false;

//    private Rigidbody m_rb = null;
//    private GameObject m_TargetDisplay = null;

//    private bool m_bIsGrounded = true;

//    private float m_fDistanceToTarget = 0f;

//    private Vector3 vDebugHeading;

//    // Start is called before the first frame update
//    void Start()
//    {
//        // Get reference to own rigidbody
//        m_rb = GetComponent<Rigidbody>();

//        // Make the target display object
//        CreateTargetDisplay();
//        m_fDistanceToTarget = (m_TargetDisplay.transform.position - transform.position).magnitude;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (m_TargetDisplay != null && m_bIsGrounded)
//        {
//            m_TargetDisplay.transform.position = m_vTargetPos;
//            vDebugHeading = m_vTargetPos - transform.position;
//        }

//        if (m_bDebugKickBall && m_bIsGrounded)
//        {
//            m_bDebugKickBall = false;
//            OnKickBall();
//        }
//    }

//    private void CreateTargetDisplay()
//    {
//        m_TargetDisplay = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
//        m_TargetDisplay.transform.position = Vector3.zero;
//        m_TargetDisplay.transform.localScale = new Vector3(1.0f, 0.1f, 1.0f);
//        m_TargetDisplay.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

//        m_TargetDisplay.GetComponent<Renderer>().material.color = Color.red;
//        m_TargetDisplay.GetComponent<Collider>().enabled = false;
//    }

//    public void OnKickBall()
//    {
//        // FORMULAS FOR PROJECTILE
//        // H = Vi^2 * sin^2(theta) / 2g
//        // R = 2Vi^2 * cos(theta) * sin(theta) / g

//        // Vi = sqrt(2gh) / sin(tan^-1(4h/r))
//        // theta = tan^-1(4h/r)

//        // Vy = V * sin(theta)
//        // Vz = V * cos(theta)

//        float fMaxHeight = m_TargetDisplay.transform.position.y;
//        float fRange = (m_fDistanceToTarget * 2);
//        float fTheta = Mathf.Atan((4 * fMaxHeight) / (fRange));

//        float fInitVelMag = Mathf.Sqrt((2 * Mathf.Abs(Physics.gravity.y) * fMaxHeight)) / Mathf.Sin(fTheta);

//        m_vInitialVel.y = fInitVelMag * Mathf.Sin(fTheta);
//        m_vInitialVel.z = fInitVelMag * Mathf.Cos(fTheta);

//        m_rb.velocity = m_vInitialVel;
//    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawLine(transform.position + vDebugHeading, transform.position);
//    }
//}



using UnityEngine.Assertions;
using UnityEngine;
using UnityEngine.UI;

public class BallPhysics : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_vTargetPos;
    [SerializeField]
    private Vector3 m_vInitialVel;
    [SerializeField]
    private bool m_bDebugKickBall = false;

    public float chargeLevel = 0f;

    public Text scoreDisplay;
    private int score = 0;

    [SerializeField]
    private float targetMoveSpeed;

    private Rigidbody m_rb = null;
    private GameObject m_TargetDisplay = null;

    private bool m_bIsGrounded = true;

    private float m_fDistanceToTarget = 0f;

    private Vector3 vDebugHeading;

    private Vector3 initialPos;
    private Quaternion initialRot;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        Assert.IsNotNull(m_rb, "Houston, we've got a problem here! No Rigidbody attached");

        CreateTargetDisplay();
        m_fDistanceToTarget = (m_TargetDisplay.transform.position - transform.position).magnitude;

        initialPos = this.transform.position;
        initialRot = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(m_TargetDisplay.transform.position, Vector3.up);

        if (m_TargetDisplay != null && m_bIsGrounded)
        {
            m_TargetDisplay.transform.position = m_vTargetPos;
            vDebugHeading = m_vTargetPos - transform.position;
        }

        if (m_bDebugKickBall && m_bIsGrounded)
        {
            m_bDebugKickBall = false;
            OnKickBall();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (m_TargetDisplay.activeSelf == false)
            {
                m_TargetDisplay.SetActive(true);
            }
            if (chargeLevel < 10)
            {
               m_TargetDisplay.transform.localScale = new Vector3(chargeLevel * 0.1f, chargeLevel * 0.1f, chargeLevel * 0.1f);
                chargeLevel += Time.deltaTime * 5f;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_bDebugKickBall = true;
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            if (m_vTargetPos.y < 10)
            {
                m_vTargetPos.y += targetMoveSpeed;
            }
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if (m_vTargetPos.y > 0)
            {
                m_vTargetPos.y -= targetMoveSpeed;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (m_vTargetPos.x > -10)
            {
                m_vTargetPos.x -= targetMoveSpeed;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (m_vTargetPos.x < 10)
            {
                m_vTargetPos.x += targetMoveSpeed;
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            Reset();
        }
    }

    private void CreateTargetDisplay()
    {
        m_TargetDisplay = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        m_TargetDisplay.transform.position = Vector3.zero;
        m_TargetDisplay.transform.localScale = new Vector3(1.0f, 0.1f, 1.0f);
        m_TargetDisplay.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        m_TargetDisplay.GetComponent<Renderer>().material.color = Color.red;
        m_TargetDisplay.GetComponent<Collider>().enabled = false;
        m_TargetDisplay.SetActive(false);
    }

    public void OnKickBall()
    {
        // H = Vi^2 * sin^2(theta) / 2g             - MaxHeight
        // R = 2Vi^2 * cos(theta) * sin(theta) / g  - Horizontal Displacement

        // Vi = sqrt(2gH) / sin(tan^-1(4H/R))       - Initial velocity
        // theta = tan^-1(4H/R)                     - Angle

        // Vy = V * sin(theta)                      - Y velocity
        // Vz = V * cos(theta)                      - Z Velocity

        

        float fMaxHeight = m_TargetDisplay.transform.position.y;
        float fRange = (m_fDistanceToTarget * 2);
        float fTheta = Mathf.Atan((4 * fMaxHeight) / (fRange));

        float fInitVelMag = Mathf.Sqrt((2 * Mathf.Abs(Physics.gravity.y) * fMaxHeight)) / Mathf.Sin(fTheta);

        m_vInitialVel.y = (fInitVelMag * Mathf.Sin(fTheta));
        m_vInitialVel.z = fInitVelMag * Mathf.Cos(fTheta);

        Vector3 yVelLocal = m_vInitialVel.y * transform.forward;
        Vector3 zVelLocal = m_vInitialVel.z * transform.forward;

        m_rb.velocity = (yVelLocal + zVelLocal) * (chargeLevel * 0.1f);



    }

    public void Reset()
    {
        m_rb.velocity = Vector3.zero;
        this.transform.position = initialPos;
        this.transform.rotation = initialRot;
        chargeLevel = 0;
        m_TargetDisplay.SetActive(false);


    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "GoalBox")
        {
            Debug.Log("GOALLLLLL");
            Reset();
            score++;
            scoreDisplay.text = score.ToString();
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position + vDebugHeading, transform.position);
    }
}

