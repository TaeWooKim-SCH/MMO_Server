namespace ServerCore {
    internal class Program {
        static volatile int count = 0;
        static Lock _lock = new Lock();

        static void Main(string[] args) {
            Task t1 = new Task(delegate () {
                _lock.WriteLock();
                count++;
                _lock.WriteUnlock();
            });
            Task t2 = new Task(delegate () {
                _lock.WriteLock();
                count--;
                _lock.WriteUnlock();
            });

            t1.Start();
            t2.Start();

            Task.WaitAll(t1, t2);

            Console.WriteLine(count);
        }
    }
}
