using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EG
{
    public class FootLogic : MonoBehaviour
    {
        public CharacterControl cControl;
        // Start is called before the first frame update
        void Start()
        {
          
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Ground")
            {
                cControl.canJump = true;
            }  
            
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Ground")
            {
                cControl.canJump = false;
            }
           
        }
    }
}