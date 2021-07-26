using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using _660_demo_cs;
using Unity.Mathematics;
using UnityEngine;


namespace MultithreadingApplication
{
    class Pid
    {
        public double Kp;
        public double Ki;
        public double Kd;
        public double friction = 2000.0;
        public double MAXLIM = 8000.0;
        public double MINLIM = -8000.0;
        public double positive_index = 1.5;
        //public double multi_calc = 1;
        public double lastNum;
        public Pid()
        {

        }

        public Pid(double kp, double ki, double kd)
        {
            this.Kp = kp;
            this.Ki = ki;
            this.Kd = kd;
        }


        public double ExpectedValue;
        public double ActualValue; //
        public double output_volt;
        /// <summary>
        /// 定义偏差值
        /// </summary>
        public double err; //

        /// <summary>
        /// 定义上一个偏差值
        /// </summary>
        public double err_last; //

        /// <summary>
        /// 定义前一个的偏差值
        /// </summary>
        public double err_prev; //


        /// <summary>
        /// 参数初始化
        /// </summary>
        public void this_Init()
        {
            this.ExpectedValue = 0.0f;
            this.ActualValue = 0.0f;
            this.output_volt = 0.0f;
            this.err = 0.0f;
            this.err_prev = 0.0f;   // two before
            this.err_last = 0.0f;   // one before
        }


        /// <summary>
        /// 增量式this公式实现
        /// </summary>
        /// <param name="reference_voltage"></param>
        /// <returns></returns>
        public double this_Realize(double reference_voltage)
        {
            double index, maxlim, minlim;
            this.ExpectedValue = reference_voltage;
            //this.ActualValue = actual_voltage;
            this.err = this.ExpectedValue - this.ActualValue;
            //eliminate the distance deadzone

            //TODO:err
            double incrementValue = this.Kp * (this.err - this.err_last) + this.Ki * this.err -
                                                this.Kd * (this.err - 2 * this.err_last + this.err_prev);
            this.output_volt += incrementValue;
        
  
            
            //TODO:DON'T EVER WRITE THESE LINES IN YOUR PROGRAM AGAIN!
            //Debug.Log(Ki);
            //Debug.Log(Kp);
            //Debug.Log(Kd);

            this.err_prev = this.err_last;
            this.err_last = this.err;

            if (this.output_volt > MAXLIM)
            {
                this.output_volt = MAXLIM;
            }

            if (this.output_volt < MINLIM)
            {
                this.output_volt = MINLIM;
            }

            //calculate the deadzone of output forces
            if (this.output_volt > 0)
            {
                return (this.output_volt  + friction);
            }
            else
            {
                return (this.output_volt * positive_index - friction);
            }
        }
    }

    
    
    public class ThreadProgram: MonoBehaviour
    {
        private static bool sampleFlag = false;
        //private int tempOutput;
        static Device device = new Device();
        private static double tsPercision = 0.05;
        private static int stableDelay = 50;
        
        public static void CallToChildThread()
        {
            int tempOutput;
            Console.WriteLine("Child thread starts");
            if (true)

            {
                device = new Device();
                device.Device_Open();
                Pid pid_step = new Pid(GlobalVariablesController.stepKp, GlobalVariablesController.stepKi,
                    GlobalVariablesController.stepKd);
                pid_step.this_Init();
                bool direction = false;
                
                while (true)
                {
                    if (sampleFlag)
                    {
                        if (!GlobalVariablesController.startUpdate)
                            break;

                        
                        GlobalVariablesController.voltageArray.RemoveAt(0);
                        GlobalVariablesController.voltageArray.Add(GlobalVariablesController.systemInput);
                        GlobalVariablesController.voltageArray2.RemoveAt(0);
                        var s = Convert.ToInt32(SignalGenerator());
                        GlobalVariablesController.voltageArray2.Add(s);
                        device.Sampler();
                        List<Int32> outint = new List<Int32>();
                        double reference_voltage = s;
                        pid_step.ActualValue = device.readData;

                        bool pidDirection = true;
                        Int32 outnum;
                        if (pid_step.lastNum < 0)
                        {
                            pid_step.Kp = !GlobalVariablesController.sineMode
                                ? GlobalVariablesController.stepKpReverse
                                : GlobalVariablesController.sineKpReverse;
                            pid_step.Ki = !GlobalVariablesController.sineMode
                                ? GlobalVariablesController.stepKiReverse
                                : GlobalVariablesController.sineKiReverse;
                            pid_step.Kd = !GlobalVariablesController.sineMode
                                ? GlobalVariablesController.stepKdReverse
                                : GlobalVariablesController.sineKdReverse;
                            outnum = Convert.ToInt32(pid_step.this_Realize(reference_voltage));
                        }
                        else
                        {
                            pid_step.Kp = !GlobalVariablesController.sineMode
                                ? GlobalVariablesController.stepKp
                                : GlobalVariablesController.sineKp;
                            pid_step.Ki = !GlobalVariablesController.sineMode
                                ? GlobalVariablesController.stepKi
                                : GlobalVariablesController.sineKi;
                            pid_step.Kd = !GlobalVariablesController.sineMode
                                ? GlobalVariablesController.stepKd
                                : GlobalVariablesController.sineKd;
                            outnum = Convert.ToInt32(pid_step.this_Realize(reference_voltage));
                        }
                        outint.Add(outnum);
                        Console.WriteLine("readData:" + device.readData);
                        GlobalVariablesController.systemInput=device.readData;
                        Console.WriteLine("outint count: " + outint.Count);

                        pid_step.lastNum = outnum;
                        // device.Output(2100);
                        tempOutput = Convert.ToInt32(pid_step.this_Realize(reference_voltage));
                        GlobalVariablesController.myCuteLittleText = tempOutput.ToString();
                        device.Output(tempOutput);
                        //TODO:CaLculate parameters here:
                        CalculatePerformanceIndex();
                        sampleFlag = false;
                    }
                    
                }
                device.Output(0);
                device.Device_Close();
            }
        }

        private void OnApplicationQuit()
        {
            device.Output(0);
            device.Device_Close();
        }

        public static void Timer()
        {
            var samplingTimeInSecond = GlobalVariablesController.samplingTime / 1000;
            while (true)
            {
                if (!GlobalVariablesController.startUpdate)
                    break;
                Thread.Sleep((int) GlobalVariablesController.samplingTime);
                if (GlobalVariablesController.sineMode)
                {
                    GlobalVariablesController.sineWaveTimer += samplingTimeInSecond;
                }
                else
                {
                    if (GlobalVariablesController.squareWaveMode)
                    {
                        GlobalVariablesController.squareWaveTimer += samplingTimeInSecond;
                    }
                }

                sampleFlag = true;
            }
        }
        
        public void StartChildThread()
        {
            ThreadStart childref = new ThreadStart(CallToChildThread);
            Console.WriteLine("In Main: Creating the Child thread");
            Thread childThread = new Thread(childref);
            Thread timerThread = new Thread(Timer);
            childThread.Start();
            timerThread.Start();
            //Console.ReadKey();
        }

        //Find the sign of derivative of a voltage array by average of 10 points
        private static bool FindDerivativeSign10(List<int> volatgeArray)
        {
            var i = volatgeArray.Count - 1;
            return volatgeArray[i] + volatgeArray[i-1] + volatgeArray[i-2] + volatgeArray[i - 3] + volatgeArray[i - 4] -
                volatgeArray[i - 5] - volatgeArray[i-6] - volatgeArray[i-7] - volatgeArray[i-8] - volatgeArray[i-9] > 0;
        }

        //Find the sign of derivative of a voltage array
        private static bool FindDerivativeSign2(List<int> volatgeArray, int i)
        {
            return volatgeArray[i] + volatgeArray[i-1] + volatgeArray[i-2] - volatgeArray[i - 3] - volatgeArray[i - 4] -
                   volatgeArray[i - 5] > 0;
        }

        private static void CalculatePerformanceIndex()
        {
            if (!GlobalVariablesController.sineMode)
            {
                if (GlobalVariablesController.MeasureTimeCounter <= 1000)
                    GlobalVariablesController.MeasureTimeCounter += 1;

                //Measure Tp
                if (GlobalVariablesController.TpMeasuringFlag && GlobalVariablesController.MeasureTimeCounter > 20)
                {
                    if (FindDerivativeSign10(GlobalVariablesController.voltageArray) !=
                        GlobalVariablesController.IsRisingSession)
                    {
                        GlobalVariablesController.tp = (float)(GlobalVariablesController.MeasureTimeCounter - 5) / 100;
                        //Debug.Log("0");
                        GlobalVariablesController.TpMeasuringFlag = false;
                    }
                }
            
                //Measure Ts
                if (GlobalVariablesController.TsMeasuringFlag)
                {
                    if (Math.Abs((GlobalVariablesController.voltageArray.Last() -
                                  GlobalVariablesController.voltageArray2.Last()) / GlobalVariablesController.DeltaAmp) < tsPercision)
                    {
                        if (GlobalVariablesController.IsStable == false)
                        {
                            GlobalVariablesController.IsStable = true;
                            GlobalVariablesController.TsTimeCounter = 0;
                        }
                        else
                        {
                            GlobalVariablesController.TsTimeCounter++;
                            if (GlobalVariablesController.TsTimeCounter >= stableDelay)
                            {
                                GlobalVariablesController.ts =
                                    (float) (GlobalVariablesController.MeasureTimeCounter - stableDelay) / 100;
                                GlobalVariablesController.TsMeasuringFlag = false;
                            }
                        }
                    }
                    else
                    {
                        GlobalVariablesController.IsStable = false;
                    }
                }
            
                //Measure Ess
                if (!GlobalVariablesController.TpMeasuringFlag && !GlobalVariablesController.TsMeasuringFlag)
                {
                    float tempInt = 0;
                    for (int j = GlobalVariablesController.voltageArray.Count - 1;
                        j >= GlobalVariablesController.voltageArray.Count - 50;
                        j--)
                    {
                        tempInt += GlobalVariablesController.voltageArray[j];
                    }

                    GlobalVariablesController.ess =
                        Math.Abs(tempInt / 50 - GlobalVariablesController.voltageArray2.Last()) / 40;
                }
            }
            else
            {
                //TODO: Improve phase delta calculation function
                if ((GlobalVariablesController.voltageArray2[GlobalVariablesController.voltageArray2.Count - 2] > 5000 &&
                     GlobalVariablesController.voltageArray2.Last() <= 5000) ||
                    (GlobalVariablesController.voltageArray2[GlobalVariablesController.voltageArray2.Count - 2] < 5000 &&
                     GlobalVariablesController.voltageArray2.Last() >= 5000))
                {
                    GlobalVariablesController.PhaseCounter = 0;
                    //Debug.Log("0");
                }
                if ((GlobalVariablesController.voltageArray[GlobalVariablesController.voltageArray.Count - 2] > 5000 &&
                     GlobalVariablesController.voltageArray.Last() <= 5000) ||
                    (GlobalVariablesController.voltageArray[GlobalVariablesController.voltageArray.Count - 2] < 5000 &&
                     GlobalVariablesController.voltageArray.Last() >= 5000))
                {
                    GlobalVariablesController.deltaPhase = (float)GlobalVariablesController.PhaseCounter/100;
                    //Debug.Log("0!");
                }

                GlobalVariablesController.PhaseCounter++;
            }

        }
        public static int SignalGenerator()
        {
            int outputNum;
            if (!GlobalVariablesController.sineMode)
            {
                if (!GlobalVariablesController.squareWaveMode)
                {
                    return (int)(40*GlobalVariablesController.stepAmplitude);
                }
                else
                {
                    var squareWavePeriod = 1 / GlobalVariablesController.squareWaveFrequency;
                    if (GlobalVariablesController.squareWaveTimer > squareWavePeriod)
                    {
                        GlobalVariablesController.squareWaveTimer -= squareWavePeriod;
                    }
                    outputNum =  (GlobalVariablesController.squareWaveTimer > 0.5 * squareWavePeriod)
                        ? (int)(5000+20*GlobalVariablesController.stepAmplitude)
                        : (int)(5000-20*GlobalVariablesController.stepAmplitude);

                    if (outputNum != GlobalVariablesController.voltageArray2.Last())
                    {
                        GlobalVariablesController.MeasureTimeCounter = 0;
                        GlobalVariablesController.TpMeasuringFlag = true;
                        GlobalVariablesController.TsMeasuringFlag = true;
                        GlobalVariablesController.DeltaAmp = Math.Abs(GlobalVariablesController.voltageArray.Last() - outputNum);
                        GlobalVariablesController.IsRisingSession = GlobalVariablesController.voltageArray.Last() < outputNum; 
                        GlobalVariablesController.tp = -1;
                        GlobalVariablesController.ts = -1;
                        GlobalVariablesController.ess = -1;
                    }
                    
                    return outputNum;
                }
            }
            else
            {
                var sineWavePeriod = 1 / GlobalVariablesController.sineFrequency;
                
                if (GlobalVariablesController.sineWaveTimer - 1 > sineWavePeriod)
                {
                    GlobalVariablesController.sineWaveTimer -= sineWavePeriod;
                }
                return (int)(5000 + 40 * GlobalVariablesController.sineAmplitude *
                    math.sin(GlobalVariablesController.sineWaveTimer * GlobalVariablesController.sineFrequency*2*math.PI));
            }
        
        }
    }
    
}
