using UnityEngine;
public class WeakSpot_Fire : MonoBehaviour
{

    public SpriteRenderer Spriterenderer;
    void Start()
    {
        Spriterenderer.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ennemy"))
        {
            Spriterenderer.enabled = true;
        }
    }
}
