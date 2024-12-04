using UnityEngine;

namespace HT
{
    public struct DayTimeModel
    {
        public int _day { set; get; }
        public int _hour { set; get; }
        public int _minute { set; get; }
        public string _meridian { set; get; }

        public void SetModel(int day, int hour, int minute, float timeOfDay)
        {
            _day = day;
            _hour = (hour > 12) ? hour - 12 : hour;
            _minute = minute;
            _meridian = timeOfDay > 12 ? "PM" : "AM";
        }
    }

    public struct GoatInfoModel
    {
        public string gendder;
        public float stamina;
        public float oestrus;
        public Vector3 screenPos;

        public void SetModel(Gender gen, float stam, float oes, Vector3 pos)
        {
            gendder = (gen == Gender.MALE) ? "M" : "f";
            stamina = stam;
            oestrus = oes;
            screenPos = pos;
        }
    }

    public struct BreedInfoModel
    {
        public int _maleCount;
        public int _femaleCount;

        public void PresentView(UI_BreedInfo_View view,int m, int f)
        {
            _maleCount = m;
            _femaleCount = f;
            view.SetData(this);
        }
    }

    public struct DialogModel{
        public string name;
        public string message;
        public Vector3 pos;

        public void PresentView(UI_Dialog_View view,string n, string m,Vector3 p){
            name = n;
            message = m;
            pos = p;
            view.SetData(this);
        }
    }
    


}
