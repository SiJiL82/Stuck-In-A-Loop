using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Config
    [SerializeField] GameObject targetObject = null;

    //Variables

    //References

    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        Ray mousePositionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane gameSpacePlane = new Plane(Vector3.back, Vector3.zero);
        float rayEntry = 0.0f;
        
        if(gameSpacePlane.Raycast(mousePositionRay, out rayEntry))
        {
            Vector3 rayHitPoint = mousePositionRay.GetPoint(rayEntry);
            transform.LookAt(rayHitPoint);
        }
    }
}
