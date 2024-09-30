namespace ServerCore {
    internal class Program {
        // 1. 근성
        // 2. 양보
        // 3. 갑질

        // 상호배제
        // Monitor
        static object _lock = new object();
        static SpinLock _lock2 = new SpinLock();
        static ReaderWriterLockSlim _lock3 = new ReaderWriterLockSlim();
        // 직접 만든다

        // [] [] [] [] [] -> 퀘스트 보상
        class Reward {

        }


        // 99.999999%
        static Reward GetRewardById(int id) {
            _lock3.EnterReadLock();
            _lock3.ExitReadLock();

            lock (_lock) {

            }

            return null;
        }

        // 0.0000001%
        static void AddReward(Reward reward) {
            _lock3.EnterWriteLock();
            _lock3.ExitReadLock();
        }

        static void Main(string[] args) {
            lock (_lock) {
                
            }
        }
    }
}
