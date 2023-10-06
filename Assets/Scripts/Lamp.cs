using UnityEngine;
using UnityEngine.Rendering.Universal;


public class Lamp : MonoBehaviour
{

  [SerializeField] Sprite unlitSprite;
  [SerializeField] Sprite litSprite;
  public Transform raycastPoint;

  private SpriteRenderer spriteRenderer;


  private Light2D light2d;
  public bool isLit = false;


  void Start()
  {
    spriteRenderer = GetComponentInParent<SpriteRenderer>();
    spriteRenderer.sprite = unlitSprite;

    light2d = GetComponent<Light2D>();
  }

  void Update()
  {
    if (isLit)
    {
      Light();
    }
    else
    {
      Unlight();
    }
  }

  public void Light()
  {
    spriteRenderer.sprite = litSprite;
    light2d.enabled = true;
    if (!LightManager.instance.lamps.Contains(this))
    {
      LightManager.instance.AddLamp(this);
    }
  }

  public void Unlight()
  {
    spriteRenderer.sprite = unlitSprite;
    light2d.enabled = false;
    if (LightManager.instance.lamps.Contains(this))
    {
      LightManager.instance.RemoveLamp(this);
    }
  }
}
