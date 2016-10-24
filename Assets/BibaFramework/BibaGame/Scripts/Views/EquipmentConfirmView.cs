using BibaFramework.BibaMenu;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace BibaFramework.BibaGame
{
	[RequireComponent(typeof(BibaButtonView))]
	public class EquipmentConfirmView : View
	{
		private BibaButtonView _button;
		public BibaButtonView BibaButtonView { 
			get { 
				if(_button == null)
				{
					_button = (BibaButtonView)GetComponent<BibaButtonView>();
				}
				return _button; 
			} 
		}
	}
}