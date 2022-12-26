// Use this script on a Camera create a fixed size gradient mesh that alwyas follow the camera and renders as a background.

using UnityEngine;
using FFStudio;

// Use a unique namespace to ensure there are no name conflicts
namespace Imphenzia
{
    // Execute in edit mode so gradient is updated if the color gradient is changed
    [ExecuteInEditMode]

    // This class inherits from GradientSkyCommon.cs where there is common and reusable code
    public class GradientSkyCamera : GradientSkyCommon
    {
        // Allows to select if the gradient should be positioned at Near or Far clipping plane - it has no visual difference, only in the scene window
        public enum ClippingPlane { NEAR, FAR }
        public ClippingPlane placeAtClippingPlane = ClippingPlane.NEAR;

        // Reference to the child gameobject that has the mesh - this is not to pollute the Camera object with mesh related components.
        // Hide in inspector - we don't need to see it there (it can't be set to private/protected since those are not serialzied and
        // kept between play/edit mode).
        public GameObject childObject;
        public Camera _camera;

        // Private variables to reference camera, camera cached settings, and clipping plane
        private float _cacheFieldOfView;
        private int _cacheCameraWidth;
        private int _cacheCameraHeight;
        private ClippingPlane _cacheClippingPlane = ClippingPlane.NEAR;

        /// <summary>
        /// Creates (or gets the) child gameobject
        /// </summary>
        public void CreateOrGetChildObject()
        {

                childObject.name = "sky";

                // Reset the local rotation so it's "zero"
                childObject.transform.localRotation = Quaternion.identity;


                // Place the gameobject just behind the near clipping plane or in front of the far clipping plane
                if ( placeAtClippingPlane == ClippingPlane.NEAR )
                    childObject.transform.localPosition = new Vector3(0, 0, _camera.nearClipPlane + 0.01f);
                else
                    childObject.transform.localPosition = new Vector3(0, 0, _camera.farClipPlane - 0.01f);

                // Create a MeshRenderer for the childObject
                CreateMeshRenderer(childObject.transform);

                // Set the Material to the shader with ZWrite off and RenderQueue set to Gemetry-1000 to ensure it's always rendered as a background
                Material _material = new Material(Shader.Find("Custom/VertexColorCamera"));
                childObject.GetComponent< MeshRenderer >().sharedMaterial = _material;
            // Set the cached clipping plane to the clipping plane used so we can compare if it's changed
            _cacheClippingPlane = placeAtClippingPlane;

            // Create the gradient mesh and sett it to the sharedMesh for the MeshFilter
            childObject.GetComponent< MeshFilter >().sharedMesh = CreateMesh();

            // Set the mesh local scale to ensure it fills the camera based on its position and camera field of view and size
            SetMeshLocalScale();
        }

        /// <summary>
        /// Sets the scale of the mesh based on camera properties to ensure it convers the entire viewport/screen and nothing more.
        /// </summary>
        void SetMeshLocalScale()
        {
            // ResetAspect needs to be called to ensure that aspect ratio is updated when changing between aspect ratios and between edit/play
            _camera.ResetAspect();

            // Set the local scale width/height
            childObject.transform.localScale = new Vector3( _camera.orthographicSize * GameSettings.Instance.sky_gradient_size, _camera.orthographicSize / _camera.aspect * GameSettings.Instance.sky_gradient_size, 1);

            // Update the cache for increased performance
            _cacheFieldOfView = _camera.orthographicSize;
            _cacheCameraWidth = _camera.pixelWidth;
            _cacheCameraHeight = _camera.pixelHeight;
        }

    }

}