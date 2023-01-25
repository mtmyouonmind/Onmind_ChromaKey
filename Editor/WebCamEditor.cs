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
        //웹캠 타겟
        webCam = target as WebCam;

        //카메라번호불러오기
        cameraNum = webCam.cameraSelect;

        //프로퍼티
        display = serializedObject.FindProperty("display");
        webcamCamera = serializedObject.FindProperty("webcamCamera");
        virtualCamera = serializedObject.FindProperty("virtualCamera");
        matchBox = serializedObject.FindProperty("matchBox");

        //저장된포지션불러오기
        webcamEditorPos = webCam.webcamCamPos;
        virtualEditorPos = webCam.virtualCamPos;
        matchBoxEditorPos = webCam.matchBoxPos;
        matchBoxEditorScl = webCam.matchBoxScl;
        webcamResetPos = webCam.webcamResetPos;
        virtualResetPos = webCam.virtualResetPos;
        matchResetPos = webCam.matchResetPos;
        matchResetScl = webCam.matchResetScl;

        //키불러오기
        inputPlayKey = webCam.playKeyCode;
        inputStopKey = webCam.stopKeyCode;
    }

    public override void OnInspectorGUI()
    {

        webCam = target as WebCam;

        //[웹캠타겟]

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("[웹캠타겟]");
        EditorGUILayout.Space();

        EditorGUI.indentLevel = 2;
        EditorGUILayout.PropertyField(display, new GUIContent("웹캠타겟"));
        EditorGUI.indentLevel = 0;




        //[웹캠 컨트롤 버튼]

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("[웹캠컨트롤]");
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

        //웹캠 선택영역        
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("웹캠 선택(Test 첫번째는 0, 두번째는 1");
        cameraNum = EditorGUILayout.IntField(cameraNum);
        webCam.cameraSelect = cameraNum;
        GUILayout.EndHorizontal();

        //웹캠 조작 키 조절영역
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("시작 단축 키");
        inputPlayKey = EditorGUILayout.TextField(inputPlayKey);
        webCam.playKeyCode=inputPlayKey;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("정지 단축 키");
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



        //[웹캠영역]

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("[웹캠]");
        EditorGUILayout.Space();
        

        EditorGUI.indentLevel = 2;
        EditorGUILayout.PropertyField(webcamCamera, new GUIContent("웹캠"));
        //웹캠 트랜스폼 위치이동 및 저장

        webcamEditorPos = EditorGUILayout.Vector3Field("위치", webcamEditorPos);
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
        showWebcamReset = EditorGUILayout.BeginFoldoutHeaderGroup(showWebcamReset, "리셋영역 수정");
        GUILayout.EndHorizontal();

        if (showWebcamReset)
        {
            webcamResetPos = EditorGUILayout.Vector3Field("리셋위치", webcamResetPos);
            webCam.webcamResetPos = webcamResetPos;
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUI.indentLevel = 0;


        //버츄얼캠영역

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("[3D캠,버츄얼캠]");
        EditorGUILayout.Space();

        EditorGUI.indentLevel = 2;
        EditorGUILayout.PropertyField(virtualCamera, new GUIContent("3D캠,버추얼캠"));
        //버츄얼캠 트랜스폼 위치이동 및 저장
        virtualEditorPos = EditorGUILayout.Vector3Field("위치", virtualEditorPos);
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
        showVirtualReset = EditorGUILayout.BeginFoldoutHeaderGroup(showVirtualReset, "리셋영역 수정");
        GUILayout.EndHorizontal();

        if (showVirtualReset)
        {
            virtualResetPos = EditorGUILayout.Vector3Field("리셋위치", virtualResetPos);
            webCam.virtualResetPos = virtualResetPos;
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUI.indentLevel = 0;



        //박스영역

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("[매치박스]");
        EditorGUILayout.Space();

        EditorGUI.indentLevel = 2;
        EditorGUILayout.PropertyField(matchBox, new GUIContent("매치박스"));

        //매치박스트랜스폼 위치,크기저장
        matchBoxEditorPos = EditorGUILayout.Vector3Field("위치",matchBoxEditorPos);
        matchBoxEditorScl = EditorGUILayout.Vector3Field("크기", matchBoxEditorScl);

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
        showBoxReset = EditorGUILayout.BeginFoldoutHeaderGroup(showBoxReset, "리셋영역 수정");
        GUILayout.EndHorizontal();
        if (showBoxReset)
        {
            matchResetPos = EditorGUILayout.Vector3Field("리셋위치", matchResetPos);
            webCam.matchResetPos = matchResetPos;
            matchResetScl = EditorGUILayout.Vector3Field("리셋크기", matchResetScl);
            webCam.matchResetScl = matchResetScl;
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUI.indentLevel = 0;

        serializedObject.ApplyModifiedProperties();




    }
    
}
