# ThreadPool Starvation

## 1. Definition

**ThreadPool starvation** occurs when all available .NET ThreadPool worker threads are busy, usually because they are blocked for too long.

As a result, new work has to wait for a ThreadPool thread to become available.

---

## 2. Simple Example

```csharp
Task.Run(() =>
{
    Thread.Sleep(10_000);
});

If many ThreadPool threads are blocked like this, new work may wait.

3. Common Causes
Thread.Sleep() inside ThreadPool work
.Wait() on Tasks
.Result on Tasks
Blocking synchronous I/O
Long-running CPU work
Excessive Task.Run()
Too much ThreadPool work at once
4. ASP.NET Core Example

Bad:

public IActionResult Get()
{
    var result = GetDataAsync().Result;

    return Ok(result);
}

Better:

public async Task<IActionResult> Get()
{
    var result = await GetDataAsync();

    return Ok(result);
}
5. Why It Is Dangerous

ThreadPool starvation can cause:

Increased request latency
Slow APIs
Timeouts
Low throughput
Queued background work
Cascading performance problems
6. How to Prevent It
Prefer async/await for I/O.
Avoid .Result and .Wait().
Avoid unnecessary Thread.Sleep().
Do not block ThreadPool threads unnecessarily.
Limit CPU-intensive parallel work.
Use ParallelOptions when appropriate.
Use queues/background workers for long-running work.
Monitor ThreadPool behavior.

7. ThreadPool Starvation vs Deadlock
| ThreadPool Starvation                     | Deadlock                            |
| ----------------------------------------- | ----------------------------------- |
| Threads are occupied/blocking             | Threads wait for each other         |
| New work cannot get threads quickly       | Waiting cycle prevents progress     |
| Often caused by excessive blocking        | Often caused by lock ordering       |
| Can recover when threads become available | Usually requires breaking the cycle |

8. Interview Point

ThreadPool starvation happens when ThreadPool threads are exhausted or unavailable for new work, commonly because existing work is blocking them for too long.

Key Points
Blocking ThreadPool threads → starvation risk
async/await → avoids unnecessary blocking
.Result / .Wait() → common warning signs
ThreadPool starvation ≠ deadlock