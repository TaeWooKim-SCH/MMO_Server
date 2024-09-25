namespace ServerCore {
    internal class Program {
        static void MainTread(object state) {
            for (int i = 0; i < 5; i++) {
                Console.WriteLine("Hello Thread!");
            }
        }

        static void Main(string[] args) {
            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(5, 5);

            for (int i = 0; i < 5; i++) {
                Task t = new Task(() => { while (true) { } }, TaskCreationOptions.LongRunning);
                t.Start();
            }

            //for (int i = 0; i< 4; i++) {
            //    ThreadPool.QueueUserWorkItem((obj) => { while (true) { } });
            //}
            ThreadPool.QueueUserWorkItem(MainTread);

            //for (int i = 0; i < 1000; i++) {
            //    Thread t = new Thread(MainTread);
            //    t.Name = "Test Thread";
            //    t.IsBackground = true;
            //    t.Start();
            //}
            //Console.WriteLine("Wating for Thread!!");

            //t.Join();
            //Console.WriteLine("Hello, World!");
            while (true) {

            }
        }
    }
}
