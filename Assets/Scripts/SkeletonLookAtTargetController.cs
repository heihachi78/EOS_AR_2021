using UnityEngine;

public class SkeletonLookAtTargetController : MonoBehaviour
{
    public Transform lookAtTarget;
    Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        lookAtTarget.position = mainCamera.transform.position;
    }
}
