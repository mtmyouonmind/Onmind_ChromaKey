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
    SerializedProperty virtualCamera;
    SerializedProperty matchBox;
    Vector3 basePos = new Vector3(0, 0, 0);
    Vector3 virtualPhonePos = new Vector3(0, 0, 0);
    Vector3 baseSumVirtualPos = new Vector3(0, 0, 0);
    Vector3 matchBoxPos = new Vector3(0, 0, 0);
    Vector3 baseSumMatchPos = new Vector3(0, 0, 0);
    Vector3 matchBoxScl = new Vector3(0, 0, 0);
    

    private void OnEnable()
    {
        //웹캠 타겟
        webCam = target as WebCam;

        //카메라번호불러오기
        cameraNum = webCam.cameraSelect;

        //프로퍼티
        display = serializedObject.FindProperty("display");
        virtualCamera = serializedObject.FindProperty("virtualCamera");
        matchBox = serializedObject.FindProperty("matchBox");

        //기본 포지션 불러오기
        basePos = webCam.basePosition;
        baseSumVirtualPos = webCam.baseVirtualPos;
        virtualPhonePos = webCam.iphonePos;
        matchBoxPos = webCam.matchBoxPos;
        matchBoxScl = webCam.matchBox.transform.localScale;
        baseSumMatchPos = webCam.baseSumMatchPos;


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

        //기본위치

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("[전체 위치 셋팅]");
        EditorGUILayout.Space();

        EditorGUI.indentLevel = 2;
        EditorGUILayout.PropertyField(virtualCamera, new GUIContent("3D카메라"));

        basePos = EditorGUILayout.Vector3Field("기준 위치", basePos);
        webCam.basePosition = basePos;

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Cam Position Load", GUILayout.MaxWidth(300), GUILayout.MaxHeight(30)))
        {
            basePos = webCam.virtualCamera.position;   
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        EditorGUI.indentLevel = 2;

        EditorGUILayout.Space(30);


        virtualPhonePos = EditorGUILayout.Vector3Field("버추얼카메라의 위치", virtualPhonePos);
        if (virtualCamera != null)
        {
            webCam.iphonePos = virtualPhonePos;
        }

        baseSumVirtualPos = basePos + virtualPhonePos;
        baseSumVirtualPos = EditorGUILayout.Vector3Field("위치 보정 계산",baseSumVirtualPos);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Cam Position Save", GUILayout.MaxWidth(300), GUILayout.MaxHeight(30)))
        {
            webCam.virtualCamera.position = baseSumVirtualPos;
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        EditorGUILayout.Space(30);
        EditorGUILayout.PropertyField(matchBox, new GUIContent("매치박스"));

        matchBoxPos = EditorGUILayout.Vector3Field("매치박스위치", matchBoxPos);
        if (matchBox != null)
        {
            webCam.matchBoxPos = matchBoxPos;
        }

        baseSumMatchPos = basePos + matchBoxPos;
        baseSumMatchPos = EditorGUILayout.Vector3Field("위치 보정 계산", baseSumMatchPos);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Matchbox Position Save", GUILayout.MaxWidth(300), GUILayout.MaxHeight(30)))
        {
            webCam.matchBox.transform.position = baseSumMatchPos;
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        EditorGUILayout.Space(10);

        matchBoxScl = EditorGUILayout.Vector3Field("매치박스크기", matchBoxScl);
        webCam.matchBox.transform.localScale = matchBoxScl;

        EditorGUILayout.Space(30);
        serializedObject.ApplyModifiedProperties();
        #region 이전작성

        ////[웹캠영역]

        //EditorGUILayout.Space(30);
        //EditorGUILayout.LabelField("[웹캠]");
        //EditorGUILayout.Space();


        //EditorGUI.indentLevel = 2;
        //EditorGUILayout.PropertyField(webcamCamera, new GUIContent("웹캠"));

        ////웹캠 트랜스폼 위치이동 및 저장

        //webcamEditorPos = EditorGUILayout.Vector3Field("위치", webcamEditorPos);
        //if (webCam.webcamCamera != null)
        //{
        //    webCam.webcamCamera.transform.position = webcamEditorPos;
        //    webCam.webcamCamPos = webcamEditorPos;
        //}

        //EditorGUILayout.Space();
        //GUILayout.BeginHorizontal();
        //GUILayout.FlexibleSpace();
        //if (GUILayout.Button("Reset", GUILayout.MaxWidth(200), GUILayout.MaxHeight(30)))
        //{
        //    webcamEditorPos = webcamResetPos;
        //    webCam.webcamCamPos = webcamResetPos;
        //}
        //GUILayout.EndHorizontal();

        //GUILayout.BeginHorizontal();
        //GUILayout.FlexibleSpace();
        //showWebcamReset = EditorGUILayout.BeginFoldoutHeaderGroup(showWebcamReset, "리셋영역 수정");
        //GUILayout.EndHorizontal();

        //if (showWebcamReset)
        //{
        //    webcamResetPos = EditorGUILayout.Vector3Field("리셋위치", webcamResetPos);
        //    webCam.webcamResetPos = webcamResetPos;
        //}
        //EditorGUILayout.EndFoldoutHeaderGroup();
        //EditorGUI.indentLevel = 0;


        ////버츄얼캠영역

        //EditorGUILayout.Space(30);
        //EditorGUILayout.LabelField("[3D캠,버츄얼캠]");
        //EditorGUILayout.Space();

        //EditorGUI.indentLevel = 2;
        //EditorGUILayout.PropertyField(virtualCamera, new GUIContent("3D캠,버추얼캠"));
        ////버츄얼캠 트랜스폼 위치이동 및 저장
        //virtualEditorPos = EditorGUILayout.Vector3Field("위치", virtualEditorPos);
        //if (webCam.virtualCamera != null)
        //{
        //    webCam.virtualCamera.transform.position = virtualEditorPos;
        //    webCam.virtualCamPos = virtualEditorPos;
        //}

        //EditorGUILayout.Space();
        //GUILayout.BeginHorizontal();
        //GUILayout.FlexibleSpace();
        //if (GUILayout.Button("Reset", GUILayout.MaxWidth(200), GUILayout.MaxHeight(30)))
        //{
        //    virtualEditorPos = virtualResetPos;
        //    webCam.virtualCamPos = virtualResetPos;
        //}
        //GUILayout.EndHorizontal();

        //GUILayout.BeginHorizontal();
        //GUILayout.FlexibleSpace();
        //showVirtualReset = EditorGUILayout.BeginFoldoutHeaderGroup(showVirtualReset, "리셋영역 수정");
        //GUILayout.EndHorizontal();

        //if (showVirtualReset)
        //{
        //    virtualResetPos = EditorGUILayout.Vector3Field("리셋위치", virtualResetPos);
        //    webCam.virtualResetPos = virtualResetPos;
        //}
        //EditorGUILayout.EndFoldoutHeaderGroup();
        //EditorGUI.indentLevel = 0;

        ////박스영역

        //EditorGUILayout.Space(30);
        //EditorGUILayout.LabelField("[매치박스]");
        //EditorGUILayout.Space();

        //EditorGUI.indentLevel = 2;
        //EditorGUILayout.PropertyField(matchBox, new GUIContent("매치박스"));

        ////매치박스트랜스폼 위치,크기저장
        //matchBoxEditorPos = EditorGUILayout.Vector3Field("위치", matchBoxEditorPos);
        //matchBoxEditorScl = EditorGUILayout.Vector3Field("크기", matchBoxEditorScl);

        //if (webCam.matchBox != null)
        //{
        //    webCam.matchBox.transform.localPosition = matchBoxEditorPos;
        //    webCam.matchBoxPos = matchBoxEditorPos;
        //    webCam.matchBox.transform.localScale = matchBoxEditorScl;
        //    webCam.matchBoxScl = matchBoxEditorScl;
        //}

        //EditorGUILayout.Space();
        //GUILayout.BeginHorizontal();
        //GUILayout.FlexibleSpace();
        //if (GUILayout.Button("Reset", GUILayout.MaxWidth(200), GUILayout.MaxHeight(30)))
        //{
        //    matchBoxEditorPos = matchResetPos;
        //    webCam.matchBoxPos = matchResetPos;
        //    matchBoxEditorScl = matchResetScl;
        //    webCam.matchBoxScl = matchResetScl;
        //}
        //GUILayout.EndHorizontal();

        //GUILayout.BeginHorizontal();
        //GUILayout.FlexibleSpace();
        //showBoxReset = EditorGUILayout.BeginFoldoutHeaderGroup(showBoxReset, "리셋영역 수정");
        //GUILayout.EndHorizontal();
        //if (showBoxReset)
        //{
        //    matchResetPos = EditorGUILayout.Vector3Field("리셋위치", matchResetPos);
        //    webCam.matchResetPos = matchResetPos;
        //    matchResetScl = EditorGUILayout.Vector3Field("리셋크기", matchResetScl);
        //    webCam.matchResetScl = matchResetScl;
        //}
        //EditorGUILayout.EndFoldoutHeaderGroup();
        //EditorGUI.indentLevel = 0;

        //serializedObject.ApplyModifiedProperties();
        #endregion







    }

}
