using System;
using System.Threading.Tasks;

namespace AutoTool.AutoCommons
{
    public class WaitHelper
    {
        public TimeSpan TimeOut;
        public WaitHelper(TimeSpan timeOut)
        {
            this.TimeOut = timeOut;
        }
        public TResult Until<TResult>(Func<TResult> condition)
        {
            TResult result = default(TResult);

            Task runCondition = new Task(() =>
            {

                while (true)
                {
                    result = condition.Invoke();
                    if (result != null)
                    {
                        var isBreak = true;
                        foreach (var pi in result.GetType().GetProperties())
                        {
                            if (pi.GetType() == typeof(bool))
                            {
                                bool val = (bool)pi.GetValue(result);
                                if (!val)
                                {
                                    isBreak = false;
                                    break;
                                }
                            }
                        }
                        if (isBreak) break;
                    }
                };
            });
            runCondition.Start();

            Task.WhenAny(runCondition, Task.Delay(this.TimeOut)).Wait();

            return result;
        }
    }

    public class WaitHelper<T>
    {
        public TimeSpan TimeOut;
        public T Input;

        public WaitHelper(T input, TimeSpan timeOut)
        {
            this.Input = input;
            this.TimeOut = timeOut;
        }

        public TResult Until<TResult>(Func<T, TResult> condition)
        {
            TResult result = default(TResult);

            Task runCondition = new Task(() =>
            {

                while (true)
                {
                    result = condition.Invoke(this.Input);
                    if (result != null)
                    {
                        var isBreak = true;
                        foreach (var pi in result.GetType().GetProperties())
                        {
                            if (pi.GetType() == typeof(bool))
                            {
                                bool val = (bool)pi.GetValue(result);
                                if (!val)
                                {
                                    isBreak = false;
                                    break;
                                }
                            }
                        }
                        if (isBreak) break;
                    }
                };
            });
            runCondition.Start();

            Task.WhenAny(runCondition, Task.Delay(this.TimeOut)).Wait();

            return result;
        }
    }
}
