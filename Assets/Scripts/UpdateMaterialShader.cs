using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UpdateMaterialShader : MonoBehaviour
{
    public int cubemapSize = 128;
    bool oneFacePerFrame = false;
    Camera cam;
    RenderTexture renderTexture;

    public Material newMaterial, originalMat;
    MaterialPropertyBlock matBlock;

    void Start()
    {
        matBlock = new MaterialPropertyBlock();
        originalMat = GetComponent<Renderer>().sharedMaterial;

        matBlock.Clear();
        matBlock.SetTexture("_Cube", renderTexture);

        GetComponent<Renderer>().SetPropertyBlock(matBlock, 0);

        GetComponent<Renderer>().sharedMaterial = newMaterial;

        UpdateCubemap(0b111111); 
    }

    void OnDisable()
    {
        DestroyImmediate(cam); 
        DestroyImmediate(renderTexture);
    }

    void LateUpdate()
    {
        if (oneFacePerFrame)
        {
            var faceToRender = Time.frameCount % 6; 
            var faceMask = 1 << faceToRender;
            UpdateCubemap(faceMask);
        }
        else
        {
            UpdateCubemap(0b111111); 
        }
    }

    void UpdateCubemap(int faceMask)
    {
        //if (!cam)
        //{
        //    GameObject obj = new GameObject("CubemapCamera", typeof(Camera));
        //    obj.hideFlags = HideFlags.HideAndDontSave;
        //    obj.transform.position = transform.position;
        //    obj.transform.rotation = Quaternion.identity;
        //    cam = obj.GetComponent<Camera>();
        //    cam.farClipPlane = 100; // don't render very far into cubemap
        //    cam.enabled = false;
        //}

        //if (!renderTexture)
        //{
        //    renderTexture = new RenderTexture(cubemapSize, cubemapSize, 16);
        //    renderTexture.dimension = UnityEngine.Rendering.TextureDimension.Cube;
        //    renderTexture.hideFlags = HideFlags.HideAndDontSave;
        //    GetComponent<Renderer>().sharedMaterial.SetTexture("_Cube", renderTexture);
        //}

        //cam.transform.position = transform.position;
        //cam.RenderToCubemap(renderTexture, faceMask);

        if (!cam)
        {
            GameObject obj = new GameObject("CubemapCamera", typeof(Camera));
            obj.hideFlags = HideFlags.HideAndDontSave;
            obj.transform.position = transform.position;
            obj.transform.rotation = Quaternion.identity;
            cam = obj.GetComponent<Camera>();
            cam.nearClipPlane = 0.3f;
            cam.farClipPlane = 100;
            cam.enabled = false;
        }

        if (!renderTexture)
        {
            renderTexture = new RenderTexture(cubemapSize, cubemapSize, 16);
            renderTexture.dimension = UnityEngine.Rendering.TextureDimension.Cube;
            renderTexture.hideFlags = HideFlags.HideAndDontSave;
            GetComponent<Renderer>().sharedMaterial = newMaterial;
            GetComponent<Renderer>().GetPropertyBlock(matBlock, 0);
            matBlock.SetTexture("_Cube", renderTexture);
            GetComponent<Renderer>().SetPropertyBlock(matBlock, 0);
        }

        cam.transform.position = transform.position;
        cam.RenderToCubemap(renderTexture, faceMask);
    }
}
