using UnityEngine;

public class IdleMoney : MonoBehaviour
{
    public float money = 0f;
    public float moneyPerSecond = 1f;   // saniyede kaç para gelsin
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f) // 1 saniye geçtiyse
        {
            money += moneyPerSecond;
            timer = 0f;

            Debug.Log("Paran: " + money);
        }
    }
}
