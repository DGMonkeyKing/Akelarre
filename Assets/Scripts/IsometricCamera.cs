using System.Collections;
using UnityEngine;

namespace DGMKCollection.Isometric
{
    public class IsometricCamera : MonoBehaviour
    {
        private Transform cameraPivot;

        [SerializeField]
        private Camera _camera;

        // Start is called before the first frame update
        void Awake()
        {
            cameraPivot = transform;
            if(_camera == null) _camera = Camera.main;
            //LoadCameraConfiguration();
        }

        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

#region CAMERA_MOVEMENTS
        public void Tilt(float degrees, float time = 0f, bool auto = false)
        {
            if(auto) StartCoroutine(TiltCoroutine(degrees, time));
            else cameraPivot.eulerAngles += new Vector3(-degrees, 0, degrees);
        }

        public void Panning(float degrees, float time = 0f, bool auto = false)
        {
            if(auto) StartCoroutine(PanningCoroutine(degrees, time));
            else cameraPivot.eulerAngles += new Vector3(0, degrees, 0);
        }

        public void Zoom(float distance, float time = 0f, bool auto = false)
        {
            if(auto) StartCoroutine(ZoomCoroutine(distance, time));
        }
        private IEnumerator ZoomCoroutine(float distance, float time)
        {
            float time_ms = time * 1000;
            Vector3 position = _camera.transform.localPosition;
            float acc_position = 0f;
            float new_position = 0f;

            float tmp_time = 0f;

            while(tmp_time <= time_ms)
            {
                tmp_time += Time.time;
                // Nos quedamos solo con el diferencial entre los dos frames.
                acc_position = Mathf.Lerp(0, distance, tmp_time/time_ms);
                new_position = acc_position - new_position;

                _camera.transform.Translate(Vector3.forward * new_position);

                yield return 0;

                new_position = acc_position;
            }
        }

        private IEnumerator PanningCoroutine(float degrees, float time)
        {
            float time_ms = time * 1000;
            float y_angle = cameraPivot.rotation.y;
            float acc_y = 0f;
            float new_y = 0f;

            float tmp_time = 0f;

            while(tmp_time <= time_ms)
            {
                tmp_time += Time.time;
                // Nos quedamos solo con el diferencial entre los dos frames.
                acc_y = Mathf.Lerp(0, degrees, tmp_time/time_ms);
                new_y = acc_y - new_y;

                cameraPivot.eulerAngles += Vector3.up * new_y;

                yield return 0;

                new_y = acc_y;
            }
        }

        private IEnumerator TiltCoroutine(float degrees, float time)
        {
            float time_ms = time * 1000;
            float x_angle = cameraPivot.rotation.x;
            float acc_x = 0f;
            float new_x = 0f;

            float tmp_time = 0f;

            while(tmp_time <= time_ms)
            {
                tmp_time += Time.time;
                // Nos quedamos solo con el diferencial entre los dos frames.
                acc_x = Mathf.Lerp(0, degrees, tmp_time/time_ms);
                new_x = acc_x - new_x;
                
                cameraPivot.localEulerAngles += Vector3.right * new_x;

                yield return 0;

                new_x = acc_x;
            }
        }

#endregion

#region CAMERA_CONFIGURATION
        private void LoadCameraConfiguration()
        {
            _camera.transform.position = new Vector3(
                PlayerPrefs.GetFloat("Camera_Position_x", 0f), 
                PlayerPrefs.GetFloat("Camera_Position_y", 0f),
                PlayerPrefs.GetFloat("Camera_Position_z", 0f)
            );

            _camera.transform.rotation = new Quaternion(
                PlayerPrefs.GetFloat("Camera_Rotation_x", 0f), 
                PlayerPrefs.GetFloat("Camera_Rotation_y", 0f),
                PlayerPrefs.GetFloat("Camera_Rotation_z", 0f),
                PlayerPrefs.GetFloat("Camera_Rotation_w", 0f)
            );
        }

        public void SaveCameraConfiguration()
        {
            PlayerPrefs.SetFloat("Camera_Position_x", _camera.transform.position.x);
            PlayerPrefs.SetFloat("Camera_Position_y", _camera.transform.position.y);
            PlayerPrefs.SetFloat("Camera_Position_z", _camera.transform.position.z);
            
            PlayerPrefs.SetFloat("Camera_Rotation_x", _camera.transform.rotation.x);
            PlayerPrefs.SetFloat("Camera_Rotation_y", _camera.transform.rotation.y);
            PlayerPrefs.SetFloat("Camera_Rotation_z", _camera.transform.rotation.z);
            PlayerPrefs.SetFloat("Camera_Rotation_w", _camera.transform.rotation.w);
        }
#endregion
    }
}