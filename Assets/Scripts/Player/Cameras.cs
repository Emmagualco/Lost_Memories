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
        float mouseSensitivity = 2.5f;
        [SerializeField]
        [Range(0.0f, 0.5f)]
        float mouseSmoothTime = 0.03f;
        [SerializeField]
    Transform playerCamera;
        float cameraPitch = 0.0f;
        Vector2 currentMouseDelta = Vector2.zero;
        Vector2 currentMouseDeltaVelocity = Vector2.zero;

        void Start()
    {
        camOne.SetActive(true);
        camTwo.SetActive(false);
       
    }


    void Update()
    {
        ChangeCamera();
           
    }
 
   
    void ChangeCamera()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
           
            if (camTwo.activeInHierarchy)
            {
                    camOne.SetActive(true);
                    camTwo.SetActive(false);
                    UpdateMouseLook();


            }
            else
            {

                    camOne.SetActive(false);
                    camTwo.SetActive(true);



                }
        }
    }
        void UpdateMouseLook()
        {
            Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            targetMouseDelta.Normalize();

            currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

            cameraPitch -= currentMouseDelta.y * mouseSensitivity;


            cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

            playerCamera.localEulerAngles = Vector3.right * cameraPitch;

            transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
        }

    }
}
