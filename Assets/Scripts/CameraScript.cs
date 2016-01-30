using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public Vector3 myPos;
    public Transform myPlay;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = myPlay.position + myPos;
    }
}
