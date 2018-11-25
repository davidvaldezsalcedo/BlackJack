using UnityEngine;

public class Cards : MonoBehaviour
{
    public int CardValue = 1;
    public Sprite[] CardFaces;
    
    private SpriteRenderer _Renderer;

    private void Awake()
    {
        _Renderer = GetComponent<SpriteRenderer>();
    }

    public void ShowCardSide(Sprite faceToShow)
    {
        _Renderer.sprite = faceToShow;
    }
}
