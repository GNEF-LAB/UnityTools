using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;
using System;

namespace JHF
{
    public static class Utils
    {
        public static bool IsPointerOverUIObject()
        {//判断是否点击的是UI，有效应对安卓没有反应的情况，true为UI
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }


        public static T FindInParents<T>(GameObject go) where T : Component
        {
            if (go == null) return null;
            var comp = go.GetComponent<T>();

            if (comp != null)
                return comp;

            var t = go.transform.parent;
            while (t != null && comp == null)
            {
                comp = t.gameObject.GetComponent<T>();
                t = t.parent;
            }
            return comp;
        }
        //Transform GetTransformRecursively(Transform check, string name)
        //{
        //    Transform forreturn = null;

        //    foreach (Transform t in check.GetComponentsInChildren<Transform>())
        //    {
        //        if (t.name == name)
        //        {
        //            Debug.Log("得到最终子物体的名字是：" + t.name);
        //            forreturn = t;
        //            return t;

        //        }

        //    }
        //    return forreturn;
        //}

        public static T GetComponentRecursively<T>(Transform root)
        {
            T t = root.GetComponentInChildren<T>();

            if (t == null)
            {
                foreach (Transform child in root)
                {
                    t = GetComponentRecursively<T>(child);
                    if (t != null)
                    {
                        break;
                    }

                }
            }
            return t;
        }

        #region webgl版的OpenFileDialog
        [DllImport("__Internal")]
        private static extern void clickSelectFileBtn();

        /// <summary>
        /// 点击Open按钮
        /// </summary>
        public static void ClickSelectFileBtn()
        {
#if UNITY_WEBGL
            clickSelectFileBtn();
#endif
        }
        #endregion

        public static Texture2D GetBase64(string base64Str)
        {
            byte[] bs = Convert.FromBase64String(base64Str);
            Texture2D pic = new Texture2D(256, 256, TextureFormat.RGBA32, false);
            pic.LoadImage(bs);
            return pic;
        }
    }

}
