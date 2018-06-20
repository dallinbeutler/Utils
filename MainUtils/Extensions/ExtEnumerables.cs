using FastMember;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Extensions
{
    public static class ExtEnumerables
    {
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> list)
        {
            return list.ToDictionary(x => x.Key, y => y.Value);
        }
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<Tuple<TKey, TValue>> list)
        {
            return list.ToDictionary(x => x.Item1, y => y.Item2);
        }

        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified predicate,
        /// and returns the zero-based index of the first occurrence within the entire <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        /// The zero-based index of the first occurrence of an element that matches the conditions defined by <paramref name="predicate"/>, if found; otherwise it'll throw.
        /// </returns>
        public static int FindIndex<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            var idx = list.Select((value, index) => new { value, index }).Where(x => predicate(x.value)).Select(x => x.index).First();
            return idx;
        }

        public static string ListToString(this IEnumerable<object> inString, string separator)
        {
            return String.Join(separator, inString.ToArray());
        }
        public static string ToString<T>(this ObservableCollection<T> inList, string separator) where T : class
        {
            return ListToString(inList.AsEnumerable(), separator);
        }

        public static bool MoveToFrontOfListWhere<T>(this List<T> collection, Func<T, bool> predicate)
        {
            if (collection == null || collection.Count <= 0) return false;

            int index = -1;
            for (int i = 0; i < collection.Count; i++)
            {
                T element = collection.ElementAt(i);
                if (!predicate(element)) continue;
                index = i;
                break;
            }

            if (index == -1) return false;

            T item = collection[index];
            collection[index] = collection[0];
            collection[0] = item;
            return true;
        }


        public static void AddSet<TKey>(this Dictionary<TKey, int> dict, TKey inKey, int value)
        {
            int tv;
            dict.TryGetValue(inKey, out tv);
            if (tv != 0)
                dict[inKey] = tv + value;
        }

        //taranslates any list of objects directly to a datatable
        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        public static bool MoveToFrontOfListWhere<T>(this IList<T> collection, Func<T, bool> predicate)
        {
            if (collection == null || collection.Count <= 0) return false;

            int index = -1;
            for (int i = 0; i < collection.Count; i++)
            {
                T element = collection.ElementAt(i);
                if (!predicate(element)) continue;
                index = i;
                break;
            }

            if (index == -1) return false;

            T item = collection[index];
            collection[index] = collection[0];
            collection[0] = item;
            return true;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            DataTable table = new DataTable();
            using (var reader = ObjectReader.Create(data))
            {
                table.Load(reader);
            }
            return table;
        }
    }
}
