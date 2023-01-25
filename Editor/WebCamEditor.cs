using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;
using JetBrains.Annotations;

[CustomEditor(typeof(WebCam))] 
public class WebCamEditor : Editor
{
    WebCam webCam;
    int cameraNum = 0;
    string inputPlayKey = "a";
    string inputStopKey = "s";
    SerializedProperty display;
    SerializedProperty webcamCamera;
    Vector3 webcamEditorPos = new Vector3(0, 0, 0);
    Vector3 webcamResetPos = new Vector3(0, 0, 0);
    bool showWebcamReset = false;
    SerializedProperty virtualCamera;
    Vector3 virtualEditorPos = new Vector3(0, 0, 0);
    Vector3 virtualResetPos = new Vector3(0, 0, 0);
    bool showVirtualReset = false;
    SerializedProperty matchBox;
    Vector3 matchBoxEditorPos = new Vector3(0, 0, 0 );
    Vector3 matchResetPos = new Vector3(0, 0, 0);
    Vector3 matchBoxEditorScl = new Vector3 (1 , 1, 1);
    Vector3 matchResetScl = new Vector3(1, 1, 1);
    bool showBoxReset = false;
    

    private void OnEnable()
    {
        //��ķ Ÿ��
        webCam = target as WebCam;

        //ī�޶��ȣ�ҷ�����
        cameraNum = webCam.cameraSelect;

        //������Ƽ
        display = serializedObject.FindProperty("display");
        webcamCamera = serializedObject.FindProperty("webcamCamera");
        virtualCamera = serializedObject.FindProperty("virtualCamera");
        matchBox = serializedObject.FindProperty("matchBox");

        //����������Ǻҷ�����
        webcamEditorPos = webCam.webcamCamPos;
        virtualEditorPos = webCam.virtualCamPos;
        matchBoxEditorPos = webCam.matchBoxPos;
        matchBoxEditorScl = webCam.matchBoxScl;
        webcamResetPos = webCam.webcamResetPos;
        virtualResetPos = webCam.virtualResetPos;
        matchResetPos = webCam.matchResetPos;
        matchResetScl = webCam.matchResetScl;

        //Ű�ҷ�����
        inputPlayKey = webCam.playKeyCode;
        inputStopKey = webCam.stopKeyCode;
    }

    public override void OnInspectorGUI()
    {

        webCam = target as WebCam;

        //[��ķŸ��]

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("[��ķŸ��]");
        EditorGUILayout.Space();

        EditorGUI.indentLevel = 2;
        EditorGUILayout.PropertyField(display, new GUIContent("��ķŸ��"));
        EditorGUI.indentLevel = 0;




        //[��ķ ��Ʈ�� ��ư]

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("[��ķ��Ʈ��]");
        EditorGUILayout.Space();

 
        EditorGUI.indentLevel = 2;

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("CamTest", GUILayout.MaxWidth(300), GUILayout.MaxHeight(30)))
        {
            webCam.CamTest();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        //��ķ ���ÿ���        
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("��ķ ����(Test ù��°�� 0, �ι�°�� 1");
        cameraNum = EditorGUILayout.IntField(cameraNum);
        webCam.cameraSelect = cameraNum;
        GUILayout.EndHorizontal();

        //��ķ ���� Ű ��������
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("���� ���� Ű");
        inputPlayKey = EditorGUILayout.TextField(inputPlayKey);
        webCam.playKeyCode=inputPlayKey;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("���� ���� Ű");
        inputStopKey = EditorGUILayout.TextField(inputStopKey);
        webCam.stopKeyCode=inputStopKey;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Start", GUILayout.MaxWidth(300), GUILayout.MaxHeight(30)))
        {
            webCam.StartCam();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Stop", GUILayout.MaxWidth(300), GUILayout.MaxHeight(30)))
        {
            webCam.StopCam();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        EditorGUI.indentLevel = 0;



        //[��ķ����]

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("[��ķ]");
        EditorGUILayout.Space();
        

        EditorGUI.indentLevel = 2;
        EditorGUILayout.PropertyField(webcamCamera, new GUIContent("��ķ"));
        //��ķ Ʈ������ ��ġ�̵� �� ����

        webcamEditorPos = EditorGUILayout.Vector3Field("��ġ", webcamEditorPos);
        if (webCam.webcamCamera != null)
        {
            webCam.webcamCamera.transform.position = webcamEditorPos;
            webCam.webcamCamPos = webcamEditorPos;
        }

        EditorGUILayout.Space();
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Reset", GUILayout.MaxWidth(200), GUILayout.MaxHeight(30)))
        {
            webcamEditorPos = webcamResetPos;
            webCam.webcamCamPos = webcamResetPos;
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        showWebcamReset = EditorGUILayout.BeginFoldoutHeaderGroup(showWebcamReset, "���¿��� ����");
        GUILayout.EndHorizontal();

        if (showWebcamReset)
        {
            webcamResetPos = EditorGUILayout.Vector3Field("������ġ", webcamResetPos);
            webCam.webcamResetPos = webcamResetPos;
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUI.indentLevel = 0;


        //�����ķ����

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("[3Dķ,�����ķ]");
        EditorGUILayout.Space();

        EditorGUI.indentLevel = 2;
        EditorGUILayout.PropertyField(virtualCamera, new GUIContent("3Dķ,���߾�ķ"));
        //�����ķ Ʈ������ ��ġ�̵� �� ����
        virtualEditorPos = EditorGUILayout.Vector3Field("��ġ", virtualEditorPos);
        if (webCam.virtualCamera != null)
        {
            webCam.virtualCamera.transform.position = virtualEditorPos;
            webCam.virtualCamPos = virtualEditorPos;
        }

        EditorGUILayout.Space();
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Reset", GUILayout.MaxWidth(200), GUILayout.MaxHeight(30)))
        {
            virtualEditorPos = virtualResetPos;
            webCam.virtualCamPos = virtualResetPos;
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        showVirtualReset = EditorGUILayout.BeginFoldoutHeaderGroup(showVirtualReset, "���¿��� ����");
        GUILayout.EndHorizontal();

        if (showVirtualReset)
        {
            virtualResetPos = EditorGUILayout.Vector3Field("������ġ", virtualResetPos);
            webCam.virtualResetPos = virtualResetPos;
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUI.indentLevel = 0;



        //�ڽ�����

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("[��ġ�ڽ�]");
        EditorGUILayout.Space();

        EditorGUI.indentLevel = 2;
        EditorGUILayout.PropertyField(matchBox, new GUIContent("��ġ�ڽ�"));

        //��ġ�ڽ�Ʈ������ ��ġ,ũ������
        matchBoxEditorPos = EditorGUILayout.Vector3Field("��ġ",matchBoxEditorPos);
        matchBoxEditorScl = EditorGUILayout.Vector3Field("ũ��", matchBoxEditorScl);

        if (webCam.matchBox != null)
        {
            webCam.matchBox.transform.localPosition = matchBoxEditorPos;
            webCam.matchBoxPos = matchBoxEditorPos;
            webCam.matchBox.transform.localScale = matchBoxEditorScl;
            webCam.matchBoxScl = matchBoxEditorScl;
        }

        EditorGUILayout.Space();
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Reset", GUILayout.MaxWidth(200), GUILayout.MaxHeight(30)))
        {
            matchBoxEditorPos = matchResetPos;
            webCam.matchBoxPos = matchResetPos;
            matchBoxEditorScl = matchResetScl;
            webCam.matchBoxScl = matchResetScl;
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        showBoxReset = EditorGUILayout.BeginFoldoutHeaderGroup(showBoxReset, "���¿��� ����");
        GUILayout.EndHorizontal();
        if (showBoxReset)
        {
            matchResetPos = EditorGUILayout.Vector3Field("������ġ", matchResetPos);
            webCam.matchResetPos = matchResetPos;
            matchResetScl = EditorGUILayout.Vector3Field("����ũ��", matchResetScl);
            webCam.matchResetScl = matchResetScl;
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUI.indentLevel = 0;

        serializedObject.ApplyModifiedProperties();




    }
    
}
