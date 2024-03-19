using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Barracuda;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ķ���κ��� �ǽð����� ĸó�� �̹��� �����͸� �޾�, �̹����� �з��� �� UI�� ǥ���ϴ� �Լ�
/// </summary>
public class Classification : MonoBehaviour
{

    const int IMAGE_SIZE = 150; // �� �Է� ������ ũ�⸦ ����� ����
    const string INPUT_NAME = "conv2d_input"; // �� �Է� ���̾��� �̸�
    const string OUTPUT_NAME = "dense_2"; // �� ��� ���̾��� �̸�

    [Header("Model Stuff")] // �ν����� â���� ������ ���� ��� �߰�
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
    

    string[] labels; // �з� ���̺� �������� �迭
    IWorker worker; // ���� ������ �ٶ���� ��Ŀ �������̽�

    void Start()
    {
        
        var model = ModelLoader.Load(modelFile); // �� ���� �ε�
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, model); // GPU ����ϴ� �ٶ���� ��Ŀ ����
        LoadLabels();
    }

    /// <summary>
    /// ���̺��� �ε��ϴ� �޼���
    /// </summary>
    void LoadLabels()
    {
        var stringArray = labelAsset.text.Split('"').Where((item, index) => index % 2 != 0); // ���̺� �ؽ�Ʈ���� ����ǥ(")�� �ѷ����� �׸� ����
        labels = stringArray.Where((x, i) => i % 2 != 0).ToArray(); // ���õ� �׸� �߿��� Ȧ�� �ε����� �׸��� �迭�� ����
    }

    void Update()
    {
        WebCamTexture webCamTexture = cameraView.GetCamImage(); // ī�޶� �信�� ��ķ �ؽ�ó ������
        { 
            if (cam.gameObject.activeInHierarchy)
            {camOn = true;
                if (camOn == true && webCamTexture.didUpdateThisFrame && webCamTexture.width > 100) // ��ķ �ؽ�ó�� �̹� �����ӿ� ������Ʈ �Ǿ���, �ʺ� 100���� ũ��, 
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
    /// ���� �������ִ� �޼���
    /// </summary>
    void RunModel(byte[] pixels)
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(RunModelRoutine(pixels)); // �� ������ ���� �ڷ�ƾ ����
        }
        
    }

    /// <summary>
    /// �� ���� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator RunModelRoutine(byte[] pixels)
    {
        
        


            Tensor tensor = TransformInput(pixels);
            var inputs = new Dictionary<string, Tensor> { { INPUT_NAME, tensor } };// �Է� ���̾� �̸���
            worker.Execute(inputs);//�� ����
            Tensor outputTensor = worker.PeekOutput();//��� ���̾�� ��� �ټ��� ������
                                                      // ���� ���� Ȯ�� ���� ���� ���̾��� �ε��� ã��
            List<float> temp = outputTensor.ToReadOnlyArray().ToList();
            float max = temp.Max(); // ��� �迭 �� ���� ū��
            int index = temp.IndexOf(max); // ū ���� �ε���

            text.text = labels[index];//UI �ؽ�Ʈ�� �з� ����� ǥ��

            tensor.Dispose(); // �ټ� ����
            outputTensor.Dispose(); // ��� �ټ� ����


            yield return null; // ���� �����ӱ��� ���� ����
        
    }

    /// <summary>
    /// �Է� �̹��� �����͸� �� �Է� ���˿� �°� ��ȯ�ϴ� �޼���
    /// </summary>
    /// <param name="pixels"></param>
    /// <returns></returns>
    Tensor TransformInput(byte[] pixels)
    {
        float[] transformedPixels = new float[pixels.Length]; // ��ȯ�� �ȼ� ���� ������ �迭

        for (int i = 0; i < transformedPixels.Length; i++) // ��� �ȼ��� ����,
        {
            transformedPixels[i] = (pixels[i] - 127f) / 128f; // 0~255 ������ �ȼ� ���� -1���� 1������ ��ȯ
            // todo: ��ó�� ���� 0~1�� ����
            // Link to: https://github.com/onnx/models/tree/main/validated/vision/classification/efficientnet-lite4#preprocessing-steps
        }

        return new Tensor(1, IMAGE_SIZE, IMAGE_SIZE, 3, transformedPixels); // ��ȯ�� �ȼ� ������ �ټ� ����
    }
   
    public void GetSeed()
    {
        if (gameObject.activeSelf && aiButtonCool == false) // ���� ������Ʈ�� Ȱ��ȭ�Ǿ� �ִ��� Ȯ��
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
                    nothingText.text = "���� ���� �Դϴ�.";
                    break;

                    

            }

           
        }
    }
    IEnumerator CoolTime(float a)
    {
        aiButtonCool = true;

        while (a > 0)
        {
            int minutes = Mathf.FloorToInt(a / 60); // �� ���
            int seconds = Mathf.RoundToInt(a % 60); // �� ���
            aiCoolText.text = "���ð�:"+ minutes.ToString("D2") + "�� " + seconds.ToString("D2") + "��"; // �а� �ʷ� ǥ��

            yield return new WaitForSeconds(1f); // 1�� ���
            a--; // ���� �ð� ����
        }

        aiCoolText.text = "���� ȹ���ϱ�"; // Ÿ�̸� ���� �� 00:00���� ǥ��
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
