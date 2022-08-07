using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DGMKCollection.Isometric
{
    public class Billboard : MonoBehaviour
    {
        private Camera _camera;

        [SerializeField]
        private bool fixX = false;
        [SerializeField]
        private bool fixY = true;
        [SerializeField]
        private bool fixZ = false;

        // Check rotation in Y to know which sprite show.
        void Awake()
        {
            _camera = Camera.main;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            float x = (fixX) ? transform.forward.x : _camera.transform.forward.x;
            float y = (fixY) ? transform.forward.y : _camera.transform.forward.y;
            float z = (fixZ) ? transform.forward.z : _camera.transform.forward.z;

            transform.forward = new Vector3(x, y, z);
        }
    }
}