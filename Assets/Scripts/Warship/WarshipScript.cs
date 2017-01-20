using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWarshipType { W0, W1, W2, W3 }; //a definir

public class WarshipScript : MonoBehaviour
{

	[SerializeField]
	private SpriteRenderer _warshipSprite;

	[SerializeField]
	private EWarshipType _type;
	public EWarshipType Type //TODO: a voir, sinon faire le choix lors du bouton play pour éviter de faire un new a chaque choix de ship
	{
		get { return _type; }
		set	{ _type = value;}
	}
	private WarshipAttributes _attributes = new WarshipDefault();


	public void Init(EWarshipType type)
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
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		//Debug.Log(_attributes.Ammunition);
	}
}
