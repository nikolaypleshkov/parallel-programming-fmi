using System.Collections.Generic;
using System.Threading;


namespace BarSimulator
{
    class Bar
    {
        List<Visitor> visitors = new List<Visitor>();
        Semaphore semaphore = new Semaphore(10, 10);

        public void Enter(Visitor visitor)
        {
            semaphore.WaitOne();
            lock (visitors)
            {
                visitors.Add(visitor);
            }
        }
        
        public int GetBarVisitors()
        {
            return visitors.Count;
        }

        public void Leave(Visitor visitor)
        {
            lock (visitors)
            {
                visitors.Remove(visitor);
            }
            semaphore.Release();
        }
    }
}
