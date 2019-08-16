﻿using CompileTimeWeaver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompileTimeWeaverFodyDemo
{
    public class TimeItAttribute : AdviceAttribute
    {
        private Stopwatch Timer;
        public override object Advise(IInvocation invocation)
        {
            // do something before target method is Called
            // ...
            Console.WriteLine($"Entering {invocation.Method.Name}()");
            Timer = new Stopwatch();
            Timer.Start();
            try
            {
                return invocation.Proceed();    // call the next advice in the "chain" of advice pipeline, or call target method
            }
            catch (Exception e)
            {
                // do something when target method throws exception
                // ...
                Console.WriteLine($"TimeIt catches an exception: {e.Message}");
                throw;
            }
            finally
            {
                // do something after target method is Called
                // ...
                Timer.Stop();
                Console.WriteLine($"Leaving {invocation.Method.Name}()");
                Console.WriteLine($"Time taken by method {invocation.Method.Name}() is {Timer.ElapsedMilliseconds}ms.");
            }
        }

        //The below method needs to be overriden even though we are not using it in this project.
        public override async Task<object> AdviseAsync(IInvocation invocation)
        {
            // do something before target method is Called
            // ...
            Console.WriteLine($"Entering async {invocation.Method.Name}()");

            try
            {
                return await invocation.ProceedAsync().ConfigureAwait(false);   // asynchroniously call the next advice in advice pipeline, or call target method
            }
            catch (Exception e)
            {
                // do something when target method throws exception
                // ...
                Console.WriteLine($"TimeIt catches an exception: {e.Message}");
                throw;
            }
            finally
            {
                // do something after target method is Called
                // ...
                Console.WriteLine($"Leaving async {invocation.Method.Name}()");
            }
        }
    }

   
}
