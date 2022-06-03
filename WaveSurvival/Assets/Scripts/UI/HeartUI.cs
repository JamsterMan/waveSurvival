using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeartUI : MonoBehaviour
{
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    public Image heart;

    public void ChangeHeartSprite(HeartState state)
    {
        if (state == HeartState.full)
            heart.sprite = fullHeart;
        else if (state == HeartState.half)
            heart.sprite = halfHeart;
        else
            heart.sprite = emptyHeart;
    }

}

public enum HeartState
{
    full,
    half,
    empty
}
