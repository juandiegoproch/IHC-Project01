using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ImagePickerWithDuplicatesCheck : MonoBehaviour
{
    public GameObject imageButtonPrefab;
    public Transform buttonParent;
    public Renderer quadRenderer;

    private HashSet<string> usedPaths = new HashSet<string>();

    public void PickImage()
    {
        NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null && File.Exists(path))
            {
                if (usedPaths.Contains(path))
                {
                    Debug.Log("Duplicate image path skipped: " + path);
                    return;
                }

                usedPaths.Add(path);

                // Load texture from file
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(File.ReadAllBytes(path));

                CreateImageButton(texture);
            }
        }, "Select an image");
    }

    private void CreateImageButton(Texture2D tex)
    {
        GameObject buttonObj = Instantiate(imageButtonPrefab, buttonParent);

        // Move to second-to-last position
        int count = buttonParent.childCount;
        buttonObj.transform.SetSiblingIndex(Mathf.Max(0, count - 2));

        Image img = buttonObj.GetComponentInChildren<Image>();
        img.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

        ApplyImage applyScript = buttonObj.GetComponent<ApplyImage>();
        if (applyScript != null)
        {
            applyScript.imageToDisplay = tex;
            applyScript.targetRenderer = quadRenderer;
        }
    }

}
