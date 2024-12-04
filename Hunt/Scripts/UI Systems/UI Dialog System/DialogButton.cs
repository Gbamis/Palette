using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HT
{
    public class DialogButton : MonoBehaviour, IPointerClickHandler
    {
        private Action action;
        private Action complete;
        [SerializeField] private Text buttonText;

        private Dialog dialog;
        private Response response;

        public void Create(Dialog m_dialog, Response m_response, Action comp)
        {
            dialog = m_dialog;
            response = m_response;

            buttonText.text = m_response.text;
            action = m_response.dialogAction.OnExecuteDialogAction;
            complete = comp;
            float x = 0.2084174f;
            gameObject.GetComponent<RectTransform>().localScale = Vector3.one * x;
        }

        public void OnPointerClick(PointerEventData ped)
        {
            action.Invoke();
            complete.Invoke();
            Debug.Log("res next is:" + response.branchDialogStatement);
        }
    }

}