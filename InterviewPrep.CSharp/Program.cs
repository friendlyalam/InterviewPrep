// See https://aka.ms/new-console-template for more information
using InterviewPrep.CSharp.Collections.Generics._01_List;
using InterviewPrep.CSharp.Collections.Generics._02_Dictionary;
using InterviewPrep.CSharp.Collections.Generics._03_HashSet;
using InterviewPrep.CSharp.Collections.Generics._04_SortedSet;
using InterviewPrep.CSharp.Collections.Generics._05_Stack;
using InterviewPrep.CSharp.Collections.Generics._06_Queue;

Console.WriteLine("Hello, World!");

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
#endregion

#endregion