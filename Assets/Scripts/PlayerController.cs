using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Config
    
    //Variables

    //References
    PlayerWeapon playerWeapon;

    void Start()
    {
        playerWeapon = GetComponent<PlayerWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        PointPlayerAtMouse();
        HandleMouseClicks();
    }

    private void HandleMouseClicks()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //Left click, fire current object
            playerWeapon.Fire();
        }
        if(Input.GetMouseButtonDown(1))
        {
            //Right click, absorb target object
            playerWeapon.AltFire();
        }
    }

    private void PointPlayerAtMouse()
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
