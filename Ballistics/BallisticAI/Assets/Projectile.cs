using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    public GameObject target;
    public float launchForce;
    Rigidbody rb;

    public Text vel;
    public Text velVector;
    public Text pos;
    public Text dist;
    public Text time;

    float? travelTime;

    //Call Time.timeScale to readjust time
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        FiringSolution launchSolve = new FiringSolution();

        Nullable<Vector3> launchVector = launchSolve.Calculate(transform.position, target.transform.position, launchForce, Physics.gravity);
        if (launchVector.HasValue)
        {
            travelTime = launchSolve.ttt;
            rb.AddForce(launchVector.Value.normalized * launchForce, ForceMode.VelocityChange);
        }

    }

    private void Update()
    {
        vel.text = rb.velocity.magnitude.ToString();
        velVector.text = rb.velocity.ToString();
        pos.text = transform.position.ToString();
        dist.text = (transform.position - target.transform.position).magnitude.ToString();
        time.text = (travelTime - Time.timeSinceLevelLoad).ToString();

        if (transform.position.y < -10f)
            SceneManager.LoadScene(0);
    }
}
