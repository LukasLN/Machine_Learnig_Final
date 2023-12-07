using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class SimpleCarAgent : Agent
{
    public float CarSpeed = 5f;
    public float CarRotSpeed = 20f;

    public float forwardInput;
    public float RotInput;

    private Rigidbody rb;

    [SerializeField] private GameObject RedLight_Wall;
    [SerializeField] private Transform GreenLight_Wall;
    [SerializeField] private GameObject targetTransform;
    [SerializeField] private GameObject passengerMesh;
    [SerializeField] private GameObject TraficLight;
    [SerializeField] private Transform EndGoal;

    [SerializeField] private Material winMat;
    [SerializeField] private Material loseMat;
    [SerializeField] private MeshRenderer floorMeshRenderer;

    bool haveHitOnce = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandleMovements();
    }


    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0.8f, 0.26f, -3.6f);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        passengerMesh.transform.localPosition = new Vector3(1.494f, 0.365f, Random.Range(-0.1f, -1.5f));
        targetTransform.SetActive(true);
        passengerMesh.SetActive(true);
        haveHitOnce = false;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Vertical");
        continuousActions[1] = Input.GetAxis("Horizontal");
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.transform.localPosition);
        sensor.AddObservation(TraficLight.GetComponent<TraficLight>().IsGreenLight_Int);
        sensor.AddObservation(EndGoal.localPosition);
        sensor.AddObservation(RedLight_Wall.transform.localPosition);
        sensor.AddObservation(GreenLight_Wall.localPosition);

    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveFB = actions.ContinuousActions[0];
        float moveSTS = actions.ContinuousActions[1];
        forwardInput = moveFB;
        RotInput = moveSTS;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Passenger")
        {
            AddReward(1f);
            targetTransform.SetActive(false);
            passengerMesh.SetActive(false);
        }
        if (other.tag == "Penalty")
        {
            AddReward(-3f);
            floorMeshRenderer.material = loseMat;
            EndEpisode();
        }
        if (other.tag == "GreenLight")
        {
            if (haveHitOnce == false)
            {
                AddReward(1f);
                haveHitOnce = true;
            }
        }
        if (other.tag == "RedLight")
        {
            AddReward(-2f);
            floorMeshRenderer.material = loseMat;
            EndEpisode();
        }
        if (other.tag == "Destination")
        {
            AddReward(1f);
            floorMeshRenderer.material = winMat;
            EndEpisode();
        }
    }


    #region Custom Methods
    protected virtual void HandleMovements()
    {
        // Move Car Forward
        Vector3 wantedPosition = transform.position + (transform.forward * forwardInput * CarSpeed * Time.deltaTime);
        rb.MovePosition(wantedPosition);

        // Rotate Car
        Quaternion wantedRotation = transform.rotation * Quaternion.Euler(Vector3.up * (CarRotSpeed * RotInput * Time.deltaTime));
        rb.MoveRotation(wantedRotation);
    }
    #endregion

}
