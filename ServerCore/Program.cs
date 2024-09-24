namespace ServerCore {
    internal class Program {
        static void MainTread() {
            while (true) {
                Console.WriteLine("Hello Thread!");
            }
        }

        static void Main(string[] args) {
            Thread t = new Thread(MainTread);
            t.Name = "Test Thread";
            t.IsBackground = true;
            t.Start();
            Console.WriteLine("Wating for Thread!!");
            t.Join();
            Console.WriteLine("Hello, World!");
        }
    }
}
