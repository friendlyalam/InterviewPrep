// See https://aka.ms/new-console-template for more information
using InterviewPrep.CSharp.Collections.Generics._01_List;
using InterviewPrep.CSharp.Collections.Generics._02_Dictionary;
using InterviewPrep.CSharp.Collections.Generics._03_HashSet;
using InterviewPrep.CSharp.Collections.Generics._04_SortedSet;
using InterviewPrep.CSharp.Collections.Generics._05_Stack;
using InterviewPrep.CSharp.Collections.Generics._06_Queue;
using InterviewPrep.CSharp.Collections.Generics._07_LinkedList;
using InterviewPrep.CSharp.Collections.Generics._08_SortedDictionary;
using InterviewPrep.CSharp.Collections.Generics._09_SortedList;
using InterviewPrep.CSharp.Collections.NonGenerics._01_ArrayList;
using InterviewPrep.CSharp.Collections.NonGenerics._02_Hashtable;
using InterviewPrep.CSharp.Collections.NonGenerics._03_SortedList;
using InterviewPrep.CSharp.Collections.NonGenerics._04_Stack;
using InterviewPrep.CSharp.Collections.NonGenerics._05_Queue;


#region Collection

#region Generic collections

#region List<T> Example
GenericListExample genericListExample = new GenericListExample();
genericListExample.GenericListBuiltMethods();
#endregion

#region Dictionary example
GenericDictionaryExample genericDictionaryExample = new GenericDictionaryExample();
genericDictionaryExample.DictionaryExample();
#endregion

#region HasSet Example
GenericHashSetExample genericHashSetExample = new GenericHashSetExample();
genericHashSetExample.HasSetExample();
#endregion

#region SortedSet Example
GenericSortedSetExample genericSortedSetExample = new GenericSortedSetExample();
genericSortedSetExample.SortedSetExample();
#endregion

#region Stack Example
GenericStackExample genericStackExample = new GenericStackExample();
genericStackExample.StackExample();
#endregion

#region Queue Example
GenericQueueExample genericQueueExample = new GenericQueueExample();
genericQueueExample.QueueExample();
#endregion

#region Linkedlist Example
GenericLinkedListExample genericLinkedListExample = new GenericLinkedListExample();
genericLinkedListExample.LinkedListExample();
#endregion

#region SortedDictionary Example
GenericSortedDictionaryExample genericSortedDictionaryExample = new GenericSortedDictionaryExample();
genericSortedDictionaryExample.SortedDictionaryExample();

#endregion

#region SortedList Example
GenericSortedListExample genericSortedListExample = new GenericSortedListExample();
genericSortedListExample.SortedListExample();
#endregion


#endregion

#region Non Generic Collections

#region ArrayList Example
NonGenericArrayListExample nonGenericArrayListExample= new NonGenericArrayListExample();
nonGenericArrayListExample.ArrayListExample();
#endregion

#region Hashtable example
NonGenericHashtable nonGenericHashtable = new NonGenericHashtable();
nonGenericHashtable.HashtableExample();
#endregion

#region SortedList example
NonGenericSortedListExample nonGenericSortedListExample= new NonGenericSortedListExample();
nonGenericSortedListExample.SortedlistExample();
#endregion

#region Stack example
NonGenericStackExample nonGenericStackExample = new NonGenericStackExample();
nonGenericStackExample.StackExample();
#endregion

#region Queue example
NonGenericQueueExample nonGenericQueueExample= new NonGenericQueueExample();
nonGenericQueueExample.QueueExample();
#endregion

#endregion

#endregion