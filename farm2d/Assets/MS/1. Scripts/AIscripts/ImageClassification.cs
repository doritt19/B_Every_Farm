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

    public int imageSize = 150; // �𵨿� �°� ������ �̹����� ũ��

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
    /// RawImage�� �ε�� �̹��� �ִ��� üũ, ���� ��� Classify �޼��� ȣ��
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
    /// �̹����� � �̹������� �з����ִ� �޼���
    /// </summary>
    /// <param name="image"></param>
    public void Classify(Texture2D image)
    {
        Texture2D resizedImage = Resize(image, imageSize, imageSize);
        Tensor input = TransformInput(resizedImage.GetPixels32(), imageSize, imageSize);

        // worker ����
        worker.Execute(input);

        // ��� ��ȯ
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
        // RT�� GPU �޸𸮿� ������ �ǰ�, �׸� ���ؼ� ������ ������ ����
        // T2D�� CPU���� ���� ����, �̹��� ������ ���� ó��

        RenderTexture.active = rt; // ���� �ؽ��� ����ϱ����� Ȱ��ȭ
        Graphics.Blit(image, rt); // image���� rt�� �̹����� �����ϸ鼭 ũ�⸦ ����.

        Texture2D resizedImage = new Texture2D(width, height);
        resizedImage.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        // rt���� �ȼ� �����͸� ����. resizeImage�� ����. 

        resizedImage.Apply();
        // ����� ������ resizedImage�� ����.

        return resizedImage;
    }

    /// <summary>
    /// �̹����� �� �Է� �ټ����·� ��ȯ���ִ� �޼���
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public Tensor TransformInput(Color32[] pic, int width, int height)
    {
        float[] floatValues = new float[width * height * 3];
        // ����ȭ�� �ȼ� ������ �����ϴµ� ���

        for (int i = 0; i < pic.Length; ++i)
        {
            var color = pic[i]; // �ϳ��� ������ �ִ� �� �ȼ����� (R, G, B)�� �ش��ϴ� ������ ������.
            floatValues[i * 3 + 0] = color.r / 255.0f;
            floatValues[i * 3 + 1] = color.g / 255.0f;
            floatValues[i * 3 + 2] = color.b / 255.0f;
        }

        return new Tensor(1, height, width, 3, floatValues);
        // (batch, height, width, channel, data(����ȭ�� R, G, B ���� ������� �������)
    }
}
