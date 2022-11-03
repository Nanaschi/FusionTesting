using UnityEngine;


    public class InputController
    {
        private Vector2 _moveInputVector;
        private Vector2 _viewInputVector;

        private void Update()
        {
            //View Input
            _viewInputVector.x = Input.GetAxis("Mouse X");
            _viewInputVector.y = Input.GetAxis("Mouse Y");
            //Move Input 
            _moveInputVector.x = Input.GetAxis("Horizontal");
            _moveInputVector.y = Input.GetAxis("Vertical");
        }
    }
