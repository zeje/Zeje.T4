using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zeje.T4
{
    public class TestClass
    {
        public string name = "zeje";
        public string sex = "男";
        public string province = "广东省";
        public string city = "深圳市";
        public string SelfIntroduction(int i)
        {
            return string.Format("自我介绍{0}次：我的名字是{1],性别{2}，所在地{3}{4}", i.ToString(), name, sex, province, city);
        }
    }

    public class Give
    {
        public Give()
        {
            int i = new Random(100).Next(100);
        }
    }
}
