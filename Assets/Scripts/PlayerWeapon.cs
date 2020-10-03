﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    //Config
    [SerializeField] Transform muzzlePosition = null;
    [SerializeField] Transform displayContainerPosition = null;

    //Variables
    private GameObject weaponObject = null;
    private string weaponObjectColourName;
    private string weaponObjectShape;
    


    //References
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        //Primary fire, shoot an absorbed object, if one exists.
        //Check we have an object stored first, don't fire if not
        if(weaponObject)
        {
            GameObject hitObject = GetHitObject();
            if(hitObject)
            {
                //Set the hit object properties to match what we have stored
                LevelObject objectProperties = hitObject.GetComponent<LevelObject>();
                objectProperties.SetNewProperties(weaponObjectShape, weaponObjectColourName);
            }
        }
        else
        {
            //Play some error vfx
        }
    }

    public void AltFire()
    {
        //Secondary fire, absorbs the targetted object
        GameObject hitObject = GetHitObject();
        if(hitObject)
        {
            AbsorbObject(hitObject);
        }
    }

    private GameObject GetHitObject()
    {
        RaycastHit hitObject = new RaycastHit();

        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitObject))
        {
            //Debug.Log($"Mouse click on {hitObject.transform.gameObject.name}");
            if(hitObject.transform.tag == "LevelObject")
            {
                return hitObject.transform.gameObject;
            }
            else return null;
        }
        else return null;
    }

    private void AbsorbObject(GameObject objectToAbsorb)
    {
        LevelObject absorbedObject = objectToAbsorb.GetComponent<LevelObject>();
        
        //Disable collision on the object so we can pull it through the front wall
        absorbedObject.SetCollision(false);

        //Pull the object towards us, shrink it and put it in the display container
        StartCoroutine(PullObject(objectToAbsorb, muzzlePosition.position, new Vector3(0.02f, 0.02f, 0.02f), 3f));

        //Tell the object to animate
        absorbedObject.enableRotation = true;
        
        //Destroy an existing object if we have one
        DestroyExistingWeaponObject();

        //Set the absorbed object as our one for firing.
        SetWeaponObjectProperties(objectToAbsorb);
    }

    private IEnumerator PullObject(GameObject objectToPull, Vector3 targetPosition, Vector3 targetScale, float speed)
    {
        while(objectToPull.transform.position != targetPosition)
        {
            objectToPull.transform.position = Vector3.MoveTowards(objectToPull.transform.position, targetPosition, speed * Time.deltaTime);
            objectToPull.transform.localScale = Vector3.Lerp(objectToPull.transform.localScale, targetScale, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        SetAbsorbedObjectInContainer(objectToPull);
    }

    private void SetAbsorbedObjectInContainer(GameObject absorbedObject)
    {
        absorbedObject.transform.SetParent(displayContainerPosition);
        absorbedObject.transform.position = displayContainerPosition.position;
    }

    private void SetWeaponObjectProperties(GameObject absorbedObject)
    {
        weaponObject = absorbedObject;
        weaponObjectColourName = absorbedObject.GetComponent<LevelObject>().colourName;
        weaponObjectShape = absorbedObject.GetComponent<LevelObject>().shapeName;
    }

    private void DestroyExistingWeaponObject()
    {
        if(weaponObject)
        {
            Destroy(weaponObject);
        }
    }
}
