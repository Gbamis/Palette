using UnityEngine;
using System.Collections;

namespace HT
{
    public class ShepheredFOV : MonoBehaviour
    {

        private Mesh _mesh;
        private Vector3[] _vertices;
        private int[] _triangles;

        private const int resolution = 40;
        private float view_distance;
        private float fov;

        public void ShowFOV(float _fov, float distance)
        {

            fov = _fov;
            view_distance = distance;
            _mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = _mesh;

            _vertices = new Vector3[resolution + 2];
            _triangles = new int[resolution * 3];

            float start_angle = 90 + (fov / 2);
            float end_angle = 90 - (fov / 2);
            _vertices[0] = Vector3.zero;
            _vertices[1] = GetVectorFromAngle(start_angle);
            _vertices[resolution + 1] = GetVectorFromAngle(end_angle);

            int iter = resolution - 1;
            float space = (fov / resolution);
            float lastAngle = start_angle;
            for (int i = 0; i < iter; i++)
            {
                float newAngle = lastAngle - space;
                _vertices[i + 2] = GetVectorFromAngle(newAngle);
                lastAngle = newAngle;
            }

            for (int i = 0; i < resolution; i++)
            {
                _triangles[i * 3] = 0;
                _triangles[i * 3 + 1] = i + 1;
                _triangles[i * 3 + 2] = i + 2;
            }


            _mesh.vertices = _vertices;
            _mesh.triangles = _triangles;
        }

        private Vector3 GetVectorFromAngle(float angleInDeg)
        {

            float angleInRad = angleInDeg * Mathf.Deg2Rad;
            float x = Mathf.Cos(angleInRad) * view_distance;
            float z = Mathf.Sin(angleInRad) * view_distance;
            return new Vector3(x, 0, z);
        }

    }

}