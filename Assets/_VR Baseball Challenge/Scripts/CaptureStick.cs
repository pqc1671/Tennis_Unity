#if UNITY_EDITOR
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(CaptureStick))]
public class CaptureStickEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Capture"))
        {
            var t = target as CaptureStick;
            t.Capture();
        }
        if (GUILayout.Button("Save"))
        {
            var t = target as CaptureStick;
            t.Save();
        }
    }
}

public class CaptureStick : MonoBehaviour
{
    [SerializeField] private Texture2D _tex;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Nhấn P để chụp màn hình
        {
            StartCoroutine(CaptureScreenshot());
        }
    }

    public void Capture()
    {
        StartCoroutine(CaptureScreenshot());
    }

    public void Save()
    {
        byte[] bytes = _tex.EncodeToPNG();
        string path = Path.Combine(Application.persistentDataPath, "screenshot.png");
        File.WriteAllBytes(path, bytes);
        Debug.Log("Screenshot saved to: " + path);
        DestroyImmediate(_tex);
    }

    private IEnumerator CaptureScreenshot()
    {
        yield return new WaitForEndOfFrame(); // Chờ khung hình vẽ xong

        int width = Screen.width;
        int height = Screen.height;
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        texture.Apply();
        _tex = texture;

        //byte[] bytes = texture.EncodeToPNG();
        //string path = Path.Combine(Application.persistentDataPath, "screenshot.png");
        //File.WriteAllBytes(path, bytes);

        //Debug.Log("Screenshot saved to: " + path);
        //Destroy(texture);
    }
}

#endif