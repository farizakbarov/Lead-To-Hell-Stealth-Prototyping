using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LTH_QualitySettings", menuName = "LTH Game Settings/LTH QualitySettings Asset", order = 2)]
public class LTH_QualitySettings : ScriptableObject {

	public bool Quality_aa;
	public bool Quality_LensEffects;
	public bool Quality_Dof;
	public bool Quality_AO;
	public bool Quality_MotionBlur;
	public bool BlackAndWhiteMode;
	public int AA_Type;
}
