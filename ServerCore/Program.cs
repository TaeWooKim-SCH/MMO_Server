namespace ServerCore {
    //class Lock {
    //    // bool <- 커널
    //    ManualResetEvent _available = new ManualResetEvent(false);

    //    public void Acquire() {
    //        _available.WaitOne(); // 입장 시도
    //    }

    //    public void Release() {
    //        _available.Set();
    //    }
    //}class Lock {
    //    // bool <- 커널
    //    AutoResetEvent _available = new AutoResetEvent(true);

    //    public void Acquire() {
    //        _available.WaitOne(); // 입장 시도
    //        //_available.Reset(); 위에 WaitOne 안에 들어가 있음
    //    }

    //    public void Release() {
    //        _available.Set();
    //    }
    //}

    // 레이스 컨디션
    internal class Program {
        static int _num = 0;
        //static Lock _lock = new Lock();
        static Mutex _lock = new Mutex(); // 커널 객체

        static void Thread_1() {
            for (int i = 0; i < 100000; i++) {
                _lock.WaitOne();
                _num++;
                _lock.ReleaseMutex();
            }
        }
        
        static void Thread_2() {
            for (int i = 0; i < 100000; i++) {
                _lock.WaitOne();
                _num--;
                _lock.ReleaseMutex();
            }
        }

        static void Main(string[] args) {
            Task t1 = new Task(Thread_1);
            Task t2 = new Task(Thread_2);

            t1.Start();
            t2.Start();

            Task.WaitAll(t1, t2);
            Console.WriteLine(_num);

        }
    }
}
