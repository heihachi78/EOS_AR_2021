using UnityEngine;
using UnityEngine.Rendering;

public class PortalController : MonoBehaviour
{
    public Material[] materials;
    private Camera mainCamera;
    public Material skyboxMaterial;
    public float doorWidthForCollisionCheck = 1f;

    private bool wasInFront;
    private bool changeWorld;
    private bool isInside;

    private void Start()
    {
        mainCamera = Camera.main;
        foreach (Material material in materials)
        {
            material.SetInt("_StencilTest", (int)CompareFunction.Equal);
        }
        wasInFront = false;
        changeWorld = false;
        isInside = false;
    }

    private bool GetIsInFront()
    {
        Vector3 pos = mainCamera.transform.position;
        pos += mainCamera.transform.forward * mainCamera.nearClipPlane;
        bool isInFront = false;
        isInFront = (transform.InverseTransformPoint(pos).z > 0) ? true : false;
        isInFront = isInFront && (Mathf.Abs(transform.InverseTransformPoint(pos).x) < doorWidthForCollisionCheck) ? true : false;
        return isInFront;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "MainCamera") return;
        wasInFront = GetIsInFront();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "MainCamera") return;

        if ((wasInFront && !GetIsInFront()) || (!wasInFront && GetIsInFront()))
        {
            changeWorld = true;
        }
        else
        {
            changeWorld = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "MainCamera") return;
        changeWorld = false;
    }

    private void Update()
    {
        if (changeWorld)
        {
            isInside = !isInside;
            wasInFront = GetIsInFront();
            changeWorld = false;
        }
        if (!isInside)
        {
            foreach (Material material in materials)
            {
                material.SetInt("_StencilTest", (int)CompareFunction.Equal);
            }
        }
        else
        {
            foreach (Material material in materials)
            {
                material.SetInt("_StencilTest", (int)CompareFunction.NotEqual);
            }
        }
    }

    private void OnDestroy()
    {
        foreach (Material material in materials)
        {
            material.SetInt("_StencilTest", (int)CompareFunction.NotEqual);
        }
        skyboxMaterial.SetInt("_StencilTest", (int)CompareFunction.Equal);
    }

}
