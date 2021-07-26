using System;
using System.Runtime.InteropServices;

namespace _660_demo_cs
{
    public class DLL7660
    {
        //打开设备
        [DllImport("USB7660.dll", CharSet = CharSet.Auto)]
        public static extern Int32 ZT7660_OpenDevice(UInt32 cardNO);
        //关闭设备
        [DllImport("USB7660.dll", CharSet = CharSet.Auto)]
        public static extern Int32 ZT7660_CloseDevice(UInt32 cardNO);
        //得到当前错误号。如果错误号为0表示无错误
        [DllImport("USB7660.dll", CharSet = CharSet.Auto)]
        public static extern Int32 ZT7660_GetLastErr();
        //清除错误号
        [DllImport("USB7660.dll", CharSet = CharSet.Auto)]
        public static extern void ZT7660_ClearLastErr();
        //对指定通道进行单次数据采集
        [DllImport("USB7660.dll", CharSet = CharSet.Auto)]
        public static extern Int32 ZT7660_AIonce(UInt32 cardNO,
                                                 UInt32 chMode,
                                                 UInt32 chNO,
                                                 UInt32 AIrange,
                                                 UInt32 AIAmp,
                                                 UInt32 ADstartMode,
                                                 UInt32 ADfreq,
                                                 UInt32 ABflag,
                                                 UInt32 ch1Flag,
                                                 UInt32 ADctrlWord,
                                                 UInt32 ADoverTime);
        //初始化AD
        [DllImport("USB7660.dll", CharSet = CharSet.Auto)]
        public static extern Int32 ZT7660_AIinit(UInt32 cardNO,
                                                 UInt32 chMode,
                                                 UInt32 chNO,
                                                 UInt32 AIrange,
                                                 UInt32 AIAmp,
                                                 UInt32 ADstartMode,
                                                 UInt32 ADfreq,
                                                 UInt32 ABflag,
                                                 UInt32 ch1Flag,
                                                 UInt32 ADctrlWord,
                                                 UInt32 ADoverTime);
        //清空模块上硬件缓冲区(FIFO)
        [DllImport("USB7660.dll", CharSet = CharSet.Auto)]
        public static extern Int32 ZT7660_ClearFifo(UInt32 cardNO);
        //得到驱动缓冲区中当前数据个数
        [DllImport("USB7660.dll", CharSet = CharSet.Auto)]
        public static extern Int32 ZT7660_GetSFifoDataCnt(UInt32 cardNO);

        //成批读取AD数据
        [DllImport("USB7660.dll", CharSet = CharSet.Auto)]
        public static extern Int32 ZT7660_AIFifoEx(UInt32 cardNO,
                                                  UInt32 wantReadCount,
                                                  [MarshalAs(UnmanagedType.I2)] out UInt16 pResultArr);
        
        //模拟量单次输出
        [DllImport("USB7660.dll", CharSet = CharSet.Auto)]
        public static extern Int32 ZT7660_AOonce(UInt32 cardNO,
            UInt32 chNO,
            UInt32 AOrange,
            Int32 nValue);
        
        //得到指定通道的开关量输入状态
        [DllImport("USB7660.dll", CharSet = CharSet.Auto)]
        public static extern Int32 ZT7660_DIBit(UInt32 cardNO, UInt32 chNO);
        //得到所有通道的开关量输入状态
        [DllImport("USB7660.dll", CharSet = CharSet.Auto)]
        public static extern Int32 ZT7660_DIAll(UInt32 cardNO);
        //指定某通道的开关量输出状态
        [DllImport("USB7660.dll", CharSet = CharSet.Auto)]
        public static extern Int32 ZT7660_DOBit(UInt32 cardNO, UInt32 chNO, UInt32 nState);
        //指定所有通道的开关量输出状态
        [DllImport("USB7660.dll", CharSet = CharSet.Auto)]
        public static extern Int32 ZT7660_DOAll(UInt32 cardNO, UInt32 nStateAll);

    }
}
