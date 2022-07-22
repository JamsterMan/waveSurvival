using UnityEngine;

public class HeartsAreaUI : MonoBehaviour
{
    public GameObject heartPrefab;

    private HeartUI[] _hearts = new HeartUI[10];//max 10 hearts 
    private int _numOfHearts = 0;
    private readonly int _maxHearts = 10;


    public void SetUpHeartsUI(int numHearts)
    {
        //numOfHearts = numHearts;

        for (int i = 0; i < numHearts; i++)
        {
            //hearts[i] = Instantiate(heartPrefab,gameObject.transform).GetComponent<HeartUI>();
            AddHeart();
        }
    }

    public void AddHeart()
    {
        if (_numOfHearts + 1 <= _maxHearts)
        {
            _hearts[_numOfHearts] = Instantiate(heartPrefab, gameObject.transform).GetComponent<HeartUI>();
            _numOfHearts++;
        }
    }

    public void RemoveHeart()
    {
        if (_numOfHearts + 1 <= _maxHearts)
        {
            _numOfHearts--;
            Destroy(_hearts[_numOfHearts].gameObject);
        }
    }

    public void UpdateHearts(int currentHealth, int health, int healthPerHeart)
    {
        int fullHearts = currentHealth / healthPerHeart;
        int halfHearts = currentHealth % healthPerHeart;
        for (int i = 0; i < fullHearts; i++)
        {
            _hearts[i].ChangeHeartSprite(HeartState.full);
        }
        if (halfHearts == 1)
        {
            _hearts[fullHearts].ChangeHeartSprite(HeartState.half);
        }
        for (int i = fullHearts+halfHearts; i < _numOfHearts; i++)
        {
            _hearts[i].ChangeHeartSprite(HeartState.empty);
        }
    }

}
