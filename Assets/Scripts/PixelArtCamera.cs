﻿// Ppppperfect pixel art rendering!! version 2
// By Alexander Ocias
// https://ocias.com

using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class PixelArtCamera : MonoBehaviour {
	public Vector2Int screenResolution;
	public Vector2Int internalResolution;
	public Vector2Int upscaledResolution;
	public bool smooth = false;
	public bool forceSquarePixels = false;
	public Vector2Int pixels = new Vector2Int(1080/12, 1920/12);
	public float pixelsPerUnit = 16;
	public bool requireStencilBuffer = false;
	public Vector2 pixelAspectRatio = Vector2.one;
	public bool windowboxing = false; //Maybe implement this later?
	public bool integerScaleOnly = false; //Maybe implement this later?
	public bool useUpscaleShader = false;

	public Material upscaleMaterial;

	RenderTexture rt;

	Rect viewportRect;

	float targetAspectRatio;
	float currentAspectRatio;
	
	[HideInInspector] public Vector2 finalBlitStretch = Vector2.one;

	public Camera mainCamera;
	public Canvas mainCanvas;

	// Use this for initialization
	void Start () {
		SetupRenderTexture();
	}

	void Reset () {
		// Try to connect everything automatically on first attach
		mainCamera = Camera.main;
		GameObject canvasObj = GameObject.Find("Canvas");
		if (canvasObj != null) {
			mainCanvas = canvasObj.GetComponent<Canvas>();
		}
		Material bilinearUpscaleMat = new Material(Shader.Find("Hidden/BilinearSharp"));
		if (bilinearUpscaleMat != null) {
			upscaleMaterial = bilinearUpscaleMat;
			upscaleMaterial.SetVector("_DestinationResolution", new Vector4(Screen.width, Screen.height, 0, 0));
			useUpscaleShader = true;
		}
		
	}

	public void SetupRenderTexture () {
		// Try to connect missing pieces
		if (mainCamera == null) {
			mainCamera = Camera.main;
		}
		if (mainCanvas == null) {
			GameObject canvasObj = GameObject.Find("Canvas");
			if (canvasObj != null) {
				mainCanvas = canvasObj.GetComponent<Canvas>();
			}
		}
		// prevent 0-size rendertextures, just in case
		if (pixels.x == 0 || pixels.y == 0) {
			return;
		}
		
		if (rt != null) {
			rt.Release();
		}
		
		screenResolution.x = Screen.width;
		screenResolution.y = Screen.height;

		float pixelAspect = 1;
		if (pixelAspectRatio.x > 0 && pixelAspectRatio.y > 0) {
			pixelAspect = pixelAspectRatio.x / pixelAspectRatio.y;
		}

		targetAspectRatio = (float)pixels.x / (float)pixels.y * pixelAspect; //1.5
		currentAspectRatio = (float)Screen.width / (float)Screen.height; // 1.777778
		
		
		internalResolution.x = pixels.x;
		internalResolution.y = pixels.y;
		
		// Figure out best pixel resolution for aspect ratio we're on
		if (currentAspectRatio != targetAspectRatio) {
			if (windowboxing) {
				// Leave internal res alone
			} else if (currentAspectRatio > targetAspectRatio) {
				// Wider screen
				internalResolution.x = (int)Mathf.Round((float)pixels.y * currentAspectRatio / pixelAspect);
			} else {
				// Taller screen
				internalResolution.y = (int)Mathf.Round((float)pixels.x / currentAspectRatio * pixelAspect);
			}
		}
		
		
		// Determine viewport rect
		float vWidth = 1f;
		float vHeight = 1f;
		if (windowboxing) {
			int scaleMultiple = Mathf.FloorToInt((float)Screen.width / (float)internalResolution.x);
			if (currentAspectRatio > targetAspectRatio) {
				vWidth = targetAspectRatio / currentAspectRatio;

				if (integerScaleOnly) {
					float proportion = (float)scaleMultiple * internalResolution.y / Screen.height;
					vWidth *= proportion;
					vHeight *= proportion;
				}

				viewportRect = new Rect((1f - vWidth) / 2f,(1f - vHeight) / 2f,vWidth,vHeight);

				

			} else {
				vHeight = currentAspectRatio / targetAspectRatio;

				if (integerScaleOnly) {
					float proportion = (float)scaleMultiple * internalResolution.x / Screen.width;
					vWidth *= proportion;
					vHeight *= proportion;
				}

				viewportRect = new Rect((1f - vWidth) / 2f,(1f - vHeight) / 2f,vWidth,vHeight);
			}
			
		} else {
			viewportRect = new Rect(0,0,1f,1f);
		}

		// Determine scale to keep pixels square
		finalBlitStretch = Vector2.one;
		if (windowboxing) {

		} else if (forceSquarePixels) {
			float internalResAspect = (float)internalResolution.x / (float)internalResolution.y;
			if (currentAspectRatio != targetAspectRatio) {
				if (currentAspectRatio > targetAspectRatio) {
					// Wider screen
					finalBlitStretch.x = (currentAspectRatio / internalResAspect);
				} else {
					// Taller screen
					finalBlitStretch.y = (internalResAspect / currentAspectRatio);
				}
			}
		}

		// Configure canvas properly to match camera
		if (mainCanvas != null) {
			mainCanvas.renderMode = RenderMode.ScreenSpaceCamera;
			mainCanvas.worldCamera = mainCamera;
			CanvasScaler scaler = mainCanvas.GetComponent<CanvasScaler>();
			scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			//scaler.referenceResolution = new Vector2(pixels.x, pixels.y);
			scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
			scaler.referencePixelsPerUnit = pixelsPerUnit;

			// If we're stretching to a non-square pixel ratio, we need to make sure the canvas scaler keeps our pixel per unit ratio
			Vector2 pixelSize;
			pixelSize.x = (float)screenResolution.x / (float)internalResolution.x;
			pixelSize.y = (float)screenResolution.y / (float)internalResolution.y;
			scaler.referenceResolution = new Vector2((float)pixels.x * pixelSize.x/pixelSize.y, (float)pixels.y);
			if (windowboxing) {
				scaler.referenceResolution = new Vector2((float)pixels.x, (float)pixels.y);
			}
		}

		// Make sure our camera projection fits our resolution
		mainCamera.orthographicSize = internalResolution.y / 2f / pixelsPerUnit;
		Shader.SetGlobalFloat("PIXELS_PER_UNIT", pixelsPerUnit);

		// rt = new RenderTexture(internalResolution.x, internalResolution.y, 16, RenderTextureFormat.ARGB32);
		
		// if (smooth) {
		// 	rt.filterMode = FilterMode.Bilinear;
		// } else {
		// 	rt.filterMode = FilterMode.Point;
		// }
		// rt.Create();
	}

	void OnPreRender() {
		if ((float)Screen.width / (float)Screen.height != currentAspectRatio) {
			SetupRenderTexture();
		}
		rt = RenderTexture.GetTemporary(internalResolution.x, internalResolution.y, 16, RenderTextureFormat.ARGB32);
		if (smooth || useUpscaleShader) {
			rt.filterMode = FilterMode.Bilinear;
		} else {
			rt.filterMode = FilterMode.Point;
		}
		// if (windowboxing) {
		// 	rt.filterMode = FilterMode.Point;
		// }
		if (requireStencilBuffer) {
			rt.depth = 32;
		}
		if (mainCamera != null) {
			// Render to our small internal texture
			mainCamera.targetTexture = rt;
		}
		mainCamera.rect = new Rect(0,0,1f,1f);
    }
	void OnPostRender() {
		if (mainCamera == null) {
			return;
		}
		mainCamera.rect = viewportRect;
		// null the targettexture so we can blit to the screen
		mainCamera.targetTexture = null;
		
		/*if (windowboxing) {
			// find suitable scale
			int scaleMultiple = Mathf.FloorToInt((float)Screen.width / (float)internalResolution.x);
			upscaledResolution.x = internalResolution.x * scaleMultiple;
			upscaledResolution.y = internalResolution.y * scaleMultiple;
			Vector2 stretch = new Vector2(1f/((float)upscaledResolution.x/(float)Screen.width), 1f/((float)upscaledResolution.y/(float)Screen.height));
			Graphics.Blit(rt, null, stretch, (stretch - Vector2.one) / -2f);
			// Graphics.Blit(rt, null, new Vector2(upscaledResolution.x / (float)Screen.width, upscaledResolution.y / (float)Screen.height), new Vector2(0f, 0f));
		} else */if (smooth) {
			Graphics.Blit(rt, null, finalBlitStretch, (finalBlitStretch - Vector2.one) / -2f);
		} else if (useUpscaleShader) {
			upscaleMaterial.SetVector("_DestinationResolution", new Vector4(Screen.width, Screen.height, 0, 0));
			Graphics.Blit(rt, null, upscaleMaterial);
		} else {
			// draw to buffer at least as big as the screen
			int scaleMultiple = Mathf.CeilToInt((float)Screen.width / (float)internalResolution.x);
			upscaledResolution.x = internalResolution.x * scaleMultiple;
			upscaledResolution.y = internalResolution.y * scaleMultiple;
			RenderTexture largeRt = RenderTexture.GetTemporary(upscaledResolution.x, upscaledResolution.y);
			Graphics.Blit(rt, largeRt);
			
			// Scale down to screen		
			Graphics.Blit(largeRt, null, finalBlitStretch, (finalBlitStretch - Vector2.one) / -2f);
			RenderTexture.ReleaseTemporary(largeRt);
		}
		RenderTexture.ReleaseTemporary(rt);
		
    }
}


