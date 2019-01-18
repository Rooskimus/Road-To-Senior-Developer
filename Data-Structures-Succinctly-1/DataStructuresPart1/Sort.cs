using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresPart1
{
    public class Sort<T>
        where T : IComparable<T>
    {
        #region BubbleSort
        public void BubbleSort(T[] items) // 0(n^2) on average; 0(n) at best case.
        {
            bool swapped;
            do
            {
                swapped = false;
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i - 1].CompareTo(items[i]) > 0)
                    {
                        Swap(items, i - 1, i);
                        swapped = true;
                    }
                }
            } while (swapped != false);
        }
        #endregion

        #region InsertionSort

        public void InsertionSort(T[] items) // O(n^2) on average; 0(n) at best case.
        {
            int sortedRangeEndIndex = 1;
            while (sortedRangeEndIndex < items.Length)
            {
                if (items[sortedRangeEndIndex].CompareTo(items[sortedRangeEndIndex - 1]) < 0) // If item to the left of the index is bigger than the item at the index
                {
                    int insertIndex = FindInsertionIndex(items, items[sortedRangeEndIndex]);
                    Insert(items, insertIndex, sortedRangeEndIndex);
                }
                sortedRangeEndIndex++;
            }
        }

        private int FindInsertionIndex(T[] items, T valueToInsert) // For use with Insertion Sort
        {
            for (int index = 0; index < items.Length; index++)
            {
                if (items[index].CompareTo(valueToInsert) > 0) return index; // If the value to insert is smaller than the value at the index, we want to insert there.
            }
            throw new InvalidOperationException("The insertion index was not found");

        }

        private void Insert(T[] itemArray, int indexInsertingAt, int indexInsertingFrom)  // Also for use with Insertion Sort
        {
            T temp = itemArray[indexInsertingAt];  // Copy item here to insert at the end
            itemArray[indexInsertingAt] = itemArray[indexInsertingFrom]; // Move value we're sorting to it's proper place in the list.
            for (int current = indexInsertingFrom; current > indexInsertingAt; current--)
            {
                itemArray[current] = itemArray[current - 1]; // Copies values in the array from their current index to one index right starting on the right.
            }
            itemArray[indexInsertingAt + 1] = temp; // Replaces item in proper sorted position (overwrites a duplicate leftover from shifting others right).
        }
        #endregion

        #region SelectSort
        public void SelectSort(T[] items)
        {
            int sortedRangeEnd = 0;
            while (sortedRangeEnd < items.Length)
            {
                int nextIndex = FindIndexOfSmallestFromIndex(items, sortedRangeEnd);
                Swap(items, sortedRangeEnd, nextIndex);
                sortedRangeEnd++;
            }
        }

        private int FindIndexOfSmallestFromIndex(T[] items, int sortedRangeEnd)
        {
            T currentSmallest = items[sortedRangeEnd];
            int currentSmallestIndex = sortedRangeEnd;
            for(int i = sortedRangeEnd + 1; i < items.Length; i++)
            {
                if (currentSmallest.CompareTo(items[i]) > 0) // if currentSmallest is larger than item[i]
                {
                    currentSmallest = items[i];
                    currentSmallestIndex = i;
                }
            }

            return currentSmallestIndex;
        }
        #endregion

        public void MergeSort(T[] items) // O(n log n); requires more memory O(n) instead of O(1) up until now.  Not ideal for data that is nearly sorted.
        {
            if (items.Length <= 1)
            {
                return;
            }
            int leftSize = items.Length / 2;
            int rightSize = items.Length - leftSize;
            T[] left = new T[leftSize];
            T[] right = new T[rightSize];
            Array.Copy(items, 0, left, 0, leftSize);
            Array.Copy(items, leftSize, right, 0, rightSize);
            Sort(left);
            Sort(right);
            Merge(items, left, right);
        }

        private void Merge(T[] items, T[] left, T[] right)
        {
            //Left off here
        }

        void Swap(T[] items, int left, int right) // a shared method for swapping values in an array by index
        {
            if (left != right)
            {
                T temp = items[left];
                items[left] = items[right];
                items[right] = temp;
            }
        }
    }
}
