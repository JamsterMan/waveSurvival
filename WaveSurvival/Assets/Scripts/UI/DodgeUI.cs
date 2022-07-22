using UnityEngine;

public class DodgeUI : MonoBehaviour
{
    public ImageFillUI image;

    public void UpdateDodgeUI(float time, float nextTime, float cooldown)
    {
        float val = (nextTime - time)/cooldown ;

        image.SetImageFill(val);
        
    }
}
