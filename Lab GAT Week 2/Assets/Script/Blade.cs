using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Collider bladeCollider;
    private Camera mainCam;
    private TrailRenderer trailRenderer;

    public float sliceForce = 5f;
    public float minSliceVelocity = 0.01f;
    public Vector3 direction { get; private set; }

    private bool slicing;

    // Start is called before the first frame update
    void Awake()
    {
        bladeCollider = GetComponent<Collider>();
        mainCam = Camera.main;
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopSlice();
    }
    private void OnDisable()
    {
        StopSlice();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("a");
            StartSlice();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlice();
        }
        else if(slicing)
        {
            ContinueSlice();
        }
    }

    private void StartSlice()
    {
        Vector3 newPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0f;
        transform.position = newPos;

        slicing = true;
        bladeCollider.enabled = true;
        trailRenderer.enabled = true;
        trailRenderer.Clear();
    }
    private void StopSlice()
    {
        slicing = false;
        bladeCollider.enabled = false;
        trailRenderer.enabled = false;
    }
    private void ContinueSlice()
    {
        Vector3 newPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0f;

        direction = newPos - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;

        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPos;
    }
}
