using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BlueCarController : MonoBehaviour
{

    private const string SUBMIT = "Submit";
    private const string SUBMITTWO = "SubmitTwo";


    private float SubmitInput;
    private float SubmitTwoInput;
    private float currentsteerAngle;
    private float steerAngle;
    private float breakForce;
    private bool isBreaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float currentbreakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    public Vector3 com;
    public Rigidbody rb;
    public float m_Thrust = 20f;

    // Boost system
    public float stamina = 100.0f;
    public Slider staminaBar;
    public TextMeshProUGUI staminaText;
    float staminaDecreaseRate = 20.0f;
    bool Boosting;
    // Boost system ^^^

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = com;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SocCar");
        }

        // Boost system
        staminaBar.value = stamina;
        int roundedStamina = (int)stamina;
        staminaText.text = "" + roundedStamina;
        RefillStamina(10.0f);

        if (Input.GetKey(KeyCode.Mouse0) && roundedStamina > 0)
        {
            Boosting = true;
            UseStamina(staminaDecreaseRate * Time.deltaTime);
            BoostBlueCar();
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Boosting = false;
        }
        // Boost system
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    // Boost methods

    void UseStamina(float amount)
    {
        stamina -= amount;
        if (stamina < 0)
        {
            stamina = 0;
        }
    }

    void RefillStamina(float refillRate)
    {
        if (stamina < 100 && Boosting == false)
        {
            stamina += refillRate * Time.deltaTime;
        }
    }

    private void BoostBlueCar()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //rb.AddForce(0, 0, m_Thrust, ForceMode.Acceleration);
            rb.AddForce(m_Thrust * transform.forward, ForceMode.VelocityChange);
        }
    }

    // Boost methods ^^^

    private void GetInput()
    {
        SubmitInput = Input.GetAxis(SUBMIT);
        SubmitTwoInput = Input.GetAxis(SUBMITTWO);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = SubmitTwoInput * motorForce;
        frontRightWheelCollider.motorTorque = SubmitTwoInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentsteerAngle = maxSteerAngle * SubmitInput;
        frontLeftWheelCollider.steerAngle = currentsteerAngle;
        frontRightWheelCollider.steerAngle = currentsteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}