using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CharacterEffects : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] BoxCollider2D playerCollider;
    private bool isBlinking;
    public bool blinkOnStart;
    public bool isGhosting{ get; set;}
    [SerializeField] bool _ghostingOnStart, destroyGosth = true;
    [SerializeField] private float _blinkDelay = .2f;
    [SerializeField] private float _ghostDelaydispeal = 10f;
    [SerializeField] private float _ghostDelayDuration = 1f;
    [SerializeField] private Color _normalColor = new Color(1f,1f,1f,1f);
    [SerializeField] private Color _blinklColor,  _ghostColor = new Color(1f,1f,1f,.2f);
    [Range(0, 10f)][SerializeField] private float _ghostDistance = .2f;
    [Range(.5f, 4f)][SerializeField] private float _ghostScale = 1;
    [SerializeField] int _blinkCount = 2;
    private Vector3 lastGhostPosition = Vector3.zero;
    GameObject _ghostLayout;
    [SerializeField] private int _maxGhostCount = 20;
    private int ghostCount = 0;


    void Start()
    {
       if(!playerCollider) playerCollider = GetComponent<BoxCollider2D>();
       if(!_spriteRenderer) _spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeSpriteSize();
       SetGhostLayout();
       CalculateGhostDistace();
       if(_ghostingOnStart) isGhosting = true;
       if(blinkOnStart) 
       {
            Blink();
        }
    }
    
    public void EnableIsGhosting()
    {
        isGhosting = true;
    }

    public void DisableIsGhosting()
    {
        isGhosting = false;
    }
    
    void Update()
    {
        //currentEffect cambia a normal desde unityEvents
        if(isGhosting)
        {
            if(ghostCount < _maxGhostCount)
                SpawnGhost();
        }
    }

    void CalculateGhostDistace()
    {
        _ghostDistance = playerCollider.bounds.size.x * _ghostDistance;

    }

    void SetGhostLayout()
    {
        _ghostLayout = new GameObject();
        _ghostLayout.name = "ghost";
        _ghostLayout.AddComponent<SpriteRenderer>();
        _ghostLayout.AddComponent<DestroyObject>();
        SpriteRenderer ghostSprite = _ghostLayout.GetComponent<SpriteRenderer>();
        _ghostLayout.transform.localScale = transform.localScale;
        ghostSprite.sprite = _spriteRenderer.sprite;
        ghostSprite.drawMode = _spriteRenderer.drawMode;
        ghostSprite.size = _spriteRenderer.size * _ghostScale;
        ghostSprite.color = new Color(_ghostColor.r, _ghostColor.g,_ghostColor.b, 0);
        ghostSprite.sortingOrder = _spriteRenderer.sortingOrder;
        _ghostLayout.SetActive(false);
    }

    void SpawnGhost()
    {
        if(ghostCount == 0) 
        {
            GhostEffect();
            return;
        }
        float currentGhostDistence = Vector3.Distance(lastGhostPosition, transform.position);
        if(currentGhostDistence >= _ghostDistance)
        {
            GhostEffect();
        }
    }

    public void Blink()
    {   
        if(!isBlinking)
        {
            isBlinking = true;
            StartCoroutine(BlinkRoutine());
        }
    }

    private void GhostEffect()
    {
        ghostCount++;
        _ghostLayout.GetComponent<SpriteRenderer>().color = new Color(_ghostColor.r, _ghostColor.g,_ghostColor.b, 1);
        GameObject ghost = Instantiate(_ghostLayout, transform.position, Quaternion.identity);
        //ghost.GetComponent<SpriteRenderer>().sortingOrder = _spriteRenderer.sortingOrder;
        ghost.SetActive(true);
        lastGhostPosition = ghost.transform.position;
        if(destroyGosth) dispelEffect(ghost);
    }

    void dispelEffect(GameObject gameObject)
    {
        StartCoroutine(DispelRoutine(gameObject));
    }

    IEnumerator BlinkRoutine()
    {
        int currentBlink = 0;
        while(currentBlink < _blinkCount)
        {
            _spriteRenderer.color = _blinklColor;
            yield return new WaitForSeconds(_blinkDelay);
            _spriteRenderer.color = _normalColor;
            yield return new WaitForSeconds(_blinkDelay);
            currentBlink++;
        }
        isBlinking = false;
    }

    void ChangeSpriteSize()
    {
        _spriteRenderer.size = playerCollider.size;
    }

    IEnumerator DispelRoutine(GameObject gameObject)
    {
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        while(sprite.color.a > 0)
        {
            sprite.color = new 
            Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - 1/_ghostDelaydispeal);
            yield return new WaitForSeconds(_ghostDelayDuration/_ghostDelaydispeal);
        }
        gameObject.GetComponent<DestroyObject>().DestroyWithDelay(.02f);
        if(ghostCount > 0) ghostCount--;
    }
}

