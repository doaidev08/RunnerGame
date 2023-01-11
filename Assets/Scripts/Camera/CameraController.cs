using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    private Vector3 offSet;
    private float smooth = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offSet  = transform.position - target.position; 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(transform.position.x,transform.position.y,offSet.z + target.position.z);
        this.transform.position = Vector3.Lerp(transform.position, newPosition, smooth);
    }
}
