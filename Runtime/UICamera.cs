using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace URPRenderUI
{

    [ExecuteAlways]
    [DisallowMultipleComponent]
    public class UICamera : MonoBehaviour
    {
        private Camera m_MainCamera;
        [SerializeField]
        private Canvas m_Canvas = null;
        [SerializeField]
        private Camera m_UICamera = null; // 仅Editor用

        public Matrix4x4 projectionMatrix { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            if (m_Canvas != null)
                m_MainCamera = m_Canvas.worldCamera;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            var frustumHeight = 2.0f * m_Canvas.planeDistance * Mathf.Tan(m_MainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
            var orthoSize = frustumHeight / 2f;
            float aspect = (float)Screen.width / Screen.height;
            projectionMatrix = Matrix4x4.Ortho(-orthoSize * aspect, orthoSize * aspect, -orthoSize, orthoSize, m_MainCamera.nearClipPlane, m_MainCamera.farClipPlane);

#if UNITY_EDITOR
            if (m_UICamera != null)
                m_UICamera.projectionMatrix = projectionMatrix;
#endif
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (m_Canvas != null)
                m_MainCamera = m_Canvas.worldCamera;
        }
#endif
    }
}
