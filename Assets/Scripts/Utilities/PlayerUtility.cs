using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class PlayerUtility
{
    public TriggerEffect curTriggerEffect;
    private MeshFilter meshFilter;
    private Rigidbody rBody;
    public Text textField;

    private Vector2 inputVector;
    private Vector3 movementVector;
    public float moveSpeed;


    /// <summary>
    /// Class Internal Initializer
    /// </summary>
    /// <param name="_thisGO"></param>
    public void Initialize(GameObject _thisGO)
    {
        rBody = _thisGO.GetComponent<Rigidbody>();
        meshFilter = _thisGO.GetComponentInChildren<MeshFilter>();
        textField = rBody.GetComponentInChildren<Text>();

        curTriggerEffect = TriggerEffect.Default;
        FSM();
    }

    /// <summary>
    /// Class Internal Fixed Update
    /// </summary>
    public void FixedTick()
    {
        GetInput();
        Locomotion();
        //Rotation();
    }

    /// <summary>
    /// Updates character locomotion using Transform.Translate
    /// </summary>
    private void Locomotion()
    {
        rBody.transform.Translate(movementVector);
    }

    private void Rotation()
    {
       // rBody.transform.Rotate()
    }

    /// <summary>
    /// Gets Input from the Vertical & Horizontal Axes & stores them in our movement vector
    /// </summary>
    private void GetInput()
    {
        inputVector = new Vector2(Input.GetAxis(Statics.Horizontal), Input.GetAxis(Statics.Vertical));
        movementVector = new Vector3(inputVector.x * moveSpeed * Time.deltaTime, 0, inputVector.y * moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Run Finite State Machine
    /// </summary>
    public void FSM()
    {
        Debug();
        switch (curTriggerEffect)
        {
            case TriggerEffect.T_Volume1:
                OverrideMesh(PrimitiveType.Sphere);
                ScaleMesh(Vector3.one);
                OverrideColor(Color.yellow);
                OverrideTextField(Statics.Trigger1_Msg);
                break;
            case TriggerEffect.T_Volume2:
                OverrideMesh(PrimitiveType.Cube);
                ScaleMesh(Vector3.one);
                OverrideColor(Color.red);
                OverrideTextField(Statics.Trigger2_Msg);
                break;
            case TriggerEffect.T_Volume3:
                OverrideMesh(PrimitiveType.Sphere);
                ScaleMesh(new Vector3(1, 0.5f, 1));
                OverrideColor(Color.white);
                OverrideTextField(Statics.Trigger3_Msg);
                break;
            case TriggerEffect.Default:
                OverrideMesh(PrimitiveType.Sphere);
                ScaleMesh(Vector3.one);
                OverrideColor(Color.white);
                OverrideTextField(Statics.Default_Msg);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Override Mesh
    /// </summary>
    /// <param name="_meshType"></param>
    private void OverrideMesh(PrimitiveType _meshType)
    {
        GameObject temp = GameObject.CreatePrimitive(_meshType);
        meshFilter.mesh = temp.GetComponent<MeshFilter>().mesh;
        Object.Destroy(temp.gameObject);    
    }

    /// <summary>
    /// Override Collider Type
    /// </summary>
    /// <param name="_meshType"></param>
    private void OverrideCollider(PrimitiveType _meshType)
    {
        switch (_meshType)
        {
            case PrimitiveType.Sphere:
                meshFilter.gameObject.AddComponent<SphereCollider>();
                break;
            case PrimitiveType.Capsule:
                meshFilter.gameObject.AddComponent<CapsuleCollider>();
                break;
            case PrimitiveType.Cylinder:
                meshFilter.gameObject.AddComponent<CapsuleCollider>();
                break;
            case PrimitiveType.Cube:
                meshFilter.gameObject.AddComponent<BoxCollider>();
                break;
            case PrimitiveType.Plane:
                meshFilter.gameObject.AddComponent<MeshCollider>();
                break;
            case PrimitiveType.Quad:
                meshFilter.gameObject.AddComponent<MeshCollider>();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Override Mesh Color
    /// </summary>
    /// <param name="_color"></param>
    private void OverrideColor(Color _color)
    {
        meshFilter.GetComponent<Renderer>().material.color = _color;
    }

    /// <summary>
    /// Override Mesh Scale
    /// </summary>
    /// <param name="_scale"></param>
    private void ScaleMesh(Vector3 _scale)
    {
        meshFilter.gameObject.transform.localScale = _scale;
    }

    /// <summary>
    /// Override Text String
    /// </summary>
    /// <param name="_msg"></param>
    private void OverrideTextField(string _msg)
    {
        textField.text = _msg;
    }

    public void Debug()
    {
        UnityEngine.Debug.Log(curTriggerEffect);
    }

}
