1. Definition

Graph Traversal means visiting every required node (or vertex) in a graph, tree, or matrix following a specific order.

The two most common traversal methods are:

BFS (Breadth-First Search)
DFS (Depth-First Search)

--------------------------------------------------------------------------------------------------------
2. Why Do We Need Graph Traversal?

Suppose you have:

A social network
A road map
A computer network
A family tree
A game map

You need to explore all connected places.

Instead of randomly visiting nodes,

Graph Traversal provides a systematic approach.

--------------------------------------------------------------------------------------------------------
3. Simple Definition

Graph Traversal is a way of visiting every connected node in a structured manner.

--------------------------------------------------------------------------------------------------------
4. Real-Life Examples
Example 1 – Visiting a Building

Imagine a building with 3 floors.

BFS

Visit floor by floor.

Floor 1

↓

Floor 2

↓

Floor 3
DFS

Keep going as deep as possible.

Floor 1

↓

Room

↓

Cupboard

↓

Drawer

↓

Return
Example 2 – Family Tree
Grandfather

├── Father
│     ├── You
│     └── Brother
│
└── Uncle
      ├── Cousin1
      └── Cousin2

BFS visits generation by generation.

DFS explores one family branch completely before moving to another.

Example 3 – Google Maps

Need the shortest route between two cities?

Many shortest-path algorithms build upon graph traversal concepts.

--------------------------------------------------------------------------------------------------------
5. Technical Example

Consider the graph:

        A
      /   \
     B     C
    / \     \
   D   E     F
BFS Order
A

↓

B C

↓

D E F

Output:

A B C D E F
DFS Order
A

↓

B

↓

D

↓

Back

↓

E

↓

Back

↓

C

↓

F

Output:

A B D E C F

--------------------------------------------------------------------------------------------------------
6. Core Idea
BFS

Visit all nearby nodes first.

Think:

Level by Level
DFS

Go as deep as possible first.

Think:

Depth First

--------------------------------------------------------------------------------------------------------
7. Visualization
BFS
            A

        /       \

      B           C

    /   \          \

   D     E          F

Traversal:

A

↓

B C

↓

D E F
DFS
A

↓

B

↓

D

↓

Back

↓

E

↓

Back

↓

C

↓

F

--------------------------------------------------------------------------------------------------------
8. Generic C# Templates
BFS

Uses a Queue

Queue<Node> queue = new();

queue.Enqueue(start);

while (queue.Count > 0)
{
    Node current = queue.Dequeue();

    foreach (var neighbor in current.Neighbors)
    {
        // Visit neighbor
    }
}
DFS (Recursive)

Uses Recursion

void DFS(Node node)
{
    // Visit node

    foreach (var neighbor in node.Neighbors)
    {
        DFS(neighbor);
    }
}
DFS (Iterative)

Uses a Stack

Stack<Node> stack = new();

stack.Push(start);

while (stack.Count > 0)
{
    Node current = stack.Pop();

    // Visit node
}

--------------------------------------------------------------------------------------------------------
9. Which Data Structures Commonly Use BFS & DFS?

| Data Structure | BFS | DFS |
| -------------- | :-: | :-: |
| Graph          |  ✅  |  ✅  |
| Tree           |  ✅  |  ✅  |
| Matrix         |  ✅  |  ✅  |
| Grid           |  ✅  |  ✅  |

--------------------------------------------------------------------------------------------------------
10. Recognition Clues
Think BFS when you see:
Shortest path (unweighted graph)
Minimum number of steps
Level order traversal
Nearest
Distance
Multi-source expansion
Think DFS when you see:
Explore all paths
Connected components
Islands
Maze exploration
Cycle detection
Tree traversal


--------------------------------------------------------------------------------------------------------
11. BFS vs DFS

| BFS                                      | DFS                                                            |
| ---------------------------------------- | -------------------------------------------------------------- |
| Breadth First Search                     | Depth First Search                                             |
| Visits level by level                    | Goes deep first                                                |
| Uses Queue                               | Uses Stack or Recursion                                        |
| Finds shortest path in unweighted graphs | Doesn't guarantee shortest path                                |
| Higher memory on wide graphs             | Lower memory on wide graphs (but recursion depth can be large) |


--------------------------------------------------------------------------------------------------------
12. Famous Interview Problems
BFS
Binary Tree Level Order Traversal
Rotting Oranges
Word Ladder
Open the Lock
Minimum Steps
Shortest Path in Binary Matrix


DFS
Number of Islands
Flood Fill
Clone Graph
Path Sum
Course Schedule (with cycle detection variants)
Surrounded Regions
 

--------------------------------------------------------------------------------------------------------
13. When Should We NOT Use BFS?

Avoid BFS when:

Memory is limited and the graph is very wide.
You don't need the shortest path.
You only need to deeply explore one branch.

--------------------------------------------------------------------------------------------------------
14. When Should We NOT Use DFS?

Avoid DFS when:

You specifically need the shortest path in an unweighted graph.
Deep recursion may cause a stack overflow.
Level-by-level processing is required.

--------------------------------------------------------------------------------------------------------
15. Time Complexity

Assume:

V = Number of Vertices (Nodes)
E = Number of Edges

Both BFS and DFS visit every vertex and edge at most once.

--------------------------------------------------------------------------------------------------------
| Algorithm |     Time |
| --------- | -------: |
| BFS       | O(V + E) |
| DFS       | O(V + E) |


--------------------------------------------------------------------------------------------------------
16. Space Complexity

| Algorithm       |             Space |
| --------------- | ----------------: |
| BFS             |      O(V) (Queue) |
| DFS (Recursive) | O(V) (Call Stack) |
| DFS (Iterative) |      O(V) (Stack) |

--------------------------------------------------------------------------------------------------------
17. Advantages
BFS

✅ Finds shortest path in unweighted graphs.

✅ Excellent for level-order processing.

DFS

✅ Simple recursive implementation.

✅ Excellent for exploring connected structures.

✅ Ideal for many tree algorithms.

--------------------------------------------------------------------------------------------------------
18. Disadvantages
BFS

❌ Can consume a lot of memory.

DFS

❌ Recursive implementation may cause stack overflow on very deep graphs.

❌ Doesn't automatically find the shortest path.

--------------------------------------------------------------------------------------------------------
19. Frequently Asked Interview Questions
Q1. What is Graph Traversal?

Answer:

A systematic method of visiting nodes in a graph, tree, or matrix.

Q2. Which data structure does BFS use?

Answer:

Queue (FIFO).

Q3. Which data structure does DFS use?

Answer:

Stack (LIFO) or Recursion (which internally uses the call stack).

Q4. Which algorithm finds the shortest path in an unweighted graph?

Answer:

BFS.

Q5. Which is better: BFS or DFS?

Answer:

Neither is universally better.

Choose based on the problem:

Need shortest path → BFS.
Need deep exploration or traversal → DFS.

--------------------------------------------------------------------------------------------------------
20. Common Mistakes

❌ Forgetting to mark nodes as visited, causing infinite loops in graphs with cycles.

❌ Using DFS when the problem explicitly asks for the minimum number of steps.

❌ Using BFS when recursion provides a much simpler solution for a tree traversal.

❌ Confusing Queue and Stack.

--------------------------------------------------------------------------------------------------------
21. Summary
Need to explore connected nodes?

        │
        ▼

Need shortest path
in an unweighted graph?

        │
   Yes ─────► BFS (Queue)

        │
        No
        │
        ▼

Need deep exploration,
tree traversal, or
connected components?

        │
        ▼

DFS (Stack / Recursion)

--------------------------------------------------------------------------------------------------------
22. Complete Technique Cheat Sheet


| Technique                 | Recognition Words                              | Common Data Structures      |        Typical Complexity |
| ------------------------- | ---------------------------------------------- | --------------------------- | ------------------------: |
| Brute Force               | Try all                                        | All                         |                    Varies |
| Linear Scan               | Find, count, maximum                           | Array, String               |                      O(n) |
| Two Pointers              | Pair, reverse, palindrome                      | Array, String, Linked List  |                      O(n) |
| Sliding Window            | Contiguous subarray/substring                  | Array, String               |                      O(n) |
| Prefix Sum                | Range sum                                      | Array, Matrix               |    Build O(n), Query O(1) |
| Hashing                   | Duplicate, frequency, lookup                   | Array, String, HashMap      |              Average O(1) |
| Binary Search             | Sorted, answer space                           | Array, Matrix               |                  O(log n) |
| Recursion                 | Smaller problem                                | Tree, Graph                 |                   Depends |
| Backtracking              | All possibilities                              | Array, String, Matrix       |       Usually Exponential |
| Divide & Conquer          | Split, merge                                   | Array, Matrix               |          Often O(n log n) |
| Greedy                    | Minimum, maximum, interval, schedule           | Array, Heap, Graph          |          Often O(n log n) |
| Dynamic Programming       | Count ways, longest, shortest, optimization    | Array, Matrix, String, Tree | Often O(n), O(n²), O(n×m) |
| Graph Traversal (BFS/DFS) | Connected, shortest path, islands, level order | Graph, Tree, Matrix, Grid   |                  O(V + E) |


--------------------------------------------------------------------------------------------------------
