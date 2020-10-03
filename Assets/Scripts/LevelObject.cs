using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour
{
    [Header("Properties")]
    private Color32 _materialColour;
    [SerializeField] private string _shapeName = null;
    public string shapeName{get{return _shapeName;} private set{}}
    [SerializeField] private string _colourName = null;
    public string colourName{get{return _colourName;} private set{}}
    [Header("Meshes")]
    [SerializeField] Mesh[] objectMeshes = null;

    private bool _enableRotation = false;
    private float rotationSpeed = 180f;
    public bool enableRotation {get{return _enableRotation;} set{_enableRotation = value;}}
    
    // Start is called before the first frame update
    void Start()
    {
        SetColour();
        SetMesh();
    }

    void Update()
    {
        if(_enableRotation)
        {
            transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
        }
    }

    private void SetMesh()
    {
        MeshFilter objectMesh = GetComponent<MeshFilter>();
        MeshCollider objectCollider = GetComponent<MeshCollider>();

        switch(_shapeName)
        {
            case "square" :
                objectMesh.mesh = objectMeshes[0];
                break;
            case "circle" : objectMesh.mesh = objectMeshes[1];
                break;
            case "triangle" : objectMesh.mesh = objectMeshes[2];
                break;
            default : Debug.Log($"No shape name set on {gameObject.name}");
                break;
        }
        objectCollider.sharedMesh = objectMesh.mesh;
    }

    public void SetCollision(bool collisionEnabled)
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        Collider collider = GetComponent<Collider>();
        if(collisionEnabled == false)
        {
            collider.enabled = false;
            rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            rigidBody.isKinematic = true;
        }
        else
        {
            collider.enabled = true;
            rigidBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rigidBody.isKinematic = false;
        }
    }

    private void SetColour()
    {
        Renderer objectRenderer = GetComponent<Renderer>();
        switch(_colourName)
        {
            case "red" : objectRenderer.material.color = Color.red;
                break;
            case "blue" : objectRenderer.material.color = Color.blue;
                break;
            case "green" : objectRenderer.material.color = Color.green;
                break;
            case "yellow" : objectRenderer.material.color = Color.yellow;
                break;
            default : Debug.Log($"No colour name set on {gameObject.name}");
                break;
        }
    }

    public void SetNewProperties(string newShapeName, string newShapeColour)
    {
        _shapeName = newShapeName;
        _colourName = newShapeColour;
        SetMesh();
        SetColour();
    }

}
