using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
[DisallowMultipleComponent]
public class UICameraSync : MonoBehaviour
{
    private Camera m_UICamera;
    private Camera m_MainCamera;
    [SerializeField]
    private Canvas m_Canvas;

    // Start is called before the first frame update
    void Start()
    {
        m_UICamera = GetComponent<Camera>();
        if (m_Canvas == null)
            m_Canvas = FindObjectOfType<Canvas>();
        m_MainCamera = m_Canvas.worldCamera;
    }

    // Update is called once per frame
    void Update()
    {
        var frustumHeight = 2.0f * m_Canvas.planeDistance * Mathf.Tan(m_MainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        m_UICamera.orthographicSize = frustumHeight / 2f;
    }
}
