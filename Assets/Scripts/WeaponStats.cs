using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [SerializeField] private float force = 1.0f;
    public float Force { get => force; set => force = value; }
    [SerializeField] private string key = "x";
    public string Key { get => key; set => key = value; }
    [SerializeField] private string animationTrigger = null;
    public string AnimationTrigger { get => animationTrigger; set => animationTrigger = value; }
}
