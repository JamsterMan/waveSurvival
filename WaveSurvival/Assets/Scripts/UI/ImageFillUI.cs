using UnityEngine;
using UnityEngine.UI;

public class ImageFillUI : MonoBehaviour
{
    public Image image;

    public void SetImageFill(float val)
    {
        if (val > 1 || val < 0)
            return;

        image.fillAmount = val;
    }
}
