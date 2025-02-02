﻿using UnityEngine ;
using UnityEngine.UI ;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening ;

public class SwitchToggle : MonoBehaviour {
   [SerializeField] RectTransform uiHandleRectTransform ;
   [SerializeField] Color backgroundActiveColor ;
   [SerializeField] Color handleActiveColor ;

   Image backgroundImage, handleImage ;

   Color backgroundDefaultColor, handleDefaultColor ;

   Toggle toggle ;
   public bool isMute = false;

   Vector2 handlePosition ;

   void Awake ( ) {
      toggle = GetComponent <Toggle> ( ) ;

      handlePosition = uiHandleRectTransform.anchoredPosition ;

      backgroundImage = uiHandleRectTransform.parent.GetComponent <Image> ( ) ;
      handleImage = uiHandleRectTransform.GetComponent <Image> ( ) ;

      backgroundDefaultColor = backgroundImage.color ;
      handleDefaultColor = handleImage.color ;

      toggle.onValueChanged.AddListener (OnSwitch) ;

      if (toggle.isOn)
         OnSwitch (true) ;
   }

   void OnSwitch (bool on) {
      //uiHandleRectTransform.anchoredPosition = on ? handlePosition * -1 : handlePosition ; // no anim
      uiHandleRectTransform.DOAnchorPos (on ? handlePosition * -1 : handlePosition, .4f).SetEase (Ease.InOutBack).SetUpdate(true) ;

      //backgroundImage.color = on ? backgroundActiveColor : backgroundDefaultColor ; // no anim
      backgroundImage.DOColor (on ? backgroundActiveColor : backgroundDefaultColor, .6f).SetUpdate(true) ;

      //handleImage.color = on ? handleActiveColor : handleDefaultColor ; // no anim
      handleImage.DOColor (on ? handleActiveColor : handleDefaultColor, .4f).SetUpdate(true) ;
   }

   private void OnEnable()
   {
      toggle.onValueChanged.AddListener(OnToggleValueChanged);
   }

   private void OnDisable()
   {
      toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
   }

   private void OnToggleValueChanged(bool value)
   {
      if (isMute)
      {
         TopDownMaster.gm.uiManager.MuteToggle(value);
      }
   }

   void OnDestroy ( ) {
      toggle.onValueChanged.RemoveListener (OnSwitch) ;
   }
}
