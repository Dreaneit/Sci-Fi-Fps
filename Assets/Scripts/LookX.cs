using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookX : MonoBehaviour
{
    [SerializeField]
    private float sensivity = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var lookX = Input.GetAxis("Mouse X");

        Vector3 newEulerAngles = transform.localEulerAngles;
        newEulerAngles.y += lookX * sensivity;
        transform.localEulerAngles = newEulerAngles;
    }
}
