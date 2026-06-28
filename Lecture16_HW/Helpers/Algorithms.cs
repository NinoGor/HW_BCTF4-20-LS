using System;
using System.Collections.Generic;
using System.Text;

namespace Lecture16_HW.Helpers
{
    internal static class Algorithms
    {
        // შენიშვნა: ვერ გავიხსენე/ვიპოვე, სადმე თუ შეგვხვედრია უკვე yield-ი, ამიტომ არ გამოვიყენებ

        // 1. Where
        // ფილტრავს კოლექციას და აბრუნებს მხოლოდ იმ ელემენტებს, რომლებიც მოცემულ პირობას აკმაყოფილებენ.
        public static IEnumerable<T> Where<T>(IEnumerable<T> collection, Predicate<T> predicate)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection), "Collection cannot be null.");
            if (predicate == null) throw new ArgumentNullException(nameof(predicate), "Filter predicate cannot be null.");

            // აღწერიდან როგორც ჩანს, ორიგინალი კოლექცია არ იცვლება, ელემენტებს ლისტის სახით დავაბრუნებ
            List<T> results = [];

            foreach (var item in collection)
            {
                if (predicate(item))
                {
                    results.Add(item);
                }
            }

            return results; 
        }

        // 2. OrderBy - უპარამეტროს გავაკეთებ, როგორც აღწერაშია და გამოვიყენებ bubble sort-ს
        // ალაგებს ელემენტებს ზრდადობით (Ascending).

        /* შენიშვნა: რადგან პირობაში მკაცრად არ ჩანს ზრდადობით დალაგებული ელემენტები უნდა დაბრუნდეს,
           თუ ორიგინალ კოლექციაშივე უნდა მოხდეს მოდიფიკაცია, ყოველი შემთხვევისთვის ორივეს დავწერ. */
        
        // მეთოდი რომელიც ორიგინალი კოლექციის მოდიფიკაციას არ აკეთებს
        public static IEnumerable<T> OrderBy<T>(IEnumerable<T> collection) 
            where T : IComparable<T> // რომ გამოვიყენო CompareTo
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection), "Collection cannot be null.");

            List<T> list = new(collection);
            int n = list.Count;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (list[j].CompareTo(list[j + 1]) > 0)
                    {
                        T temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
            return list;
        }

        // მეთოდი რომელიც ორიგინალ კოლექციაშივე ალაგებს ელემენტებს ზრდადობით
        // აქ დაგვჭირდება IList, რადგან ინდექსებზე წვდომა შეგვეძლოს
        public static void OrderByInPlace<T>(IList<T> list)
        where T : IComparable<T>
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list), "Collection cannot be null.");

            // შევამოწმოთ, მხოლოდ წაკითხვადი კოლექცია ხომ არ არის
            if (list.IsReadOnly)
                throw new NotSupportedException("Collection is unmodifiable.");

            int n = list.Count;

            // Bubble Sort
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (list[j].CompareTo(list[j + 1]) > 0)
                    {
                        T temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
        }

        // 3. First
        // აბრუნებს პირველ ელემენტს. თუ ელემენტი არ არსებობს, აგდებს Exception-ს.
        public static T First<T>(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection), "Collection cannot be null.");

            // foreach-ს გამოვიყენებ და პირველივე ელემენტს დავაბრუნებ
            // თუ ელემენტი არ არსებობს გადავა exception-ზე
            foreach (var item in collection)
            {
                return item;
            }
            // შესაბამისი LINQ მეთოდი ამ ტიპის exception-ს ისვრის ამ შემთხვევაში
            throw new InvalidOperationException("Sequence contains no elements.");
        }

        // 4. FirstOrDefault
        // აბრუნებს პირველ ელემენტს, ხოლო თუ ვერ იპოვა — აბრუნებს default მნიშვნელობას(null, 0, false და ა.შ.).
        public static T? FirstOrDefault<T>(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection), "Collection cannot be null.");

            foreach (var item in collection)
            {
                return item;
            }
            return default;
        }

        // 5. Single
        // აბრუნებს ერთადერთ ელემენტს, რომელიც პირობას აკმაყოფილებს. თუ არ არსებობს ან ერთზე მეტია — აგდებს Exception-ს.
        public static T Single<T>(IEnumerable<T> collection, Predicate<T> predicate)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection), "Collection cannot be null.");
            if (predicate == null) throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            bool found = false;
            T result = default!; 

            foreach (var item in collection)
            {
                if (predicate(item))
                {
                    if (found)
                    {
                        // შესაბამისი LINQ მეთოდი ამ ტიპის exception-ს ისვრის ამ შემთხვევაში
                        throw new InvalidOperationException("Sequence contains more than one matching element.");
                    }
                    result = item;
                    found = true;
                }
            }

            if (!found)
            {
                // შესაბამისი LINQ მეთოდი ამ ტიპის exception-ს ისვრის ამ შემთხვევაში
                throw new InvalidOperationException("No matching element was found.");
            }
            // წერს warning-ს თუმცა თუ კოდი აქამდე მოვიდა, ე.ი. მნიშვნელობა მიენიჭა
            // ამიტომ !-ით გავთიშე ზემოთ
            return result;
        }

        // 6. SingleOrDefault
        // აბრუნებს ერთადერთ ელემენტს, ხოლო თუ არ არსებობს — აბრუნებს default-ს. თუ ერთზე მეტია, მაინც აგდებს Exception-ს.
        public static T? SingleOrDefault<T>(IEnumerable<T> collection, Predicate<T> predicate)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection), "Collection cannot be null.");
            if (predicate == null) throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            bool found = false;
            // თუ ვერ იპოვა, დარჩება default-ად და დაბრუნდება
            T? result = default;

            foreach (var item in collection)
            {
                if (predicate(item))
                {
                    if (found)
                    {
                        // შესაბამისი LINQ მეთოდი ამ ტიპის exception-ს ისვრის ამ შემთხვევაში
                        throw new InvalidOperationException("Sequence contains more than one matching element.");
                    }
                    result = item;
                    found = true;
                }
            }

            return result; 
        }

        // 7. Any
        // ამოწმებს არსებობს თუ არა მინიმუმ ერთი ელემენტი, რომელიც პირობას აკმაყოფილებს. აბრუნებს true ან false.
        public static bool Any<T>(IEnumerable<T> collection, Predicate<T> predicate)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection), "Collection cannot be null.");
            if (predicate == null) throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            foreach (var item in collection)
            {
                // თუ ერთი მაინც ვიპოვეთ, რომელიც აკმაყოფილებს, ანუ შეგვიძლია true დავაბრუნოთ
                if (predicate(item)) return true;
            }
            return false;
        }

        // 8. All
        // ამოწმებს აკმაყოფილებს თუ არა ყველა ელემენტი მოცემულ პირობას. აბრუნებს true ან false.
        public static bool All<T>(IEnumerable<T> collection, Predicate<T> predicate)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection), "Collection cannot be null.");
            if (predicate == null) throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

            foreach (var item in collection)
            {
                // აქ კი პირიქით, თუ ერთი მაინც ვიპოვეთ, რომელიც არ აკმაყოფილებს, ბრუნდება false
                if (!predicate(item)) return false;
            }
            return true;
        }

        // 9. Count - default-ად null-ს გადავცემ და თუ "პირობა" არ იქნა გადაცემული, ჩვეულებრივად დაითვლის
        // ითვლის ელემენტების რაოდენობას (სურვილის შემთხვევაში მხოლოდ იმათს, რომლებიც პირობას აკმაყოფილებენ).
        public static int Count<T>(IEnumerable<T> collection, Predicate<T>? predicate = null)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection), "Collection cannot be null.");

            int count = 0;
            foreach (var item in collection)
            {
                // თუ პრედიკატი არ გადაეცა პირველი პირობა სულ true იქნება და ყველა ელემენტი დაითვლება
                // თუ არა-null პრედიკატი გადაეცა, პირველი სულ false იქნება და დავთვლით მეორე პირობის მიხედვით
                if (predicate == null || predicate(item))
                {
                    count++;
                }
            }
            return count;
        }

        // 10. Distinct
        // აშორებს დუბლირებულ ელემენტებს და ტოვებს მხოლოდ უნიკალურ მნიშვნელობებს.

        /* შენიშვნა: აღწერიდან როგორც გავიგე, ორიგინალი კოლექციიდან ვშლით დუბლირებულებს / ვტოვებთ
           უნიკალურ მნიშვნელობებს. თუმცა LINQ-ის შესაბამისი მეთოდი მხოლოდ აბრუნებს ასეთ მიმდევრობას.
           ყოველი შემთხვევისთვის დავწერ ორივე ვერსიას.

           ასევე, შესაძლოა მეთოდები უფრო სწრაფი გავხადოთ HashSet-ის გამოყენებით, 
           თუმცა ვეცადე სავარჯიშოდ მოვრიდებოდი
        */

        // ვერსია რომელიც ორიგინალი კოლექციის მოდიფიკაციის გარეშე აბრუნებს ელემენტებს
        public static IEnumerable<T> Distinct<T>(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection), "Collection cannot be null.");

            List<T> uniqueElements = new List<T>();

            foreach (var item in collection)
            {
                if (!uniqueElements.Contains(item))
                {
                    uniqueElements.Add(item);
                }
            }

            return uniqueElements;
        }

        // ვერსია რომელიც ორიგინალ კოლექციაზე მუშაობს / ამოდიფიცირებს მას
        public static void DistinctInPlace<T>(ICollection<T> collection)
        {
            // ეს მეთოდი ICollection-ზე იქნება, რომ შევძლოთ Clear/Add-ის გამოყენება.
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (collection.IsReadOnly)
                throw new NotSupportedException("Collection is unmodifiable.");


            // აქ შევინახავ "ნანახ" უნიკალურ მნიშვნელობებს, რომლებიც უნდა დაგვრჩეს
            var uniqueElements = new List<T>();
            foreach (var item in collection)
            {
                if (!uniqueElements.Contains(item))
                {
                    uniqueElements.Add(item);
                }
            }

            collection.Clear();
            foreach (var item in uniqueElements)
            {
                collection.Add(item);
            }
        }

    }
}