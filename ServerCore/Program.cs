namespace ServerCore {
    // 레이스 컨디션
    internal class Program {
        static int number = 0;
        static object _obj = new object();

        static void Thread_1() {
            // 상호배체 Mutual Exclusive
            for (int i = 0; i< 100000; i++) {

                lock (_obj) {
                    number++;
                }
            }
        }

        // 데드락(DeadLock) -> 죽은 상황
        static void Thread_2() {
            for (int i = 0;i < 100000;i++) {
                lock (_obj) {
                    number--;
                }
            }
        }

        static void Main(string[] args) {

            Task t1 = new Task(Thread_1);
            Task t2 = new Task(Thread_2);

            t1.Start();
            t2.Start();

            Task.WaitAll(t1, t2);

            Console.WriteLine(number);
        }
    }
}
