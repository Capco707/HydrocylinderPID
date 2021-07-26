using System;
using System.Collections.Generic;


namespace _660_demo_cs
{
    public class Device
    {
        UInt32 m_cardNO = 1; 
        // List<UInt16> m_dataArr;
        public Int32 readData;
        UInt32 m_chCnt = 48;

        public void Device_Open()
        {
            if (DLL7660.ZT7660_OpenDevice(m_cardNO) != 0)
            {
                GlobalVariablesController.consoleText += ("Open device is failed!\n");
                //return;
            }
            GlobalVariablesController.consoleText += ("Open_Device Errorcode: " + DLL7660.ZT7660_GetLastErr() + '\n');
        }

        public void Device_Close()
        {
            DLL7660.ZT7660_ClearLastErr();
            DLL7660.ZT7660_CloseDevice(m_cardNO);
        }

       public void Init_AD() 
       {
           DLL7660.ZT7660_ClearFifo(m_cardNO);
           DLL7660.ZT7660_AIinit(m_cardNO, 1, m_chCnt, 2, 0, 0, 4, 0, 0, 0, 0); 
           Console.WriteLine("Start_ADErrorcode: " + DLL7660.ZT7660_GetLastErr()+ '\n');
       }

       public void Sampler()
       {
           readData = DLL7660.ZT7660_AIonce(m_cardNO, 0, 21, 2, 0, 0, 4, 0, 0, 0, 0); //?????????
           Console.WriteLine("????????:" + readData);
            
           if (DLL7660.ZT7660_GetLastErr() != 0) GlobalVariablesController.consoleText +=("Sample Errorcode: " + DLL7660.ZT7660_GetLastErr()+ '\n');
        }
        
       public void Output(Int32 data)
        {
            DLL7660.ZT7660_AOonce(m_cardNO, chNO: 1, 6, data);
                Console.WriteLine("Output data: " + data);
                if (DLL7660.ZT7660_GetLastErr() != 0) GlobalVariablesController.consoleText +=("Output Errorcode: " + DLL7660.ZT7660_GetLastErr()+ '\n');
        }
    }
}