// See https://aka.ms/new-console-template for more information
using InterviewPrep.CSharp.Collections.Generics._01_List;
using InterviewPrep.CSharp.Collections.Generics._02_Dictionary;
using InterviewPrep.CSharp.Collections.Generics._03_HashSet;
using InterviewPrep.CSharp.Collections.Generics._04_SortedSet;

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
#endregion

#endregion