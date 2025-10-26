using UnityEngine;
using UnityEditor;
using System.IO;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent;

/// <summary>
/// 将多张小图合并成一张PNG序列帧
/// </summary>
public class MergeTexture : EditorWindow
{
    [MenuItem("Tools/MergeTexture")]
    static void DoMergeTexture()
    {
        // 合并后大图最大像素宽度
        const int maxWidth = 1024;

        // 获取所选图片
        // Texture2D selectedImg = Selection.ac as Texture2D;

        Object[] selectedObjects = Selection.objects;
        if (selectedObjects == null || selectedObjects.Length <= 0)
        {
            Debug.Log("合并未完成，未选中任何资源");
        }

        foreach (Object obj in selectedObjects)
        {
            string assetPath = AssetDatabase.GetAssetPath(obj);
            if (!AssetDatabase.Contains(obj) || !assetPath.ToLower().EndsWith("png"))
            {
                Debug.Log("合并未完成，所选资源包含了非PNG格式图片");
                return;
            }
        }

        // 获取所选图片所在目录路径
        string rootPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(selectedObjects[0]));
        string outputFolder = "seq_output_" + selectedObjects[0].name;
        if (!AssetDatabase.IsValidFolder(rootPath+"/"+outputFolder))
        {
            // 创建输出文件夹
            AssetDatabase.CreateFolder(rootPath, outputFolder);
        }

        // TODO 创建合并后的PNG大图对象
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
        // 创建一个透明像素（R=0, G=0, B=0, A=0）
        Color transparentColor = new Color(0, 0, 0, 0);
        // 初始化所有像素为透明
        Color[] pixels = new Color[width * height];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = transparentColor;
        }
        // 将像素数据应用到纹理
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
        // 保存序列帧PNG文件
        string saveImgPath = rootPath + "/" + outputFolder + "/frames.png";
        File.WriteAllBytes(saveImgPath, finalImg.EncodeToPNG());
        // 刷新资源窗口界面
        AssetDatabase.Refresh();
        // 设置小图的格式
        TextureImporter smallTextureImp = AssetImporter.GetAtPath(saveImgPath) as TextureImporter;
        // 设置为可读
        smallTextureImp.isReadable = true;
        // 设置alpha通道
        smallTextureImp.alphaIsTransparency = true;
        // 不开启mipmap
        smallTextureImp.mipmapEnabled = false;
        AssetDatabase.ImportAsset(saveImgPath);
    }
}
