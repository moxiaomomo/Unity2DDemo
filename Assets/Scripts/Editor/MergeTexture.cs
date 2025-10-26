using UnityEngine;
using UnityEditor;
using System.IO;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent;

/// <summary>
/// ������Сͼ�ϲ���һ��PNG����֡
/// </summary>
public class MergeTexture : EditorWindow
{
    [MenuItem("Tools/MergeTexture")]
    static void DoMergeTexture()
    {
        // �ϲ����ͼ������ؿ��
        const int maxWidth = 1024;

        // ��ȡ��ѡͼƬ
        // Texture2D selectedImg = Selection.ac as Texture2D;

        Object[] selectedObjects = Selection.objects;
        if (selectedObjects == null || selectedObjects.Length <= 0)
        {
            Debug.Log("�ϲ�δ��ɣ�δѡ���κ���Դ");
        }

        foreach (Object obj in selectedObjects)
        {
            string assetPath = AssetDatabase.GetAssetPath(obj);
            if (!AssetDatabase.Contains(obj) || !assetPath.ToLower().EndsWith("png"))
            {
                Debug.Log("�ϲ�δ��ɣ���ѡ��Դ�����˷�PNG��ʽͼƬ");
                return;
            }
        }

        // ��ȡ��ѡͼƬ����Ŀ¼·��
        string rootPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(selectedObjects[0]));
        string outputFolder = "seq_output_" + selectedObjects[0].name;
        if (!AssetDatabase.IsValidFolder(rootPath+"/"+outputFolder))
        {
            // ��������ļ���
            AssetDatabase.CreateFolder(rootPath, outputFolder);
        }

        // TODO �����ϲ����PNG��ͼ����
        var width = 0;
        var height = 0;
        int curWidth = 0;
        int curHeight = 0;
        foreach (Object obj in selectedObjects)
        {
            string assetPath = AssetDatabase.GetAssetPath(obj);
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
            if (texture==null)
            {
                continue;
            }
            if (curWidth + texture.width > maxWidth)
            {
                height += curHeight;
                curHeight = texture.height;
                width = maxWidth;
                curWidth = texture.width;
            }
            else
            {
                curWidth += texture.width;
                if (width < curWidth)
                {
                    width = curWidth;
                }
                if (curHeight < texture.height)
                {
                    curHeight = texture.height;
                }
                if (height < curHeight)
                {
                    height = curHeight;
                }
            }
        }
        Debug.Log($"FINAL: {width} - {height}");

        Texture2D finalImg = new Texture2D(width, height, TextureFormat.RGBA32, false);
        // ����һ��͸�����أ�R=0, G=0, B=0, A=0��
        Color transparentColor = new Color(0, 0, 0, 0);
        // ��ʼ����������Ϊ͸��
        Color[] pixels = new Color[width * height];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = transparentColor;
        }
        // ����������Ӧ�õ�����
        finalImg.SetPixels(pixels);

        curWidth = 0;
        curHeight = 0;
        int lastRowHeight = 0;
        foreach (Object obj in selectedObjects)
        {
            string assetPath = AssetDatabase.GetAssetPath(obj);
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
            if (texture == null)
            {
                continue;
            }
            if (curWidth + texture.width > maxWidth)
            {
                curHeight += lastRowHeight;
                curWidth = texture.width;

                Debug.Log($"TEMP: 0 - {height - curHeight}");
                finalImg.SetPixels(0, height-curHeight-texture.height, texture.width, texture.height, texture.GetPixels());
            }
            else
            {
                Debug.Log($"TEMP: {curWidth} - {height - curHeight}");
                finalImg.SetPixels(curWidth, height - curHeight - texture.height, texture.width, texture.height, texture.GetPixels());

                curWidth += texture.width;
                if (lastRowHeight < texture.height)
                {
                    lastRowHeight = texture.height;
                }
            }
        }
        // ��������֡PNG�ļ�
        string saveImgPath = rootPath + "/" + outputFolder + "/frames.png";
        File.WriteAllBytes(saveImgPath, finalImg.EncodeToPNG());
        // ˢ����Դ���ڽ���
        AssetDatabase.Refresh();
        // ����Сͼ�ĸ�ʽ
        TextureImporter smallTextureImp = AssetImporter.GetAtPath(saveImgPath) as TextureImporter;
        // ����Ϊ�ɶ�
        smallTextureImp.isReadable = true;
        // ����alphaͨ��
        smallTextureImp.alphaIsTransparency = true;
        // ������mipmap
        smallTextureImp.mipmapEnabled = false;
        AssetDatabase.ImportAsset(saveImgPath);
    }
}
