using UnityEngine;
using UnityEngine.UI;
using System;

//##########################
// Class Declaration
//##########################
[ExecuteInEditMode]
public class MaterialUICutoff : MonoBehaviour
{
	//=======================
	// Variables
	//=======================
	public Image image;
	[SerializeField]
	protected float _cutoff;
	[NonSerialized]
	public float lastCutoff; // need these for the animator... cuz it cant animate accessors...
	[SerializeField]
	protected float _softness;
	[NonSerialized]
	public float lastSoftness;
	[SerializeField]
	protected Texture _finalTexture;
	[NonSerialized]
	public Texture lastFinalTexture;
	[SerializeField]
	protected Color _finalTint = Color.white;
	[NonSerialized]
	public Color lastFinalTint;
	
	//=======================
	// Initialization
	//=======================
	protected virtual void Awake()
	{
		if ( image != null && image.material != null )
		{
			// Load defaults from material
			_cutoff = image.material.GetFloat( "_Cutoff" );
			_softness = image.material.GetFloat( "_Softness" );
			_finalTexture = image.material.GetTexture( "_FinalTex" );
			_finalTint = image.material.GetColor( "_FinalTint" );
			
			// Create material instance
			if ( Application.isPlaying )
			{
				Material tempMaterial = Instantiate( image.material );
				image.material = tempMaterial;
			}
		}
	}
	
	//=======================
	// Cutoff
	//=======================
	public virtual float cutoff
	{
		get
		{
			return _cutoff;
		}
		set
		{
			setCutoff( value );
		}
	}
	
	public virtual void setCutoff( float tCutoff )
	{
		if ( tCutoff != _cutoff )
		{
			float tempOld = _cutoff;
			_cutoff = tCutoff;
			effectsCutoff( tempOld );
		}
	}
	
	protected virtual void effectsCutoff( float tOld )
	{
		if ( image != null && image.material != null )
		{
			image.material.SetFloat( "_Cutoff", _cutoff );
		}
	}
	
	//=======================
	// Softness
	//=======================
	public virtual float softness
	{
		get
		{
			return _softness;
		}
		set
		{
			setSoftness( value );
		}
	}
	
	public virtual void setSoftness( float tSoftness )
	{
		if ( tSoftness != _softness )
		{
			float tempOld = _softness;
			_softness = tSoftness;
			effectsSoftness( tempOld );
		}
	}
	
	protected virtual void effectsSoftness( float tOld )
	{
		if ( image != null && image.material != null )
		{
			image.material.SetFloat( "_Softness", _softness );
		}
	}
	
	//=======================
	// Final Texture
	//=======================
	public virtual Texture finalTexture
	{
		get
		{
			return _finalTexture;
		}
		set
		{
			setFinalTexture( value );
		}
	}
	
	public virtual void setFinalTexture( Texture tTexture )
	{
		if ( tTexture != _finalTexture )
		{
			Texture tempOld = _finalTexture;
			_finalTexture = tTexture;
			effectsFinalTexture( tempOld );
		}
	}
	
	protected virtual void effectsFinalTexture( Texture tOld )
	{
		if ( image != null && image.material != null )
		{
			image.material.SetTexture( "_FinalTex", _finalTexture );
		}
	}
	
	//=======================
	// Final Tint
	//=======================
	public virtual Color finalTint
	{
		get
		{
			return _finalTint;
		}
		set
		{
			setFinalTint( value );
		}
	}
	
	public virtual void setFinalTint( Color tTint )
	{
		if ( tTint != _finalTint )
		{
			Color tempOld = _finalTint;
			_finalTint = tTint;
			effectsFinalTint( tempOld );
		}
	}
	
	protected virtual void effectsFinalTint( Color tOld )
	{
		if ( image != null && image.material != null )
		{
			image.material.SetColor( "_FinalTint", _finalTint );
		}
	}
	
	//=======================
	// Tick
	//=======================
	protected virtual void LateUpdate() // unity needs to enable accessor animations so we can avoid this extra tick
	{
		if ( _cutoff != lastCutoff )
		{
			lastCutoff = _cutoff;
			effectsCutoff( lastCutoff );
		}
		
		if ( _softness != lastSoftness )
		{
			lastSoftness = _softness;
			effectsSoftness( lastSoftness );
		}
		
		if ( _finalTexture != lastFinalTexture )
		{
			lastFinalTexture = _finalTexture;
			effectsFinalTexture( lastFinalTexture );
		}
		
		if ( _finalTint != lastFinalTint )
		{
			lastFinalTint = _finalTint;
			effectsFinalTint( lastFinalTint );
		}
	}
}