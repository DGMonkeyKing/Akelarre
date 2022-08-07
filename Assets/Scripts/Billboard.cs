using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DGMKCollection.Isometric
{
    public class Billboard : MonoBehaviour
    {
        private Camera _camera;

        // Check rotation in Y to know which sprite show.
        void Awake()
        {
            _camera = Camera.main;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.forward = new Vector3(
                _camera.transform.forward.x, 
                transform.forward.y, 
                _camera.transform.forward.z
            );
        }
    }
}