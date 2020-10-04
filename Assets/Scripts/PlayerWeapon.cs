using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class PlayerWeapon : MonoBehaviour
{
    //Config
    [Header("Hardpoints")]
    [SerializeField] Transform muzzlePosition = null;
    [SerializeField] Transform displayContainerPosition = null;
    [Header("SFX")]
    [SerializeField] AudioClip sfxPrimaryFire = null;
    [SerializeField] float sfxPrimaryFireVolume = 0.5f;
    [SerializeField] AudioClip sfxSecondaryFire = null;
    [SerializeField] float sfxSecondaryFireVolume = 0.5f;
    [SerializeField] AudioClip sfxAltFire = null;
    [SerializeField] float sfxAltFireVolume = 0.5f;
    [SerializeField] AudioClip sfxErrorFire = null;
    [SerializeField] float sfxErrorFireVolume = 0.5f;
    [Header("VFX")]
    [SerializeField] GameObject vfxFireErrorPrefab = null;


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

    public void PrimaryFire()
    {
        //Primary fire, change targetted object's shape.
        Fire("shape");
    }

    public void SecondaryFire()
    {
        //Secondary fire, change targetted object's colour.
        Fire("colour");
    }

    public void Fire(string fireMode)
    {
        //Check we have an object stored first, don't fire if not
        GameObject hitObject = GetHitObject();
        if(hitObject)
            {
                if(weaponObject)
                {
                    //Set the hit object's shape to match what we have stored
                    LevelObject objectProperties = hitObject.GetComponent<LevelObject>();
                    if(fireMode == "shape")
                    {
                        objectProperties.shapeName = weaponObjectShape;
                    }
                    else if(fireMode == "colour")
                    {
                        objectProperties.colourName = weaponObjectColourName;
                    }
                    //play FX
                    PlayFireSFX(sfxPrimaryFire, sfxPrimaryFireVolume);
                    PlayContainerVFX(vfxFireErrorPrefab, "yellow", 6f);
                }
                else
                {
                    //Play some error vfx
                    PlayContainerErrorVFX();
                    PlayContainerErrorSFX();
                }
            }
    }

    private void PlayContainerErrorVFX()
    {
        PlayContainerVFX(vfxFireErrorPrefab, "red", 6f);
    }

    private void PlayContainerErrorSFX()
    {
        PlayFireSFX(sfxErrorFire, sfxErrorFireVolume);
    }

    public void AltFire()
    {
        //Absorbs the targetted object
        GameObject hitObject = GetHitObject();
        if(hitObject)
        {
            AbsorbObject(hitObject);
            //play FX
            PlayFireSFX(sfxAltFire, sfxAltFireVolume);
        }
    }

    private void PlayContainerVFX(GameObject vfxToPlay, string colourName, float intensity)
    {
        GameObject vfxObject = Instantiate(vfxToPlay, displayContainerPosition.position, Quaternion.identity, displayContainerPosition);
        VisualEffect vfx = vfxObject.GetComponent<VisualEffect>();
        ExposedProperty colourProperty = "Colour";
        Vector4 colourToSet;
        float hdrMultiplication = Mathf.Pow(2, intensity);
        colourToSet = (Color)typeof(Color).GetProperty(colourName.ToLowerInvariant()).GetValue(null, null);

        colourToSet = colourToSet * hdrMultiplication;
        
        vfx.SetVector4(colourProperty, colourToSet);
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

    private void PlayFireSFX(AudioClip clipToPlay, float clipVolume)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        
        audioSource.PlayOneShot(clipToPlay, clipVolume);
    }
}
