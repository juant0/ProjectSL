using UnityEngine;

public class DistanceTest : MonoBehaviour
{
    bool counting = false;
    float timer = 0;
    private void OnTriggerEnter(Collider other)
    {
        Count();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }
    private void Count() {
        counting = !counting;
        if (!counting)
            Debug.Log(timer);
        timer = 0f;
    }
}
