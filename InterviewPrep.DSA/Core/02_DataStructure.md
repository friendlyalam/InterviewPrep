# Data Structure

## Definition

A Data Structure is a way of organizing and storing data so that it can be accessed, searched, updated, and processed efficiently.

In simple words,

A Data Structure tells us how to organize data.

------------------------------------------------------------

Why do we need Data Structures?

Imagine all your clothes are lying on the floor.

Finding one shirt will take time.

Now arrange them in a cupboard.

Finding the shirt becomes easy.

The cupboard is acting like a Data Structure.

It organizes items.

------------------------------------------------------------

Real-Life Example 1

Cupboard

Different shelves store different clothes.

This makes searching easier.

------------------------------------------------------------

Real-Life Example 2

Library

Books are arranged according to categories.

You can quickly find a book.

------------------------------------------------------------

Technical Example 1

List<Employee>

Stores employees one after another.

Useful when order matters.

------------------------------------------------------------

Technical Example 2

Dictionary<int, Employee>

Stores employees using EmployeeId.

Searching becomes much faster.

------------------------------------------------------------

Common Data Structures

• Array

• List

• Linked List

• Stack

• Queue

• Dictionary (Hash Map)

• Hash Set

• Tree

• Graph

• Heap

------------------------------------------------------------

Important Points

A Data Structure does NOT create data.

It only organizes data.

Choosing the correct Data Structure makes software faster.

------------------------------------------------------------

PART 1 – Data Structures

These are the containers that store data.

| #  | Data Structure                     | Simple Definition                                       | Real-Life Example                         |
| -- | ---------------------------------- | ------------------------------------------------------- | ----------------------------------------- |
| 1  | Array                              | Stores similar items in continuous memory               | Train compartments                        |
| 2  | String                             | Sequence of characters                                  | A sentence in a book                      |
| 3  | Linked List                        | Nodes connected one after another                       | Treasure hunt using clues                 |
| 4  | Stack                              | Last In, First Out (LIFO)                               | Stack of plates                           |
| 5  | Queue                              | First In, First Out (FIFO)                              | People standing in a ticket queue         |
| 6  | Deque                              | Insert/Delete from both ends                            | Double-ended train platform               |
| 7  | HashMap / Dictionary               | Stores key-value pairs                                  | Phone contacts (Name → Number)            |
| 8  | HashSet                            | Stores only unique values                               | Guest list without duplicate names        |
| 9  | Tree                               | Hierarchical structure                                  | Family tree or company organization chart |
| 10 | Binary Tree                        | Every node has at most two children                     | Decision tree                             |
| 11 | Binary Search Tree (BST)           | Ordered binary tree                                     | Dictionary arranged alphabetically        |
| 12 | Heap                               | Special tree where highest/lowest priority stays on top | Hospital emergency queue                  |
| 13 | Trie                               | Stores words efficiently                                | Mobile keyboard auto-complete             |
| 14 | Graph                              | Nodes connected in many ways                            | Google Maps road network                  |
| 15 | Disjoint Set (Union-Find)          | Tracks connected groups                                 | Friend groups in a social network         |
| 16 | Segment Tree                       | Fast range queries and updates                          | Dashboard showing sales over date ranges  |
| 17 | Fenwick Tree (Binary Indexed Tree) | Efficient prefix sum updates                            | Running total of daily sales              |

-----------------------------------------------------------------

PART 2 – Problem Solving Techniques

These are the methods used to solve problems.

| #  | Technique                  | Simple Definition                            | Real-Life Example                                                 |
| -- | -------------------------- | -------------------------------------------- | ----------------------------------------------------------------- |
| 1  | Brute Force                | Try every possibility                        | Checking every key to open a lock                                 |
| 2  | Linear Scan                | Visit every element once                     | Finding the tallest student in a class                            |
| 3  | Two Pointers               | Two indices moving together                  | Two people searching from opposite ends of a bookshelf            |
| 4  | Sliding Window             | Maintain a moving range                      | Camera sliding over a football field                              |
| 5  | Prefix Sum                 | Store cumulative sums                        | Bank account running balance                                      |
| 6  | Difference Array           | Store only changes                           | Recording salary increases instead of every salary                |
| 7  | Binary Search              | Repeatedly divide the search space           | Guessing a number between 1 and 100                               |
| 8  | Fast & Slow Pointer        | Two pointers moving at different speeds      | Two runners on a circular track                                   |
| 9  | Recursion                  | Function solves a smaller version of itself  | Russian nesting dolls                                             |
| 10 | Divide and Conquer         | Split → Solve → Combine                      | Divide a large cleaning task among family members                 |
| 11 | Backtracking               | Try, undo, try another                       | Solving a maze                                                    |
| 12 | Greedy                     | Choose the best option now                   | Giving the largest currency notes first                           |
| 13 | Dynamic Programming        | Save previous answers to avoid repeated work | Remembering previous calculations instead of solving again        |
| 14 | BFS (Breadth-First Search) | Explore level by level                       | Finding all people one friendship away, then two friendships away |
| 15 | DFS (Depth-First Search)   | Go deep before coming back                   | Exploring one cave tunnel completely before trying another        |
| 16 | Monotonic Stack            | Stack kept in increasing/decreasing order    | Keeping books arranged by height                                  |
| 17 | Monotonic Queue            | Queue kept in sorted order                   | Maintaining the highest score in a moving window                  |
| 18 | Heap Technique             | Always access highest/lowest priority        | Airport boarding priority                                         |
| 19 | Hashing Technique          | Fast lookup using keys                       | Looking up a contact by name                                      |
| 20 | Bit Manipulation           | Solve using binary operations                | Using switches that are only ON or OFF                            |

----------------------------------------------
| Technique           | Common Keywords                  | Data Structures            | Typical Complexity |
| ------------------- | -------------------------------- | -------------------------- | ------------------ |
| Linear Scan         | Find max, min, search            | Array, List                | O(n)               |
| Two Pointers        | Reverse, pair, sorted            | Array, String, Linked List | O(n)               |
| Sliding Window      | Subarray, substring              | Array, String              | O(n)               |
| Prefix Sum          | Range sum                        | Array                      | O(n) preprocessing |
| Binary Search       | Sorted                           | Array                      | O(log n)           |
| Hashing             | Frequency, unique                | Array, String              | O(n)               |
| DFS                 | Tree, graph                      | Tree, Graph                | O(V + E)           |
| BFS                 | Level order                      | Tree, Graph                | O(V + E)           |
| Dynamic Programming | Maximum, minimum, number of ways | Array, Matrix, Tree        | Varies             |
