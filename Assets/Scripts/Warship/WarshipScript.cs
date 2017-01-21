using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWarshipType { W0, W1, W2, W3 }; //a definir

public class WarshipScript : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
	private ViewZoneScript _vzScript;

	[SerializeField]
	private SpriteRenderer _warshipSprite;

	private float _defaultAlpha = 0.4f;

	[SerializeField]
	private EWarshipType _type;
	public EWarshipType Type //TODO: a voir, sinon faire le choix lors du bouton play pour éviter de faire un new a chaque choix de ship
	{
		get { return _type; }
		set { _type = value; }
	}
	private WarshipAttributes _attributes;

	public WarshipAttributes Attributes
	{
		get { return _attributes; }
		set { _attributes = value; }
	}

	public void Init( EWarshipType type )
	{
		_type = type;
		switch( _type )
		{
		case EWarshipType.W0:
			_attributes = new WarshipDefault();
			break;
		case EWarshipType.W1:
			_attributes = new WarshipType1();
			break;
		case EWarshipType.W2:
			//_attributes = new WharshipType1();
			break;
		case EWarshipType.W3:
			//_attributes = new WharshipType1();
			break;
		default:
			_attributes = new WarshipDefault();//TODO: exception ici
			break;
		}
	}
	void Start()
	{
         animator.SetBool("isAlive", true);
        _attributes = new WarshipDefault();
		_defaultAlpha = 0.4f;
	}

	public bool ok;
	void Update()
	{
		if( ok )
		{
			_vzScript.StartAscendCoroutine();
			StartCoroutine("AscendCoroutine");
			ok = false;
        }

        //Mettre dans l'event getDamage
        animator.SetBool("isAlive", false);
    }

	public IEnumerator AscendCoroutine()
	{
		_warshipSprite.color = new Color(_warshipSprite.material.color.r, _warshipSprite.material.color.g, _warshipSprite.material.color.b, _defaultAlpha + 0.1f);
		yield return new WaitForSeconds(.4f);
		_warshipSprite.color = new Color(_warshipSprite.material.color.r, _warshipSprite.material.color.g, _warshipSprite.material.color.b, _defaultAlpha + 0.2f);
		yield return new WaitForSeconds(.4f);
		_warshipSprite.color = new Color(_warshipSprite.material.color.r, _warshipSprite.material.color.g, _warshipSprite.material.color.b, _defaultAlpha + 0.3f);
		yield return new WaitForSeconds(.4f);
		_warshipSprite.color = new Color(_warshipSprite.material.color.r, _warshipSprite.material.color.g, _warshipSprite.material.color.b, _defaultAlpha + 0.4f);
		yield return new WaitForSeconds(.4f);
		_warshipSprite.color = new Color(_warshipSprite.material.color.r, _warshipSprite.material.color.g, _warshipSprite.material.color.b, 1);
		yield return null;

	}
	public IEnumerator DescendCoroutine()
	{
		_warshipSprite.color = new Color(_warshipSprite.material.color.r, _warshipSprite.material.color.g, _warshipSprite.material.color.b, _warshipSprite.material.color.a - 0.1f);
		yield return new WaitForSeconds(.4f);
		_warshipSprite.color = new Color(_warshipSprite.material.color.r, _warshipSprite.material.color.g, _warshipSprite.material.color.b, _warshipSprite.material.color.a - 0.2f);
		yield return new WaitForSeconds(.4f);
		_warshipSprite.color = new Color(_warshipSprite.material.color.r, _warshipSprite.material.color.g, _warshipSprite.material.color.b, _warshipSprite.material.color.a - 0.4f);
		yield return new WaitForSeconds(.4f);
		_warshipSprite.color = new Color(_warshipSprite.material.color.r, _warshipSprite.material.color.g, _warshipSprite.material.color.b, _warshipSprite.material.color.a - 0.6f);
		yield return new WaitForSeconds(.4f);
		_warshipSprite.color = new Color(_warshipSprite.material.color.r, _warshipSprite.material.color.g, _warshipSprite.material.color.b, _defaultAlpha);
		yield return null;

	}
}
