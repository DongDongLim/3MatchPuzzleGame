using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라 해상도를 고정하기 위한 클래스(카메라 부착)
public class CameraResolution : MonoBehaviour
{
    [SerializeField]
    // 사용자 설정 너비율
    int setWidth;

    [SerializeField]
    // 사용자 설정 높이
    int setHeight;


    private void Awake()
    {
        Camera cam = GetComponent<Camera>();

        // 기기 너비 저장
        int deviceWidth = Screen.width;
        // 기기 높이 저장
        int deviceHeight = Screen.height;

        // SetResolution(너비, 높이, 전체화면)
        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true);

        // 기기의 해상도 비가 더 큰 경우(가로가 더 길다면)
        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight)
        {
            // 새로운 너비
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight);
            // 새로운 Rect 적용
            cam.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f);
        }
        // 게임의 해상도 비가 더 큰 경우(세로가 더 길다면)
        else if((float)setWidth / setHeight > (float)deviceWidth / deviceHeight)
        {
            // 새로운 높이
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight);
            // 새로운 Rect 적용
            cam.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight);
        }
    }
}
