Array Reversal / In-Place Array Technique

This is an important DSA technique because it teaches you how to modify an array without using another array.

1. What is Array Reversal?

Given:

[1, 2, 3, 4, 5]

Reverse it to:

[5, 4, 3, 2, 1]

The common optimal approach is the two-pointer technique.

left →              ← right

[1, 2, 3, 4, 5]
 ↑                 ↑
left              right

Swap the two values, then move both pointers toward the center.

2. In-place means what?

In-place means modifying the original array instead of creating another array.

❌ Not in-place
int[] result = new int[nums.Length];

You're using another array.

✅ In-place
int temp = nums[left];
nums[left] = nums[right];
nums[right] = temp;

Only a few variables are used.

3. Optimal C# implementation
public static void Reverse(int[] nums)
{
    if (nums is null)
        throw new ArgumentNullException(nameof(nums));

    if (nums.Length <= 1)
        return;

    int left = 0;
    int right = nums.Length - 1;

    while (left < right)
    {
        int temp = nums[left];
        nums[left] = nums[right];
        nums[right] = temp;

        left++;
        right--;
    }
}

The widget above isn't relevant to array reversal; the key DSA idea here is the two-pointer movement, so focus on the code/process rather than the widget.

4. Dry run

Array:

[1, 2, 3, 4, 5]
Step 1
left = 0
right = 4

[1, 2, 3, 4, 5]
 ↑           ↑
 L           R

Swap:

[5, 2, 3, 4, 1]

Move:

left++
right--
Step 2
[5, 2, 3, 4, 1]
    ↑     ↑
    L     R

Swap:

[5, 4, 3, 2, 1]

Move again.

Step 3
left = 2
right = 2

Now:

left < right

is false, so stop.

Final:

[5, 4, 3, 2, 1]
5. Why left < right?

This is important.

We only need to swap pairs.

When:

left == right

we are at the middle element.

For:

[1, 2, 3, 4, 5]

3 is already in the correct position after the outer swaps.

So:

while (left < right)

is correct.

6. Complexity
| Complexity  | Value                       |
| ----------- | --------------------------- |
| Time        | **O(n)**                    |
| Extra Space | **O(1)**                    |
| Technique   | **Two Pointers + In-place** |

Technique	Two Pointers + In-place

Why O(n)?

We process approximately half the elements:

n / 2

But constants are ignored in Big-O:

O(n / 2) = O(n)
7. Why is this important for DSA?

Array reversal itself is simple, but the technique is reusable.

The same ideas appear in:

Reverse String
Reverse Array
Reverse Linked List
Reverse part of an array
Rotate Array
Palindrome checking
Two Sum variants
Partitioning
Dutch National Flag
Reverse words
Reverse a subarray

The important thing to learn is not just:

"How do I reverse an array?"

It's:

"Can I solve this by modifying the input in-place using two pointers?"

⭐ Points to Remember
In-place → modify the original array.
Avoid creating another array when O(1) extra space is required.
Use two pointers: left and right.

Start:

left = 0;
right = nums.Length - 1;
Swap nums[left] and nums[right].

Move:

left++;
right--;

Continue while:

left < right
Time complexity = O(n).
Extra space = O(1).
The original array is modified.
Mental model
LEFT  →  [ elements ]  ← RIGHT
           ↓
         SWAP
           ↓
     LEFT++ / RIGHT--
           ↓
        repeat
           ↓
         center
           ↓
          STOP

For your DSA preparation, this is the core "In-place + Two Pointers" pattern.