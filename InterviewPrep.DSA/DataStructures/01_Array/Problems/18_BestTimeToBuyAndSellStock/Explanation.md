# Array Problem 18 — Best Time to Buy and Sell Stock

## Problem

Given an array `prices` where `prices[i]` represents the stock price on day `i`.

Choose exactly one day to buy and a different day in the future to sell.

Return the maximum profit.

If no profit is possible, return `0`.

---

# Better Approach — Brute Force

## Why?

We need to find the best combination of:

- Buying day
- Selling day

The selling day must always be after the buying day.

The simplest approach is to try every valid buy/sell combination.

---

## Idea

1. Select every possible buying day.
2. For each buying day, check every future selling day.
3. Calculate:

   `Profit = Selling Price - Buying Price`

4. Keep the maximum profit.
5. Return `0` if no positive profit exists.

---

## Dry Run

Input:

[7, 1, 5, 3, 6, 4]

Some valid combinations:

7 -> 1 = -6
7 -> 5 = -2
7 -> 6 = -1

1 -> 5 = 4
1 -> 3 = 2
1 -> 6 = 5
1 -> 4 = 3

The maximum profit is:

5

Therefore:

Answer = 5

---

## Time Complexity

O(n²)

### Why?

The outer loop chooses a buying day.

The inner loop checks all future selling days.

In the worst case, approximately:

n × n

comparisons are performed.

Therefore:

O(n²)

---

## Space Complexity

O(1)

### Why?

Only a few variables are used.

No additional data structure is created.

-----------------------------------------------------------------------------------------------------------------

# Optimal Approach — One Pass

## Why?

We don't need to compare every possible pair.

While moving from left to right, we can remember the lowest price seen so far.

For every current price:

`Current Profit = Current Price - Lowest Previous Price`

Then keep the maximum profit.

---

## Important Rule

The buying price must occur BEFORE the selling price.

Therefore, we only compare today's price with the minimum price found in previous days.

---

## Idea

Maintain two variables:

- `minBuyPrice` — lowest price seen so far
- `maxProfit` — maximum profit found so far

For every price:

1. If the price is lower than `minBuyPrice`, update `minBuyPrice`.
2. Otherwise calculate today's profit.
3. Update `maxProfit` if today's profit is greater.
4. Continue until the end.

---

## Dry Run

Input:

[7, 1, 5, 3, 6, 4]

Initial:

minBuyPrice = 7
maxProfit = 0

### Price = 1

1 < 7

minBuyPrice = 1

### Price = 5

Profit:

5 - 1 = 4

maxProfit = 4

### Price = 3

Profit:

3 - 1 = 2

maxProfit = 4

### Price = 6

Profit:

6 - 1 = 5

maxProfit = 5

### Price = 4

Profit:

4 - 1 = 3

maxProfit = 5

Final:

Answer = 5

---

## Why Does This Work?

At every day, `minBuyPrice` represents the cheapest price available BEFORE the current day.

Therefore, when we calculate:

Current Price - minBuyPrice

we are always performing a valid transaction:

Buy first -> Sell later

We don't need to remember every previous price.

We only need the cheapest one.

---

## Time Complexity

O(n)

### Why?

The array is traversed exactly once.

Each element requires constant-time operations.

Therefore:

O(n)

---

## Space Complexity

O(1)

### Why?

Only a fixed number of variables are used:

- minBuyPrice
- maxProfit
- profit

No additional data structure is required.

---

# Edge Cases

## 1. Single Element

Input:

[5]

There is no future day to sell.

Output:

0

---

## 2. Continuously Decreasing Prices

Input:

[7, 6, 4, 3, 1]

Every possible transaction results in a loss.

Output:

0

---

## 3. Continuously Increasing Prices

Input:

[1, 2, 3, 4, 5]

Buy at 1 and sell at 5.

Output:

4

---

## 4. Lowest Price Appears After a Higher Price

Input:

[2, 4, 1]

We cannot buy at 1 and sell at 4 because 1 occurs after 4.

Valid transaction:

Buy = 2
Sell = 4

Profit:

4 - 2 = 2

Output:

2

---

## 5. Best Buying Price Is in the Middle

Input:

[7, 6, 4, 1, 5]

Buy at 1 and sell at 5.

Output:

4

---

## 6. Null Array

Input:

null

The method throws:

ArgumentNullException

This is defensive handling because the original problem constraints do not allow null.

---

## 7. Empty Array

Input:

[]

The method throws:

ArgumentException

This is defensive handling because the original problem requires at least one price.

---

# Key DSA Pattern

When you see:

- Buy once
- Sell once
- Buy must happen before sell
- Find maximum profit

Think:

Track Minimum So Far + Maximum Profit

Pattern:

minBuyPrice = minimum price seen before today

profit = currentPrice - minBuyPrice

maxProfit = maximum profit found so far

Complexity:

Time: O(n)
Space: O(1)