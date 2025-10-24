using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaderToggles : MonoBehaviour
{
    bool _diffuse = true;
    bool _ambient = true;
    bool _specular = true;
    bool _toon = true;

    public Image _diffuseButton;
    public Image _ambientButton;
    public Image _specularButton;
    public Image _toonButton;

    public Color _active;
    public Color _inactive;

    public List<Material> _materials = new List<Material>();

    public void ToggleDiffuse()
    {
        _diffuse = !_diffuse;
        _diffuseButton.color = _diffuse ? _active :_inactive;
        foreach (Material mat in _materials)
        {
            mat.SetFloat("_UseDiffuse", _diffuse ? 1 : 0);
        }
    }
    public void ToggleAmbient()
    {
        _ambient = !_ambient;
        _ambientButton.color = _ambient ? _active : _inactive;
        foreach (Material mat in _materials)
        {
            mat.SetFloat("_UseAmbient", _ambient ? 1 : 0);
        }
    }
    public void ToggleSpecular()
    {
        _specular = !_specular;
        _specularButton.color = _specular ? _active : _inactive;
        foreach (Material mat in _materials)
        {
            mat.SetFloat("_UseSpecular", _specular ? 1 : 0);
        }
    }
    public void ToggleToon()
    {
        _toon = !_toon;
        _toonButton.color = _toon ? _active : _inactive;
        foreach (Material mat in _materials)
        {
            mat.SetFloat("_UseToon", _toon ? 1 : 0);

        }
    }
}
