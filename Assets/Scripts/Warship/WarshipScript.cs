using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWarshipType { W0, W1, W2, W3 }; //a definir
public enum EWarshipState { INSIDE, OUTSIDE };

public class WarshipScript : MonoBehaviour
{
	[SerializeField]
	Animator animator;

	[SerializeField]
	private ViewZoneScript _vzScript;

	[SerializeField]
	private SpriteRenderer _warshipSprite;
	public SpriteRenderer WarshipSprite
	{
		get { return _warshipSprite; }
		set { _warshipSprite = value; }
	}

	private Color _insideColor;
	private Color _outsideColor;

	[SerializeField]
	private EWarshipType _type;
	public EWarshipType Type //TODO: a voir, sinon faire le choix lors du bouton play pour éviter de faire un new a chaque choix de ship
	{
		get { return _type; }
		set { _type = value; }
	}

	[SerializeField]
	private EWarshipState _state;
	public EWarshipState State
	{
		get { return _state; }
		set { _state = value; }
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
	public void Init( EWarshipType type, EWarshipState state )
	{
		Init(type);
		switch( state )
		{
		case EWarshipState.INSIDE:
			_warshipSprite.color = _insideColor;
			break;
		case EWarshipState.OUTSIDE:
			_warshipSprite.color = _outsideColor;
			break;
		default:
			_warshipSprite.color = _insideColor; //a voir, exception
			break;
		}
	}
	void Start()
	{

		animator.SetBool("isAlive", true);
		_attributes = new WarshipDefault();

		_insideColor = _warshipSprite.color;
		_outsideColor = new Color(255f, 255f, 255f, 255f);
	}


	public bool Ascend_Descend;
	public bool ShowViewZone;
	void Update()
	{
		//Mettre dans l'event getDamage
		animator.SetBool("isAlive", false);
		if( ShowViewZone )
		{
			StartCoroutine("EnableViewZone");
		}
		if( Ascend_Descend )
		{
			if( _state == EWarshipState.OUTSIDE )
			{
				if( ShowViewZone )
				{
					_vzScript.StartDescendCoroutine();
				}
				StartCoroutine("DescendCoroutine");
				Ascend_Descend = false;
			}
			else
			{
				if( ShowViewZone )
				{
					_vzScript.StartAscendCoroutine();
				}
				StartCoroutine("AscendCoroutine");
				Ascend_Descend = false;
			}
		}
	}


	public IEnumerator AscendCoroutine()
	{
		_warshipSprite.color = new Color(_warshipSprite.material.color.r, _warshipSprite.material.color.g, _warshipSprite.material.color.b, _insideColor.a + 0.1f);
		yield return new WaitForSeconds(.4f);
		_warshipSprite.color = new Color(_warshipSprite.material.color.r, _warshipSprite.material.color.g, _warshipSprite.material.color.b, _insideColor.a + 0.2f);
		yield return new WaitForSeconds(.4f);
		_warshipSprite.color = new Color(_warshipSprite.material.color.r, _warshipSprite.material.color.g, _warshipSprite.material.color.b, _insideColor.a + 0.3f);
		yield return new WaitForSeconds(.4f);
		_warshipSprite.color = new Color(_warshipSprite.material.color.r, _warshipSprite.material.color.g, _warshipSprite.material.color.b, _insideColor.a + 0.4f);
		yield return new WaitForSeconds(.4f);
		_warshipSprite.color = new Color(_warshipSprite.material.color.r, _warshipSprite.material.color.g, _warshipSprite.material.color.b, _outsideColor.a);
		_state = EWarshipState.OUTSIDE;
		yield return null;

	}
	public IEnumerator DescendCoroutine()
	{
		_warshipSprite.color = new Color(_warshipSprite.material.color.r, _warshipSprite.material.color.g, _warshipSprite.material.color.b, _outsideColor.a - 0.1f);
		yield return new WaitForSeconds(.4f);
		_warshipSprite.color = new Color(_warshipSprite.material.color.r, _warshipSprite.material.color.g, _warshipSprite.material.color.b, _outsideColor.a - 0.2f);
		yield return new WaitForSeconds(.4f);
		_warshipSprite.color = new Color(_warshipSprite.material.color.r, _warshipSprite.material.color.g, _warshipSprite.material.color.b, _outsideColor.a - 0.4f);
		yield return new WaitForSeconds(.4f);
		_warshipSprite.color = new Color(_warshipSprite.material.color.r, _warshipSprite.material.color.g, _warshipSprite.material.color.b, _outsideColor.a - 0.6f);
		yield return new WaitForSeconds(.4f);
		_warshipSprite.color = new Color(_warshipSprite.material.color.r, _warshipSprite.material.color.g, _warshipSprite.material.color.b, _insideColor.a);
		_state = EWarshipState.INSIDE;
		yield return null;

	}
	public IEnumerator EnableViewZone()
	{
		_vzScript.Sprite.enabled = true;
		yield return new WaitForSeconds(4);
		_vzScript.Sprite.enabled = false;
		ShowViewZone = false;
		yield return null;
	}

}
