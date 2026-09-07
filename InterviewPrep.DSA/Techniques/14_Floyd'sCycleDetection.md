1. Definition

Floyd's Cycle Detection is a pointer technique used to detect whether a sequence of nodes contains a cycle.

Floyd's Cycle Detection is also called the Tortoise and Hare technique.

It is mainly used to detect a cycle in a linked list.

It uses two pointers:

Slow (Tortoise) → moves 1 step
Fast (Hare) → moves 2 steps

If a cycle exists, the fast pointer will eventually meet the slow pointer.

If there is no cycle, the fast pointer reaches null.

Idea

Use two pointers:

Slow → moves 1 step
Fast → moves 2 steps

If there is a cycle, eventually:

slow == fast

If there is no cycle, fast reaches null.


2. Basic Example

Consider this linked list:

1 → 2 → 3 → 4
        ↑     ↓
        └─────┘

There is a cycle:

3 → 4 → 3 → 4 → 3 → ...

Pointers start at the head:

slow = 1
fast = 1
Movement
Step 1:

1 → 2 → 3 → 4
    ↑
   slow

        ↑
       fast

After one iteration:

slow = 2
fast = 3

Next:

slow = 3
fast = 3

They meet.

Therefore:

Cycle exists
3. Process / Steps
Step 1 — Create two pointers
ListNode slow = head;
ListNode fast = head;

Both start from the beginning.

Step 2 — Move slow by one
slow = slow.Next;
Step 3 — Move fast by two
fast = fast.Next.Next;
Step 4 — Compare them
if (slow == fast)
    return true;

If they meet → cycle exists.

Step 5 — Stop if fast reaches the end
while (fast != null && fast.Next != null)

If this condition eventually becomes false:

No cycle
4. C# Example
public class ListNode
{
    public int Value;
    public ListNode? Next;

    public ListNode(int value)
    {
        Value = value;
    }
}

Detection:

public static bool HasCycle(ListNode? head)
{
    ListNode? slow = head;
    ListNode? fast = head;

    while (fast != null && fast.Next != null)
    {
        slow = slow!.Next;
        fast = fast.Next.Next;

        if (slow == fast)
            return true;
    }

    return false;
}
5. Example Without Cycle
1 → 2 → 3 → 4 → null

Eventually:

fast → null

Therefore:

return false;
6. Example With Cycle
1 → 2 → 3 → 4
        ↑     ↓
        └─────┘

Eventually:

slow == fast

Therefore:

return true;
7. Why Does It Work?

Think about a circular running track.

One person runs at 1 step/second.

Another runs at 2 steps/second.

If they are running around a circular track, the faster person will eventually catch the slower person.

That's exactly what happens inside a linked-list cycle.

Slow → 1 step
Fast → 2 steps

       ↓
   ┌─────────┐
   │         │
   │  cycle  │
   │         │
   └─────────┘
       ↑
       │
They eventually meet
8. Complexity
Complexity	Value	Why
Time	O(n)	Pointers traverse the list
Space	O(1)	Only two pointers are used

This is the major advantage of Floyd's algorithm.

9. Advantages
1. Constant extra space

Only:

slow
fast

are required.

Space = O(1)
2. No HashSet required

Another solution is:

HashSet<ListNode>

You store every visited node.

That requires:

Space = O(n)

Floyd avoids this.

3. Optimal for cycle detection

For a linked-list cycle detection problem, Floyd's technique gives:

Time  → O(n)
Space → O(1)
10. Disadvantages
Mainly useful when you have a next-pointer/sequence structure.
The basic version only tells you whether a cycle exists.
To find the starting node of the cycle, you need an additional phase.
11. Finding the Start of the Cycle

Floyd's algorithm can do more than simply detect a cycle.

Example:

1 → 2 → 3 → 4 → 5
        ↑         ↓
        └─────────┘

Cycle starts at:

3
Phase 1 — Detect the cycle
slow = slow.Next;
fast = fast.Next.Next;

Wait until:

slow == fast;
Phase 2 — Reset one pointer
slow = head;

Keep fast where the meeting happened.

Then move both one step at a time:

slow = slow.Next;
fast = fast.Next;

When they meet again:

slow == fast

that node is the cycle starting node.

12. Finding Cycle Start — C#
public static ListNode? DetectCycle(ListNode? head)
{
    ListNode? slow = head;
    ListNode? fast = head;

    // Phase 1: Detect cycle
    while (fast != null && fast.Next != null)
    {
        slow = slow!.Next;
        fast = fast.Next.Next;

        if (slow == fast)
            break;
    }

    // No cycle
    if (fast == null || fast.Next == null)
        return null;

    // Phase 2: Find cycle start
    slow = head;

    while (slow != fast)
    {
        slow = slow!.Next;
        fast = fast!.Next;
    }

    return slow;
}
13. Important DSA Pattern

Remember Floyd's technique like this:

             Floyd's Cycle Detection
                       │
              ┌────────┴────────┐
              ↓                 ↓
           Slow              Fast
         1 step             2 steps
              │                 │
              └───────┬─────────┘
                      ↓
                 Do they meet?
                  /          \
                YES           NO
                 ↓             ↓
             Cycle          No cycle
One-line interview answer

Floyd's Cycle Detection uses two pointers moving at different speeds—slow by one step and fast by two steps—to detect a cycle in O(n) time and O(1) extra space.

Most important things to remember: slow = +1, fast = +2, slow == fast → cycle, and fast == null → no cycle.



Why is this better than HashSet?

A simple approach is:

HashSet<ListNode>

Store every visited node and check whether you've seen it before.

That requires O(n) extra space.

Floyd's technique needs only two pointers:

Time  → O(n)
Space → O(1)

So Floyd's Cycle Detection is the optimal approach for linked-list cycle detection.


Important pattern to remember
Slow → 1 step
Fast → 2 steps

fast == null
    ↓
No cycle

slow == fast
    ↓
Cycle

It is also useful beyond linked lists, such as finding a duplicate number when the array can be modeled as a functional graph.