using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewZoneScript : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer _sprite;

	[SerializeField]
	private ViewZoneCollider _vzCollider;

	private float _defaultAlpha;

	// Use this for initialization
	void Start()
	{
		_defaultAlpha = _sprite.color.a;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void StartAscendCoroutine()
	{
		StartCoroutine("AscendCoroutine");
	}
	public void StartDescendCoroutine()
	{
		StartCoroutine("DescendCoroutine");
	}

	public IEnumerator AscendCoroutine()
	{
		_sprite.color = new Color(_sprite.material.color.r, _sprite.material.color.g, _sprite.material.color.b, _defaultAlpha - 0.2f);
		yield return new WaitForSeconds(.4f);
		_sprite.color = new Color(_sprite.material.color.r, _sprite.material.color.g, _sprite.material.color.b, _defaultAlpha - 0.4f);
		yield return new WaitForSeconds(.4f);
		_sprite.color = new Color(_sprite.material.color.r, _sprite.material.color.g, _sprite.material.color.b, _defaultAlpha - 0.6f);
		yield return new WaitForSeconds(.4f);
		_sprite.color = new Color(_sprite.material.color.r, _sprite.material.color.g, _sprite.material.color.b, _defaultAlpha - 0.8f);
		yield return new WaitForSeconds(.4f);
		_sprite.color = new Color(_sprite.material.color.r, _sprite.material.color.g, _sprite.material.color.b, 0);
		yield return null;

	}

	public IEnumerator DescendCoroutine()
	{
		_sprite.color = new Color(_sprite.material.color.r, _sprite.material.color.g, _sprite.material.color.b, _sprite.material.color.a + 0.2f);
		yield return new WaitForSeconds(.4f);
		_sprite.color = new Color(_sprite.material.color.r, _sprite.material.color.g, _sprite.material.color.b, _sprite.material.color.a + 0.2f);
		yield return new WaitForSeconds(.4f);
		_sprite.color = new Color(_sprite.material.color.r, _sprite.material.color.g, _sprite.material.color.b, _defaultAlpha);
		yield return null;
	}
}
