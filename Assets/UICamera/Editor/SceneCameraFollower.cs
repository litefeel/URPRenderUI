using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneCameraFollower : EditorWindow
{
    private Camera m_TargetCamera;
    private bool m_Lock = true;
    
    private void OnGUI()
    {
        m_TargetCamera = (Camera)EditorGUILayout.ObjectField("TargetCamera", m_TargetCamera, typeof(Camera), true);
        m_Lock = EditorGUILayout.Toggle("Lock Scene Camera", m_Lock);
    }

    private void OnEnable()
    {
        if (m_TargetCamera == null)
        {
            var canvas = GameObject.FindObjectOfType<Canvas>();
            if (canvas != null)
                m_TargetCamera = canvas.worldCamera;
        }

        SceneView.duringSceneGui -= OnSceneGUI;
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (!m_TargetCamera || !m_Lock) return;

        var sceneCamera = sceneView.camera;
        sceneCamera.transform.position = m_TargetCamera.transform.position;
        sceneCamera.transform.rotation = m_TargetCamera.transform.rotation;
        SceneView.lastActiveSceneView.LookAtDirect(m_TargetCamera.transform.position, m_TargetCamera.transform.rotation);
    }


    [MenuItem("Window/Scene Camera Follwer")]
    private static void Init()
    {
        GetWindow<SceneCameraFollower>().Show();
    }
}
