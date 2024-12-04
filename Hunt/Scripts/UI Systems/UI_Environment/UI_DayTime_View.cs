using UnityEngine;
using UnityEngine.UI;

namespace HT
{
    public class UI_DayTime_View : UI_Views
    {
        public Text time_text;
        public Text day_text;

        public void SetData(DayTimeModel model)
        {
            if (time_text == null || day_text==null) { return; }
            day_text.text = "DAY "+model._day.ToString();
            time_text.text = model._hour.ToString() + " : " + model._minute.ToString() + " " + model._meridian ;
        }
    }

}