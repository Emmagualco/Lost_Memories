using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EG
{
    public class Cameras : MonoBehaviour
{
    public GameObject camOne;
    public GameObject camTwo;
      [SerializeField]
    Transform playerCamera;
    //[SerializeField]
    //float mouseSensitivity = 2.5f;
    //[SerializeField]
    //[Range(0.0f, 0.5f)]
    //float mouseSmoothTime = 0.03f;
    //[SerializeField]
    //[Range(0.0f, 0.5f)]
    //float moveSmoothTime = 0.3f;
    //[SerializeField]
    //bool lockCursor = true;
    //Vector2 currentMouseDelta = Vector2.zero;
    //Vector2 currentMouseDeltaVelocity = Vector2.zero;

    //float cameraPitch = 0.0f;
    void Start()
    {
        camOne.SetActive(true);
        camTwo.SetActive(false);
       
    }


    void Update()
    {
        ChangeCamera();
        //UpdateMouseLook();
            //Look();
    }
    //void UpdateMouseLook()
    //{
    //    Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    //    targetMouseDelta.Normalize();

    //    currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

    //    cameraPitch -= currentMouseDelta.y * mouseSensitivity;


    //    cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

    //    playerCamera.localEulerAngles = Vector3.right * cameraPitch;

    //    transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    //}
        //private void Look()
        //{
        //    transform.Rotate(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        //}
        void ChangeCamera()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
           
            if (camOne.activeInHierarchy)
            {

                camOne.SetActive(false);
                camTwo.SetActive(true);
                
            }
            else
            {
                camOne.SetActive(true);
                camTwo.SetActive(false);
               
            }
        }
    }
}
}
