using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

/// <summary>
/// 웹캠에서 캡처된 이미지를 처리하여 콜백 함수에 전달하는 함수
/// </summary>
public class Preprocess : MonoBehaviour
{
    RenderTexture renderTexture; // 이미지 처리를 위한 중간 렌더 텍스처를 저장
    Vector2 scale = new Vector2(1, 1); // 이미지 크기 조정을 위한 스케일 벡터
    Vector2 offest = Vector2.zero; // 이미지 자르기를 위한 오프셋 벡터

    UnityAction<byte[]> callback; // 처리된 이미지 데이터를 전달받을 콜백 함수 저장

    /// <summary>
    /// 웹캠 텍스처를 받아 크기 조정 및 자르기 작업을 수행하고, 결과를 콜백 함수로 전달하는 메서드.
    /// </summary>
    public void ScaleAndCropImage(WebCamTexture webCamTexture, int desiredSize, UnityAction<byte[]> callback)
    {
        this.callback = callback; // 전달받은 콜백 함수를 멤버 변수에 저장

        if (renderTexture == null) // 렌더 텍스처가 초기화 되지 않았다면,
        {
            renderTexture = new RenderTexture(desiredSize, desiredSize, 0, RenderTextureFormat.ARGB32); // 지정된 크기와 포맷으로 새 렌더 텍스처 생성
        }

        scale.x = (float)webCamTexture.height / (float)webCamTexture.width; // 웹캠 텍스처의 종횡비 계산 후 스케일 벡터에 저장.
        offest.x = (1 - scale.x) / 2f; // 스케일 조정에 따른 오프셋 값 계산
        Graphics.Blit(webCamTexture, renderTexture, scale, offest); // 웹캠 텍스처를 렌더 텍스처로 복사하면서 크기 조정 및 오프셋을 적용
        AsyncGPUReadback.Request(renderTexture, 0, TextureFormat.RGB24, OnCompleteReadback); // 처리된 이미지의 비동기 GPU 읽기를 요청, 완료시 호출될 메서드를 지정
    }

    /// <summary>
    /// 비동기 GPU 읽기가 완료되었을 때 호출되는 메서드
    /// </summary>
    /// <param name="request"></param>
    void OnCompleteReadback(AsyncGPUReadbackRequest request)
    {
        if (request.hasError) // 읽기 요청에 에러가 생겼다면,
        {
            Debug.Log("GPU 읽기 요청에 에러가 발생했습니다."); // 로그 출력
            return; // 메서드 종료
        }

        callback.Invoke(request.GetData<byte>().ToArray()); // 오류가 없다면, 읽어온 데이터를 바이트 배열로 변환하여 콜백 함수에 전달
    }
}
