﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace incorporates
{
    public class train
    {
        public train()
        {
            newtrain();
        }
        
        public double num_电量
        {
            set
            {
                _电量 = value;
            }
            get
            {
                return _电量;
            }
        }

        public double money_价格
        {
            set
            {
                _价格 = value;
            }
            get
            {
                return _价格;
            }
        }

        public void newtrain()
        {
            num_电量 = instrument.ran.Next(10 , 100);
            money_价格 = instrument.ran.Next(1000);
        }

        private double _价格;
        private double _电量;        

    }
}
