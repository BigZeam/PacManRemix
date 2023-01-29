
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class AnimatedSprite : MonoBehaviour
{   
    
    public SpriteRenderer sr { get; private set; }
    public Sprite[] sprites;

    public float animationTime = .25f;
    public int animationFrame{ get; private set; }

    public bool loop = true;
    
    private void Awake() 
    {
        this.sr = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime);
    }

    private void Advance()
    {
        if(!this.sr.enabled)
        {
            return;
        }

        this.animationFrame++;
        if(this.animationFrame>= this.sprites.Length && this.loop)
        {
            this.animationFrame = 0;
        }
        if(this.animationFrame >= 0 && this.animationFrame < this.sprites.Length)
        {
            this.sr.sprite = this.sprites[this.animationFrame];
        }
    }
    public void Restart()
    {
        this.animationFrame = -1;
        Advance();

    }
}
