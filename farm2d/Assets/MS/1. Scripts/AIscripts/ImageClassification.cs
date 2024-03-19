using System.Collections;
using System.Collections.Generic;
using Unity.Barracuda;
using UnityEngine;
using UnityEngine.UI;

public class ImageClassification : MonoBehaviour
{
    public NNModel modelAsset;
    private Model runtimeModel;
    private IWorker worker;
    public RawImage image;
    public Button classifyButton;
    public Text displayClassText;
    public Text displayPercentageText;

    public int imageSize = 150; // 모델에 맞게 조절할 이미지의 크기

    private readonly string[] classLabels = { "Bean", "Bitter_Gourd", "Bottle_Gourd", "Brinjal",
                                              "Broccoli", "Cabbage", "Capsicum", "Carrot", "Cauliflower",
                                              "Cucumber", "Papaya", "Potato", "Pumpkin", "Radish", "Tomato" };

    // Start is called before the first frame update
    void Start()
    {
        runtimeModel = ModelLoader.Load(modelAsset);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, runtimeModel);

        classifyButton.onClick.AddListener(ClassifiyCurrentImage);
    }

    /// <summary>
    /// RawImage에 로드된 이미지 있는지 체크, 있을 경우 Classify 메서드 호출
    /// </summary>
    private void ClassifiyCurrentImage()
    {
        Texture2D inputImage = (Texture2D)image.texture;
        if (inputImage != null)
        {
            Classify(inputImage);
        }
        else
        {
            Debug.Log("No image is loaded.");
        }
    }

    /// <summary>
    /// 이미지가 어떤 이미지인지 분류해주는 메서드
    /// </summary>
    /// <param name="image"></param>
    public void Classify(Texture2D image)
    {
        Texture2D resizedImage = Resize(image, imageSize, imageSize);
        Tensor input = TransformInput(resizedImage.GetPixels32(), imageSize, imageSize);

        // worker 실행
        worker.Execute(input);

        // 결과 반환
        Tensor output = worker.PeekOutput();

        float[] scores = output.ToReadOnlyArray();
        for (int i = 0; i < scores.Length; i++)
        {
            Debug.Log($"{classLabels[i]}: {scores[i] * 100}%");
        }

        int maxIndex = output.ArgMax()[0];
        float maxProbaility = scores[maxIndex] * 100f;
        Debug.Log($"maxProbaility: {maxProbaility}");

        displayClassText.text = classLabels[maxIndex];
        displayPercentageText.text = maxProbaility.ToString() + "%";

        input.Dispose();
        output.Dispose();
        Destroy(resizedImage);
    }

    void OnDestroy()
    {
        worker.Dispose();
    }

    public Texture2D Resize(Texture2D image, int width, int height)
    {
        RenderTexture rt = new RenderTexture(width, height, 24);
        // RenderTexture vs Texture2D
        // RT는 GPU 메모리에 저장이 되고, 그를 통해서 랜더링 연산이 가능
        // T2D는 CPU에서 접근 가능, 이미지 데이터 저장 처리

        RenderTexture.active = rt; // 랜더 텍스쳐 사용하기위해 활성화
        Graphics.Blit(image, rt); // image에서 rt로 이미지를 복사하면서 크기를 조정.

        Texture2D resizedImage = new Texture2D(width, height);
        resizedImage.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        // rt에서 픽셀 데이터를 읽음. resizeImage에 저장. 

        resizedImage.Apply();
        // 변경된 사항을 resizedImage에 적용.

        return resizedImage;
    }

    /// <summary>
    /// 이미지를 모델 입력 텐서형태로 변환해주는 메서드
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public Tensor TransformInput(Color32[] pic, int width, int height)
    {
        float[] floatValues = new float[width * height * 3];
        // 정규화된 픽셀 값들을 저장하는데 사용

        for (int i = 0; i < pic.Length; ++i)
        {
            var color = pic[i]; // 하나의 사진에 있는 각 픽셀들의 (R, G, B)에 해당하는 정보를 가져옴.
            floatValues[i * 3 + 0] = color.r / 255.0f;
            floatValues[i * 3 + 1] = color.g / 255.0f;
            floatValues[i * 3 + 2] = color.b / 255.0f;
        }

        return new Tensor(1, height, width, 3, floatValues);
        // (batch, height, width, channel, data(정규화된 R, G, B 값이 순서대로 들어있음)
    }
}
