using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Barracuda;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 웹캠으로부터 실시간으로 캡처된 이미지 데이터를 받아, 이미지를 분류한 뒤 UI에 표시하는 함수
/// </summary>
public class Classification : MonoBehaviour
{

    const int IMAGE_SIZE = 150; // 모델 입력 데이터 크기를 상수로 정의
    const string INPUT_NAME = "conv2d_input"; // 모델 입력 레이어의 이름
    const string OUTPUT_NAME = "dense_2"; // 모델 출력 레이어의 이름

    [Header("Model Stuff")] // 인스펙터 창에서 구분을 위한 헤더 추가
    public NNModel modelFile;
    public TextAsset labelAsset;
    

    [Header("Scene Stuff")]
    public CameraView cameraView;
    public Preprocess preprocess;
    public Text text;
    public Text nothingText;

    public Shopbutton shopbutton;
    bool aiButtonCool = false;
    public Text aiCoolText;
    public RawImage cam;
   public bool camOn = false;
    

    string[] labels; // 분류 레이블 저장해줄 배열
    IWorker worker; // 모델을 실행할 바라쿠다 워커 인터페이스

    void Start()
    {
        
        var model = ModelLoader.Load(modelFile); // 모델 파일 로드
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, model); // GPU 사용하는 바라쿠다 워커 생성
        LoadLabels();
    }

    /// <summary>
    /// 레이블을 로드하는 메서드
    /// </summary>
    void LoadLabels()
    {
        var stringArray = labelAsset.text.Split('"').Where((item, index) => index % 2 != 0); // 레이블 텍스트에서 따옴표(")로 둘러싸인 항목만 선택
        labels = stringArray.Where((x, i) => i % 2 != 0).ToArray(); // 선택된 항목 중에서 홀수 인덱스의 항목만을 배열로 저장
    }

    void Update()
    {
        WebCamTexture webCamTexture = cameraView.GetCamImage(); // 카메라 뷰에서 웹캠 텍스처 가져옴
        { 
            if (cam.gameObject.activeInHierarchy)
            {camOn = true;
                if (camOn == true && webCamTexture.didUpdateThisFrame && webCamTexture.width > 100) // 웹캠 텍스처가 이번 프레임에 업데이트 되었고, 너비가 100보다 크면, 
                {
                    preprocess.ScaleAndCropImage(webCamTexture, IMAGE_SIZE, RunModel);
                }

            }
            else
            {
                camOn = false;
            }
          
           
        }
        
    }

    /// <summary>
    /// 모델을 실행해주는 메서드
    /// </summary>
    void RunModel(byte[] pixels)
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(RunModelRoutine(pixels)); // 모델 실행을 위한 코루틴 시작
        }
        
    }

    /// <summary>
    /// 모델 실행 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator RunModelRoutine(byte[] pixels)
    {
        
        


            Tensor tensor = TransformInput(pixels);
            var inputs = new Dictionary<string, Tensor> { { INPUT_NAME, tensor } };// 입력 레이어 이름과
            worker.Execute(inputs);//모델 실행
            Tensor outputTensor = worker.PeekOutput();//출력 레이어에서 결과 텐서를 가져옴
                                                      // 가장 높은 확률 값을 가진 레이어의 인덱스 찾기
            List<float> temp = outputTensor.ToReadOnlyArray().ToList();
            float max = temp.Max(); // 결과 배열 중 가장 큰값
            int index = temp.IndexOf(max); // 큰 값의 인덱스

            text.text = labels[index];//UI 텍스트에 분류 결과를 표시

            tensor.Dispose(); // 텐서 해제
            outputTensor.Dispose(); // 출력 텐서 해제


            yield return null; // 다음 프레임까지 실행 중지
        
    }

    /// <summary>
    /// 입력 이미지 데이터를 모델 입력 포맷에 맞게 변환하는 메서드
    /// </summary>
    /// <param name="pixels"></param>
    /// <returns></returns>
    Tensor TransformInput(byte[] pixels)
    {
        float[] transformedPixels = new float[pixels.Length]; // 변환된 픽셀 값을 저장할 배열

        for (int i = 0; i < transformedPixels.Length; i++) // 모든 픽셀에 대해,
        {
            transformedPixels[i] = (pixels[i] - 127f) / 128f; // 0~255 범위의 픽셀 값을 -1에서 1값으로 변환
            // todo: 전처리 과정 0~1로 수정
            // Link to: https://github.com/onnx/models/tree/main/validated/vision/classification/efficientnet-lite4#preprocessing-steps
        }

        return new Tensor(1, IMAGE_SIZE, IMAGE_SIZE, 3, transformedPixels); // 변환된 픽셀 값으로 텐서 생성
    }
   
    public void GetSeed()
    {
        if (gameObject.activeSelf && aiButtonCool == false) // 게임 오브젝트가 활성화되어 있는지 확인
        {
            StartCoroutine(Capture());
            
            switch (text.text)
            {
                case "Carrot":                   

                    shopbutton.vagetableSeed[0] += 5;
                    shopbutton.seedText[0].text = shopbutton.vagetableSeed[0].ToString();
                    StartCoroutine(CoolTime(600f));
                    break;
                case "Cabbage":                  

                    shopbutton.vagetableSeed[1] += 5;
                    shopbutton.seedText[1].text = shopbutton.vagetableSeed[1].ToString();
                    StartCoroutine(CoolTime(900f));
                    break;
                case "Pumpkin":                  

                    shopbutton.vagetableSeed[2] += 5;
                    shopbutton.seedText[2].text = shopbutton.vagetableSeed[2].ToString();
                    StartCoroutine(CoolTime(1200f));
                    break;
                case "Tomato":                  
                   

                    shopbutton.vagetableSeed[3] += 5;
                    shopbutton.seedText[3].text = shopbutton.vagetableSeed[3].ToString();
                    StartCoroutine(CoolTime(1500f));
                    break;
                default:
                    StartCoroutine(nothingSeed());
                    nothingText.text = "없는 씨앗 입니다.";
                    break;

                    

            }

           
        }
    }
    IEnumerator CoolTime(float a)
    {
        aiButtonCool = true;

        while (a > 0)
        {
            int minutes = Mathf.FloorToInt(a / 60); // 분 계산
            int seconds = Mathf.RoundToInt(a % 60); // 초 계산
            aiCoolText.text = "대기시간:"+ minutes.ToString("D2") + "분 " + seconds.ToString("D2") + "초"; // 분과 초로 표시

            yield return new WaitForSeconds(1f); // 1초 대기
            a--; // 남은 시간 감소
        }

        aiCoolText.text = "씨앗 획득하기"; // 타이머 종료 시 00:00으로 표시
        aiButtonCool = false;
    }
    IEnumerator Capture()
    {
        cam.color = new Color(0, 0, 0, 255);
        yield return new WaitForSeconds(0.2f);
        cam.color = new Color(255, 255, 255, 255);
    }
    IEnumerator nothingSeed()
    {
        nothingText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        nothingText.gameObject.SetActive(false);
    }
}
